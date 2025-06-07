using UnityEngine;

public class Enemyproba : MonoBehaviour 
{
    public float amplitude = 2f;
    public float frequency = 1f;
    public float speed = 3f;
    public LayerMask wallLayer;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public float rotationSmoothness = 5f; // Added: Controls rotation speed

    private Vector2 startPosition;
    private int direction = 1;
    private float timeOffset;
    private Vector2 currentMoveDirection; // Added: Track movement direction

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        
        startPosition = rb.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void FixedUpdate()
    {
        // Calculate desired velocity
        float targetXVelocity = speed * direction;
        float targetYVelocity = Mathf.Cos((Time.time + timeOffset) * frequency) * amplitude * frequency;

        // Store movement direction for rotation
        currentMoveDirection = new Vector2(targetXVelocity, targetYVelocity).normalized;

        // Apply force to reach target velocity
        Vector2 force = new Vector2(
            (targetXVelocity - rb.linearVelocity.x) * rb.mass,
            (targetYVelocity - rb.linearVelocity.y) * rb.mass
        );

        rb.AddForce(force);

        // Smooth rotation update
        UpdateSpriteRotation();
    }

    void UpdateSpriteRotation()
    {
        if (currentMoveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(currentMoveDirection.y, currentMoveDirection.x) * Mathf.Rad2Deg + 180;
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(
                transform.rotation, 
                targetRotation, 
                rotationSmoothness * Time.fixedDeltaTime
            );
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (wallLayer == (wallLayer | (1 << collision.gameObject.layer)))
        {
            direction *= -1;
            startPosition = rb.position;
            timeOffset = Time.time * frequency;
        }
    }

    private void OnValidate()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }
}