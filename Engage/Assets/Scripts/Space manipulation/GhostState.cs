using UnityEngine;

// Enables setting the object's Invisibility and Ghost statuses
// Invisibility - disables the renderer
// Ghost - disables the collider

public class GhostState : MonoBehaviour
{
    [SerializeField] private bool startInvisible = false;
    [SerializeField] private bool startGhost = false;

    private Renderer selfRenderer;
    private Collider selfCollider;

    void Awake()
    {
        selfRenderer = GetComponent<Renderer>();
        selfCollider = GetComponent<Collider>();

        if (startInvisible) SetVisible(false);
        if (startGhost) SetGhost(true);
    }

    public void SetVisible(bool visible)
    {
        if (selfRenderer != null)
            selfRenderer.enabled = visible;
    }

    public void SetGhost(bool isGhost)
    {
        if (selfCollider != null)
            selfCollider.enabled = !isGhost;
    }

    // Invert current state
    public void ToggleVisibility()
    {
        if (selfRenderer == null) return;
        SetVisible(!selfRenderer.enabled);
    }

    // Invert current state
    public void ToggleGhost()
    {
        if (selfCollider == null) return;
        SetGhost(selfCollider.enabled);
    }

}
