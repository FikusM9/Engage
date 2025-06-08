using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy1 : MonoBehaviour
{
    
    public Particles bloodParticles;
    [NonSerialized] public Camera MyCamera;

    private Vector2 _startingPosition;
    
    private AudioSource _audioSource;
    public AudioClip hitSound;


    private void Awake()
    {
        _startingPosition = transform.position;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        MyCamera= Camera.main;
    }
    
    public void ResetPosition()
    {
        transform.position = _startingPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Arrow"))
        {
            AudioSource.PlayClipAtPoint(hitSound, Camera.main.transform.position);
            MyCamera.GetComponent<CameraFollow>().TriggerShake(0.2f, 0.1f);
            bloodParticles.particles.Play();
            bloodParticles.transform.SetParent(null);
            bloodParticles.DestroyParticles();
            Destroy(gameObject); 
        }
    }
}
