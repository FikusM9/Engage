using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Vector2 _movement;
    public float rotationSpeed = 10f;
    
    public GameObject myArrow;
    public GameObject arrowPrefab;
    private CameraShake _cameraShake;
    private Animator _animator;

    public GameObject head;
    public GameObject arm1;
    public GameObject arm2;
    
    public Camera myCamera;
    

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _cameraShake = FindObjectOfType<CameraShake>();
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

        if (Input.GetMouseButtonDown(1) && GameManager.CurrentStopWatchCount > 0 && GameManager.Timer > 1.1f)
        {
            UseStopWatch();
        }
    }
    void FixedUpdate() 
    {

        if (_movement == Vector2.zero)
        {
            _animator.Play("PlayerIdle2d");
        }
        else
        {
            _animator.Play("PlayerWalking2d");
        }
        
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
        //_cameraShake.Shake(0.05f, 0.1f);
        GameManager.CurrentBulletCount--;
        
        GameObject arrow = Instantiate(arrowPrefab, myArrow.transform.position, myArrow.transform.rotation);
        
        if (GameManager.CurrentBulletCount <= 0)
        {
            myArrow.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GetHit();
        }

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            GameManager.CurrentCheckPointIndex= other.gameObject.transform.GetSiblingIndex();
        }
    }

    void UseStopWatch()
    {
        GameManager.CurrentStopWatchCount--;
        GameManager.Timer = 1.1f;
    }
    
    public void GetHit()
    {
        GameManager.Health--;
        if (GameManager.Health <= 0)
        {
            print("Game Over");
        }
        myCamera.GetComponent<CameraFollow>().TriggerShake(0.1f, 0.02f);
        _cameraShake.Shake(0.1f, 0.02f);
        StartCoroutine(OnHitFlash());
    }
    
    IEnumerator OnHitFlash()
    {
        SpriteRenderer sr1= head.GetComponent<SpriteRenderer>();
        SpriteRenderer sr2 = arm1.GetComponent<SpriteRenderer>();
        SpriteRenderer sr3 = arm2.GetComponent<SpriteRenderer>();
        Color originalColor = Color.white;
        float pulseTime = 0.1f;
        int pulseCount = 3;

        for (int i = 0; i < pulseCount; i++)
        {
            // Fade to transparent
            float t = 0f;
            while (t < pulseTime)
            {
                float alpha = Mathf.Lerp(1f, 0f, t / pulseTime);
                sr1.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                sr2.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                sr3.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                t += Time.deltaTime;
                yield return null;
            }

            // Fade back to original
            t = 0f;
            while (t < pulseTime)
            {
                float alpha = Mathf.Lerp(0f, 1f, t / pulseTime);
                sr1.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                sr2.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                sr3.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                t += Time.deltaTime;
                yield return null;
            }
        }

        // Ensure exact final color
        sr1.color = originalColor;
        sr2.color = originalColor;
        sr3.color = originalColor;
    }
}
