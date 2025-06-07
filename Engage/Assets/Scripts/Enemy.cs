using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float health;
    void Start()
    {
        health = 5f; 

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
           Destroy(gameObject);
        }
    }
}
