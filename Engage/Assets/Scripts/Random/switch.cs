// ON/OF switch za igraca potrebno vezati skriptu "Player Interaction"

using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    private bool isActive = false;
    private Renderer rendOfActive;
    private Renderer rendOfInactive;
    [Header("Light game objects")]
    [SerializeField] private GameObject activeLight;
    [SerializeField] private GameObject inactiveLight;
    [Header("Materials")]
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveMaterial;

    private void Awake()
    {
        rendOfActive = activeLight.GetComponent<Renderer>();
        rendOfInactive = inactiveLight.GetComponent<Renderer>();
    }
    public void Interact()
    {
        isActive = !isActive;
        ChangeLights();
    }

    private void ChangeLights()
    {
        if (isActive)
        {
            rendOfActive.material = activeMaterial;
            rendOfInactive.material = defaultMaterial;
        }
        else
        {
            rendOfActive.material = defaultMaterial;
            rendOfInactive.material = inactiveMaterial;
        }
    }
   
}
