// WIP: Potencijalno dodati zaustavljanje i rotacije

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows broader time manipulation methods

public class TimeManipulation : MonoBehaviour
{
    // singleton
    public static TimeManipulation Instance { get; private set; }

    [SerializeField]
    private string ignoredLayerName = "Player";

    private float defaultTimeScale = 1f;
    private float currentMultiplier = 1f;

    private Dictionary<Rigidbody, Vector3> objectSpeedDict = new Dictionary<Rigidbody, Vector3>();

    bool timeStopped;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float GetCurrentMultiplier()
    {
        return currentMultiplier;
    }

    public void StopTime()
    {
        Time.timeScale = 0f;
        Debug.Log("Time stopped");
    }

    public void ResumeTime()
    {
        Time.timeScale = defaultTimeScale * currentMultiplier;
    }

    public void SetTimeMultiplier(float multiplier)
    {
        currentMultiplier = multiplier;
        Time.timeScale = defaultTimeScale * currentMultiplier;
    }

    // Using SmoothTimeScale is prefferable to SetTimeMultiplier
    public void SmoothTimeScale(float targetMultiplier, float duration)
    {
        StartCoroutine(SmoothTimeTransition(targetMultiplier, duration));
    }

    private IEnumerator SmoothTimeTransition(float targetMultiplier, float duration)
    {
        float startMultiplier = currentMultiplier;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float newMultiplier = Mathf.Lerp(startMultiplier, targetMultiplier, elapsed / duration);
            SetTimeMultiplier(newMultiplier);
            yield return null;
        }

        SetTimeMultiplier(targetMultiplier);
    }

    public void StopTimeForAllExceptPlayers()
    {
        Rigidbody[] allRigidbodies = FindObjectsByType<Rigidbody>(FindObjectsSortMode.None);
        int ignoredLayer = LayerMask.NameToLayer(ignoredLayerName);

        foreach (var rb in allRigidbodies)
        {
            if (rb.gameObject.layer == ignoredLayer) continue;

            if (!objectSpeedDict.ContainsKey(rb))
                objectSpeedDict[rb] = rb.velocity;

            rb.velocity = Vector3.zero;
            //rb.linearDamping = 100f;
        }

        timeStopped = true;
        Debug.Log("Time stopped for all except layer: " + ignoredLayerName);
    }

    public void ResumeTimeForAll()
    {
        foreach (var entry in objectSpeedDict)
        {
            Rigidbody rb = entry.Key;
            if (rb != null)
            {
                //rb.linearDamping = 0f;
                rb.velocity = entry.Value;
            }
        }

        objectSpeedDict.Clear();
        timeStopped = false;
        Debug.Log("Time resumed");
    }

    void FixedUpdate()
    {
        if (timeStopped)
        {
            foreach (var rb in objectSpeedDict.Keys)
            {
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }

}
