using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed = 5f;

    private float _shakeDuration = 0f;
    private float _shakeMagnitude = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Shake
        if (_shakeDuration > 0)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * _shakeMagnitude;
            smoothedPosition += new Vector3(shakeOffset.x, shakeOffset.y, 0);
            _shakeDuration -= Time.deltaTime;
        }

        transform.position = smoothedPosition;
    }
    public void TriggerShake(float duration, float magnitude)
    {
        _shakeDuration = duration;
        _shakeMagnitude = magnitude;
    }
}
