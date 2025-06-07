using UnityEngine;
using System;
using System.Collections.Generic;

// Attached to a spherical object, pulls other objects in,
// not allowing escape after a certain point (event horizon).

[RequireComponent(typeof(SphereCollider))]
public class BlackHole : MonoBehaviour
{
    [SerializeField]
    private float pullForce = 10f;
    [SerializeField]
    private float eventHorizonScale = 0.5f; // The scale of the event horizon compared to the black hole radius.

    private const float MAX_FORCE = 100f;   // prevents extreme speeds
    private float maxDistance;
    private float eventHorizonRadius;

    private HashSet<Rigidbody> lockedBodies = new HashSet<Rigidbody>();

    public Action<Rigidbody> OnEnterEventHorizon = rb => { };   // Can be assigned externally

    private void Awake()
    {
        SphereCollider col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        maxDistance = col.radius;
        eventHorizonRadius = maxDistance * eventHorizonScale;
    }

    private void PullObject(Rigidbody rb)
    {
        Vector3 toCenter = transform.position - rb.position;
        float distance = toCenter.magnitude;
        Vector3 directionToCenter = toCenter.normalized;

        // Strong pull based on inverse square
        float normalizedDistance = distance / maxDistance;
        float force = Mathf.Min(pullForce / (normalizedDistance * normalizedDistance), MAX_FORCE);

        rb.AddForce(directionToCenter * force, ForceMode.Acceleration);
    }

    private void LockIfInside(Rigidbody rb)
    {
        Vector3 toCenter = transform.position - rb.position;
        float distance = toCenter.magnitude;
        Vector3 directionToCenter = toCenter.normalized;

        // If inside event horizon, prevent escape
        if (distance <= eventHorizonRadius)
        {
            if (!lockedBodies.Contains(rb))
            {
                lockedBodies.Add(rb);
                OnEnterEventHorizon(rb);    // Optional additional action upon entering the event horizon
            }

            Vector3 velocity = rb.velocity;
            float awayComponent = Vector3.Dot(velocity, directionToCenter);
            if (awayComponent < 0f)
            {
                // Remove outward motion
                Vector3 inwardVelocity = velocity - directionToCenter * awayComponent;
                rb.velocity = inwardVelocity;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;
        PullObject(rb);
        LockIfInside(rb);
    }

    private void OnTriggerExit(Collider other)
    {
        // Clean up if needed
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null)
        {
            lockedBodies.Remove(rb);
        }
    }
}
