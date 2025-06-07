using UnityEngine;

public class EnemyFollow : MonoBehaviour {
    [Header("Movement Settings")]
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public float rotationSpeed = 10f;
    public LayerMask wallLayer;

    [Header("Detection Settings")]
    public float detectionRadius = 5f;
    public LayerMask playerLayer;
    public Vector2 detectionOffset = Vector2.zero;

    [Header("References")]
    public Rigidbody2D rb;
    public Transform player;
    
    private int direction = 1;
    private Vector2 currentVelocity;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        
        if (player == null)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj) player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        if (PlayerInDetectionRange() && player != null)
        {
            // Chase player
            Vector2 targetPosition = Vector2.MoveTowards(rb.position, player.position, chaseSpeed * Time.fixedDeltaTime);
            currentVelocity = (targetPosition - rb.position).normalized;
            rb.MovePosition(targetPosition);
        }
        else
        {
            // Patrol in straight line
            Vector2 movement = new Vector2(patrolSpeed * direction * Time.fixedDeltaTime, 0);
            rb.MovePosition(rb.position + movement);
            currentVelocity = movement.normalized;
        }

        // Rotate to face movement direction
        if (currentVelocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(currentVelocity.y, currentVelocity.x) * Mathf.Rad2Deg + 180;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (wallLayer == (wallLayer | (1 << collision.gameObject.layer)))
        {
            direction *= -1;
        }
    }

    bool PlayerInDetectionRange()
    {
        if (player == null) return false;
        return Physics2D.OverlapCircle((Vector2)transform.position + detectionOffset, detectionRadius, playerLayer);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + detectionOffset, detectionRadius);
    }
}
