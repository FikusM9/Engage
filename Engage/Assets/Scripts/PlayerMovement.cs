using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 _movement;
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        
        _movement.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate() 
    {
        rb.MovePosition(rb.position + _movement.normalized * moveSpeed * Time.fixedDeltaTime);
        
        Vector2 moveDirection = new Vector2(_movement.x, _movement.y).normalized;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle+ 90f); // Adjust the angle if needed

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
