using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    public static CameraShake ShakeInstance;
    
    private Vector3 _initialPosition;
    private Coroutine _shakeRoutine;

    public void Awake()
    {
        
    }

    void OnEnable()
    {
        _initialPosition = transform.localPosition;
    }

    public void Shake(float duration, float magnitude)
    {
        if (_shakeRoutine != null)
            StopCoroutine(_shakeRoutine);

        _shakeRoutine = StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = _initialPosition + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _initialPosition;
        _shakeRoutine = null;
    }
}