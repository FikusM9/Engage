using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{

    public float speed = 10f;
    //public float acceleration = 10f;
    public float jumpHeight = 3f;
    public float mouseSensitivity = 100f;
    public Transform camTransform;
    public TextMeshProUGUI scope;
    public GameObject inventory;

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
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        move = transform.position;
        pickUpLayer = LayerMask.GetMask("Interactable");
        scope.color = scopeGray;
        pickedItems = new List<GameObject>();
        inventoryController = inventory.GetComponent<InventoryController>();
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

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
        /*if (velocity.y > 0.01f)
            move = targetMove;
        else
            move = Vector3.Lerp(move, targetMove, acceleration * Time.deltaTime);*/

        float currentSpeed = isGrounded ? speed : speed / 3;

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(targetMove*speed*Time.deltaTime + velocity * Time.deltaTime);
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);
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
            /*try
            {
                PickUpController pickUp = hit.collider.gameObject.GetComponent<PickUpController>();

                if (pickUp == null) return;

            }
            catch 
            {
                Debug.Log("Missing PickUpController script");
            }*/
            
        }
        else if (!isHit && canPickUp)
        {
            canPickUp = false;
            scope.color = scopeGray;
            triggeredItem = null;
        }
    }

    private void PickUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (triggeredItem == null) return;
            pickedItems.Add(triggeredItem);
            Sprite sprite = triggeredItem.GetComponent<PickUpController>().icon;
            int itemId = triggeredItem.GetComponent<PickUpController>().id;
            inventoryController.AddItem(sprite, itemId);
            triggeredItem.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            isTriggered = true;
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
                triggeredItem = null;
            }
        }
    }

}
