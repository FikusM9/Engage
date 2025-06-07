// WIP: potencijalno podesiti uticaj na rotaciju

using System.Collections.Generic;
using UnityEngine;

// A vortex which alters the passing of time for the objects inside
// NOTE: put this script on an object with a trigger collider

public class TimeVortex : MonoBehaviour
{
    [Range(0.01f, 1f)]
    public float vortexMultiplier = 0.2f;   //  vortex speed factor

    private HashSet<Rigidbody> affectedBodySet = new HashSet<Rigidbody>();
    private Dictionary<Rigidbody, Vector3> originalVelocityDict = new Dictionary<Rigidbody, Vector3>();

    private void Awake()
    {
        // make sure the object is a trigger
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        if (!affectedBodySet.Contains(rb))
        {
            originalVelocityDict[rb] = rb.velocity;
            rb.velocity *= vortexMultiplier;
            //rb.linearDamping = 10f; // Optional
            affectedBodySet.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null || !affectedBodySet.Contains(rb)) return;

        if (originalVelocityDict.TryGetValue(rb, out Vector3 original))
        {
           // rb.linearDamping = 0f;
            rb.velocity = original;
        }

        affectedBodySet.Remove(rb);
        originalVelocityDict.Remove(rb);
    }

    private void OnDisable()
    {
        // Restore affected objects if the vortex is disabled or destroyed
        foreach (var rb in affectedBodySet)
        {
            if (rb != null && originalVelocityDict.TryGetValue(rb, out Vector3 original))
            {
               // rb.linearDamping = 0f;
                rb.velocity = original;
            }
        }

        affectedBodySet.Clear();
        originalVelocityDict.Clear();
    }
}
