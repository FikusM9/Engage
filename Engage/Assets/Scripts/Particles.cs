using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem particles;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void DestroyParticles()
    {
        StartCoroutine(DestroyAfterDelay(5f));
    }
    
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
