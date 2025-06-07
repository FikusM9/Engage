using UnityEditor.Build;
using UnityEngine;

// Manages the aging process of AgeableObjects.

public class AgeManager : MonoBehaviour
{
    // singleton
    public static AgeManager Instance { get; private set; }

    [SerializeField] private float globalAgeSpeed = 1f;
    [SerializeField] private bool isAgingPaused = false;
    private bool isAgingReversed = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public float GetAgingDeltaTime()
    {
        return isAgingPaused ? 0f : Time.deltaTime * globalAgeSpeed;
    }

    public bool IsPaused()
    {
        return isAgingPaused;
    }

    public bool IsReversed()
    {
        return isAgingReversed;
    }

    public float GetGlobalAgeSpeed()
    {
        return globalAgeSpeed;
    }

    public void SetPaused(bool paused)
    {
        isAgingPaused = paused;
    }

    public void SetGlobalAgeSpeed(float speed)
    {
        if (speed == 0) isAgingPaused = true;
        else isAgingPaused = false;

        if (speed > 0) isAgingReversed = false;
        else isAgingReversed = true;
        
        globalAgeSpeed = speed;
    }
}
