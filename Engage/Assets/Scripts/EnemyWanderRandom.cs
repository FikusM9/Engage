using UnityEngine;

public class EnemyWanderRandom : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float wanderRadius = 5f; // Max distance from start point
    public float minWanderTime = 1f;
    public float maxWanderTime = 3f;
    public float rotationSpeed = 5f;

    [Header("References")]
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    private Vector2 _startPosition;
    private Vector2 _targetPosition;
    private float _wanderTimer;
    private Vector2 _currentDirection;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        
        _startPosition = transform.position;
        SetNewRandomTarget();
    }

    void FixedUpdate()
    {
        // Move toward target
        Vector2 moveDirection = (_targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = moveDirection * moveSpeed;
        
        // Smooth rotation
        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg + 180;
            Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Check if reached target or time expired
        _wanderTimer -= Time.fixedDeltaTime;
        if (Vector2.Distance(transform.position, _targetPosition) < 0.5f || _wanderTimer <= 0)
        {
            SetNewRandomTarget();
        }
    }

    void SetNewRandomTarget()
    {
        // Get random point within wander radius
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
        float randomDistance = UnityEngine.Random.Range(0.5f, wanderRadius);
        _targetPosition = _startPosition + randomDirection * randomDistance;
        
        // Set random duration for this movement
        _wanderTimer = Random.Range(minWanderTime, maxWanderTime);
        
        // Store current direction for facing
        _currentDirection = (_targetPosition - (Vector2)transform.position).normalized;
    }

    void OnDrawGizmosSelected()
    {
        // Draw wander radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Application.isPlaying ? _startPosition : transform.position, wanderRadius);
        
        // Draw current target
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_targetPosition, 0.2f);
    }
}
