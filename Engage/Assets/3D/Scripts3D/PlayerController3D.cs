using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{

    public float speed = 10f;
    public float acceleration = 20f;
    public float jumpHeight = 3f;
    public float mouseSensitivity = 100f;
    public Transform camTransform;
    public TextMeshProUGUI scope;
    public GameObject inventory;
    public AudioClip loot;
    public AudioClip lava;
    public AudioClip walk;
    public GameObject lavaVisualEffect;

    CharacterController characterController;

    Vector3 move = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    float gravity = -9.81f;
    bool isGrounded;
    float camRotationY = 0f;

    LayerMask pickUpLayer;
    public float pickUpDistance = 3f;
    public bool isTriggered = false;
    bool canPickUp = false;
    List<GameObject> pickedItems;
    GameObject triggeredItem;


    Color scopeGray = new Color(107f/255f, 107f/255f, 107f/255f);
    Color scopeYellow = new Color(255f/255f, 251f/255f, 0);

    InventoryController inventoryController;

    bool inLava = false;

    AudioSource audioSource;

    bool isWalking = false;

    bool isDoorOpen = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        move = transform.position;
        pickUpLayer = LayerMask.GetMask("Interactable");
        scope.color = scopeGray;
        pickedItems = new List<GameObject>();
        if(inventory != null)
            inventoryController = inventory.GetComponent<InventoryController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Jump();
        Move();

        //characterController.Move(velocity * Time.deltaTime);

        Look();
        ScopeDetection();
        PickUp();


    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 targetMove = (transform.right*moveX + transform.forward*moveY).normalized;
        
        if (targetMove.magnitude < 0.2f)
        {
            move = Vector3.zero;
        }
        else
        {
            move = Vector3.Lerp(move, targetMove, acceleration * Time.deltaTime);
            if (!isWalking)
            {
                isWalking = true;
                if(audioSource != null)
                    audioSource.Play();
            }
        }

        if (!isGrounded || move == Vector3.zero)
        {
            if (!inLava && isWalking)
            {
                isWalking = false;
                if (audioSource != null)
                    audioSource.Pause();
            }
        }
            

        float currentSpeed = isGrounded ? speed : speed / 4;

        velocity.y += 3f * gravity * Time.deltaTime;

        characterController.Move(move*speed*Time.deltaTime + velocity * Time.deltaTime);
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -5f);
        }

    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        Vector3 rotation = Vector3.up * mouseX;
        transform.Rotate(rotation);

        camRotationY -= mouseY;
        camRotationY = Mathf.Clamp(camRotationY, -70, 70f);
        camTransform.localRotation = Quaternion.Euler(camRotationY, 0f, 0f);
    }


    void ScopeDetection()
    {
        if (!isTriggered) return;
        Ray ray = new Ray(camTransform.position, camTransform.forward);
        RaycastHit hit;
        bool isHit = Physics.Raycast(ray, out hit, pickUpDistance, pickUpLayer);
        if (isHit && !canPickUp)
        {
            if (hit.collider.isTrigger) return;
            canPickUp = true;
            scope.color = scopeYellow;
            triggeredItem = hit.collider.gameObject;
            triggeredItem.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (!isHit && canPickUp)
        {
            canPickUp = false;
            scope.color = scopeGray;
            if(triggeredItem != null)
            {
                triggeredItem.transform.GetChild(0).gameObject.SetActive(false);
            }
            triggeredItem = null;
        }
    }

    private void PickUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (triggeredItem == null) return;

            audioSource.loop = false;
            if (audioSource != null)
                audioSource.PlayOneShot(loot);

            pickedItems.Add(triggeredItem);
            
            GameObject myTag = triggeredItem.transform.Find("MyTag").gameObject;
            
            if(myTag.CompareTag("Crossbow"))
            {
                GameManager.HasCrossbow = true;
            }

            if (myTag.CompareTag("ArrowPickUp"))
            {
                GameManager.CurrentBulletCount++;
            }

            if (myTag.CompareTag("Heal"))
            {
                GameManager.Health++;
            }

            if (myTag.CompareTag("Key"))
            {
                GameManager.HasKey = true;
            }
            
            //Debug.Log(triggeredItem.gameObject);
            Sprite sprite = triggeredItem.GetComponent<PickUpController>().icon;
            int itemId = triggeredItem.GetComponent<PickUpController>().id;
            triggeredItem.SetActive(false);
            triggeredItem = null;
            if (sprite == null) return;
            inventoryController.AddItem(sprite, itemId);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        print("Trigger Entered: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Interactable"))
        {
            isTriggered = true;
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            if (!inLava)
            {
                audioSource.clip = lava;
                audioSource.loop = true;
                if (audioSource != null)
                    audioSource.Play();
                inLava = true;
                lavaVisualEffect.SetActive(true);
                StartCoroutine(InLava());
            }
        }
        
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            GameManager.CurrentCheckPointIndex= other.gameObject.transform.GetSiblingIndex();
        }
        else if (other.gameObject.CompareTag("Door") && !isDoorOpen)
        {
            Animator doorAnimator = other.gameObject.GetComponent<Animator>();
            doorAnimator.SetTrigger("Open");
            isDoorOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            isTriggered = false;
            if (canPickUp)
            {
                canPickUp = false;
                scope.color = scopeGray;
                triggeredItem.transform.GetChild(0).gameObject.SetActive(false);
                triggeredItem = null;
            }
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            inLava = false;
            lavaVisualEffect.SetActive(false);
            if (audioSource != null)
                audioSource.Pause();
            audioSource.clip = walk;
        }
    }


    private IEnumerator InLava()
    {
        while (inLava)
        {
            Debug.Log("GORIMMM");
            yield return new WaitForSeconds(1);
        }
    }

}
