//vezuje se za video nadzor sa kojim kada se interaktuje preko celog ekrana vidite ono sto ta kamera nadgleda, za igraca potrebno vezati skriptu "Player Interaction"

using UnityEngine;

public class SecurityCamera : MonoBehaviour,IInteractable
{
    [Header("kamera video nadzora i igraceva kamera")]
    [SerializeField] private Camera securityCamera;
    [SerializeField] private Camera mainCamera;
    private bool isActive = false;

    public void Interact()
    {
        securityCamera.gameObject.SetActive(!isActive);
        mainCamera.gameObject.SetActive(isActive);
        isActive = !isActive;
    }
}
