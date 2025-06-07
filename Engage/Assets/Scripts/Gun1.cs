using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : MonoBehaviour
{
    public float speed;
    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = -(mousePosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle),  speed*Time.fixedDeltaTime);
    }
}
