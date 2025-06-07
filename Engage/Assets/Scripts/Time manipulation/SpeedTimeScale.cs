using UnityEngine;

// Scales the speed of time with the selected player's velocity

public class SpeedTimeScale : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerRigidbody;

    [SerializeField]
    private float speedFactor = 0.5f;

    [SerializeField]
    private float minMultiplier = 0.1f;
    [SerializeField]
    private float maxMultiplier = 10f;
    [SerializeField]
    private float smoothScaleDuration = 1f; // how long time will take to scale (in seconds)

    private float baseMultiplier;

    void FixedUpdate()
    {
        if (playerRigidbody != null && TimeManipulation.Instance != null)
        {
            baseMultiplier = TimeManipulation.Instance.GetCurrentMultiplier();

            float speed = playerRigidbody.velocity.magnitude;
            float newMultiplier = baseMultiplier + speed * speedFactor;

            // Keeps the multiplier inside the min-max range
            newMultiplier = Mathf.Clamp(newMultiplier, minMultiplier, maxMultiplier);

            TimeManipulation.Instance.SmoothTimeScale(newMultiplier, smoothScaleDuration);
        }
    }

}
