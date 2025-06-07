using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyproba : MonoBehaviour
{
    public float horizontalSpeed = 2f;
    public float verticalAmplitude = 0.5f;
    public float verticalFrequency = 2f;

    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        float x = transform.position.x - horizontalSpeed * Time.deltaTime;
        float y = _startPos.y + Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
