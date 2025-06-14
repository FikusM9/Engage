using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float acceleration = 10f;
    public float maxSpeed = 30f;

    private Vector3 _direction;
    private float _currentSpeed;
    
    [NonSerialized] public AudioSource AudioSource;
    public AudioClip shootSound;
    public AudioClip hitSound;

    public void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.PlayOneShot(shootSound);
        _direction = transform.rotation * Vector2.up;
        _currentSpeed = initialSpeed;
        StartCoroutine(DestroyArrow());
    }

    public void PlayHitSound()
    {
        AudioSource.PlayOneShot(hitSound);
    }

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    void FixedUpdate()
    {
        _currentSpeed += acceleration * Time.fixedDeltaTime;
        _currentSpeed = Mathf.Min(_currentSpeed, maxSpeed);

        transform.position += _direction * _currentSpeed * Time.fixedDeltaTime;
    }
}
