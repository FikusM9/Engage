using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (mainCamera == null) return;

        // Offset the canvas above the plant
        if (transform.parent != null)
        {
            transform.position = transform.parent.position + new Vector3(0, 0.5f, 0); // adjust Y as needed
        }

        // Rotate only horizontally to face the camera
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0f; // keep it upright
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(-direction);
    }
}
