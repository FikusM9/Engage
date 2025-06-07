using UnityEngine;
using System.Collections.Generic;

// Allows broader gravity manipulation methods

public class GravityManipulator : MonoBehaviour
{
    // singleton
    public static GravityManipulator Instance { get; private set; }

    [SerializeField]
    private float gravityScale = 1f;
    private Vector3 gravityDirection = Vector3.down;
    private HashSet<int> ignoredLayerIndices = new HashSet<int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void FixedUpdate()
    {
        // Update Unity's built-in gravity based on the set direction and scale
        Physics.gravity = gravityDirection.normalized * (9.81f * gravityScale);
    }

    public void SetGravityScale(float scale)
    {
        gravityScale = scale;
    }

    public float GetGravityScale()
    {
        return gravityScale;
    }

    public void SetGravityDirection(Vector3 direction, bool retainInertia = false)
    {
        if (!retainInertia)
        {
            foreach (Rigidbody rb in FindObjectsByType<Rigidbody>(FindObjectsSortMode.None))
            {
                if (!IsIgnoredLayer(rb.gameObject.layer))
                {
                    RemoveInertia(rb);
                }
            }
        }
        gravityDirection = direction.normalized;
    }

    private void RemoveInertia(Rigidbody rb)
    {
        rb.velocity = Vector3.ProjectOnPlane(rb.velocity, gravityDirection.normalized);
    }

    public void StopGravity(bool retainInertia = false)
    {
        foreach (Rigidbody rb in FindObjectsByType<Rigidbody>(FindObjectsSortMode.None))
        {
            if (!IsIgnoredLayer(rb.gameObject.layer))
            {
                rb.useGravity = false;  // Disables Unity’s gravity for specific objects
                if (!retainInertia)
                {
                    RemoveInertia(rb);
                }
            }
        }
    }

    public void RestoreGravity()
    {
        foreach (Rigidbody rb in FindObjectsByType<Rigidbody>(FindObjectsSortMode.None))
        {
            if (!IsIgnoredLayer(rb.gameObject.layer))
            {
                rb.useGravity = true;   // Re-enables Unity’s gravity
            }
        }
    }

    public void IgnoreGravityForLayer(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer != -1)
        {
            ignoredLayerIndices.Add(layer);
            SetGravityForLayer(layer, false);
        }
    }

    public void RestoreGravityForLayer(string layerName)
    {
        int layer = LayerMask.NameToLayer(layerName);
        if (layer != -1 && ignoredLayerIndices.Contains(layer))
        {
            ignoredLayerIndices.Remove(layer);
            SetGravityForLayer(layer, true);
        }
    }

    private void SetGravityForLayer(int layer, bool enable)
    {
        foreach (Rigidbody rb in FindObjectsByType<Rigidbody>(FindObjectsSortMode.None))
        {
            if (rb.gameObject.layer == layer)
            {
                rb.useGravity = enable;
            }
        }
    }

    private bool IsIgnoredLayer(int layer)
    {
        return ignoredLayerIndices.Contains(layer);
    }
}
