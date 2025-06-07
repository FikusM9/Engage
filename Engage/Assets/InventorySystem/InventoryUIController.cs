// This simple class allows the player to interact with the inventory tabs
// using the keyboard

using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private GameObject playerLoadoutCanvas;
    [SerializeField] private GameObject playerInventoryCanvas;

    public static bool InventoryOpen { get; private set; }

    private bool isOpen;

    private void Start()
    {
        InventoryOpen = false;
        isOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            SetCanvasesActive(isOpen);
            InventoryOpen = isOpen;
        }
    }

    private void SetCanvasesActive(bool active)
    {
        playerLoadoutCanvas.SetActive(active);
        playerInventoryCanvas.SetActive(active);

        if (!active)
        {
            UISlot.CancelDragging();
        }
    }
}
