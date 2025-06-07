using UnityEngine;

public class GunMovement : MonoBehaviour
{
    public Camera cam;
    public Rigidbody2D rb;
    public Transform player;

    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (player == null) return;
        // Make gun follow player position with offset
        transform.position = player.position;
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (angle > 90 || angle < -90)
        {
            sr.flipY = true; // Or flipX depending on your sprite orientation
        }
        else
        {
            sr.flipY = false;
        }
    }
}
