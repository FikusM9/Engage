using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 _movement;
    public float rotationSpeed = 10f;

    public GameObject myArrow;
    public GameObject arrowPrefab;


    private void OnEnable()
    {
        if (GameManager.CurrentBulletCount > 0)
        {
            myArrow.SetActive(true);
        }
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        
        _movement.y = Input.GetAxisRaw("Vertical");
        
        if (Input.GetMouseButtonDown(0) && GameManager.CurrentBulletCount > 0)
        {
            SpawnArrow();
        }
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
    
    
    
    void SpawnArrow()
    {
        GameManager.CurrentBulletCount--;
        
        GameObject arrow = Instantiate(arrowPrefab, myArrow.transform.position, myArrow.transform.rotation);
        
        if (GameManager.CurrentBulletCount <= 0)
        {
            myArrow.SetActive(false);
        }
    }
}
