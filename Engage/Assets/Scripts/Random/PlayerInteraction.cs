// vezuje se za igraca i koristi se za interaktovanje sa game objectima koji imaju vezanu skriptu sa interfejsom IInteractable, npr. door

using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 direction;
    [SerializeField] float interactionRange = 300f; // Range za interakciju sa objektima
    [SerializeField] GameObject cam; // izdvojiti ovu logiku nekako u kameru ...
    [SerializeField] GameObject cam2;
    private void OnInteract()
    {
        TryInteract();
    }

    private void Awake()
    {
        Set3rdPersonPositionVectors();
        //cam2.SetActive(false); ne znam sta ce ovo ovde, will fix that
    }

    private void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, interactionRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                return;
            }
        }
    }
    private void Set3rdPersonPositionVectors() {
        origin = transform.position;
        direction = transform.forward;
    }
    private void Set1stPersonPositionVectors()
    {
        origin = Camera.main.transform.position;
        direction = Camera.main.transform.forward;
    }
}