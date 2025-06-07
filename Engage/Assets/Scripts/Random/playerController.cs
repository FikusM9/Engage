// buguje kad se drzi W/A/S/D kad dashuje i npr ako skocis u zid i i dalje drzis dugme da ides u zid "zalepi se za zid" treba da dodam raycasts za svaku stranu
// izolovati dash od kamere

using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private PlayerControll playerControlls; 
    private Rigidbody rb;
    private Vector2 moveInputValue;
    private Vector3 moveDirection = new Vector3(0, 0, 1);
    public float rayLen = 1f;

    [Header("Movement settings")]
    [SerializeField] private float walkingSpeed = 6f;
    [SerializeField] private float sprintingSpeed = 12;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float dashForce = 3f;
    [SerializeField] private float dashCooldown = 10f;
    [SerializeField] float jumpCooldown = 0.5f;
    //bools
    private bool canDash = true;
    private bool canJump = true;
    private bool isSprinting = false;
    //temps
    private float dashCooldownTemp;
    private float jumpCooldownTemp;
    private float movementSpeed;

    // Kamera koristena je pod nazivom First Person, potrebno je za target dodati objekat koji prati / iz kog gleda
    [Header("FP camera")]
    [SerializeField] public Camera cam;

    void Awake()
    {
        playerControlls = new PlayerControll();
        rb = GetComponent<Rigidbody>();
        movementSpeed = walkingSpeed;
        dashCooldownTemp = dashCooldown;
        jumpCooldownTemp = jumpCooldown;
    }

    private void FixedUpdate()
    {
        HandleFirstPersonMovement();
        Timer(ref dashCooldownTemp, ref canDash);
        Timer(ref jumpCooldownTemp, ref canJump);       
    }

    private void OnMove(InputValue inputValue) { moveInputValue = inputValue.Get<Vector2>(); }
    private void OnJump() 
    {
        if (canJump && FireRay((transform.position + new Vector3(0f, 0.1f, 0f)), -(Vector3.up), rayLen, "JUMP"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCooldownTemp = jumpCooldown;
            canJump = !canJump;
        }
    }

    private void OnDash()
    {
        if (canDash)
        {
            Vector3 cameraForward = cam.transform.forward;
            cameraForward.y = 0f;
            rb.AddForce(cameraForward * dashForce, ForceMode.Impulse);
            dashCooldownTemp = dashCooldown;
            canDash = !canDash;
        }
    }
    
    private void OnSprint()
    {
        if (isSprinting) { movementSpeed = sprintingSpeed; }
        else { movementSpeed = walkingSpeed; }
        isSprinting = !isSprinting;

    }


    private void HandleSimpleThirdPersonMovement()
    {
        if (moveInputValue.sqrMagnitude > 0)
        {
            Vector3 tempVector = new Vector3(moveInputValue.x, 0f, moveInputValue.y);
            moveDirection = tempVector.normalized;
            rb.velocity = moveDirection * movementSpeed;
        }
    }
    private void HandleFirstPersonMovement()
    {
       transform.eulerAngles = new Vector3(transform.eulerAngles.x, cam.transform.eulerAngles.y, transform.eulerAngles.z); 
        if (moveInputValue.sqrMagnitude > 0)
        {
            Vector3 cameraForward = cam.transform.forward;
            Vector3 cameraRight = cam.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();
            moveDirection = (cameraRight * moveInputValue.x + cameraForward * moveInputValue.y).normalized;
            rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
        }
    }

    private bool FireRay(Vector3 origin, Vector3 direction, float  rayDistance, string rayName)
    {
        RaycastHit hit;
        // Debug.DrawRay(origin, direction * rayDistance, Color.green);    //za debug upaliti po potrebi 
        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            // Debug.Log(rayName + " hit: " + hit.collider.name);          //za debug
            return true;
        }
        else { return false; }
    }
    private void Timer( ref float cooldownTempTime,ref bool canDo)
    {
        bool isCounting = !canDo;
        if (isCounting) { cooldownTempTime =cooldownTempTime - Time.fixedDeltaTime; } 
        if (cooldownTempTime <= 0) { canDo = true; }
    }
}

