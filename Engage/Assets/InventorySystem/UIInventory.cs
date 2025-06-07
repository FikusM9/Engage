// The UIInventory is used to initialize the UI elements, as well as populate the
// inventory grid with prefab slots based on the given PlayerInventory ScriptableObject.

using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] protected PlayerInventory inventoryData;
    [SerializeField] protected GameObject slotTemplate;
    [SerializeField] protected Transform slotParent;

    protected UISlot[,] uiSlots;

    void Start()
    {
        GenerateInventoryUI();
        UpdateInventoryUI();
    }

    void GenerateInventoryUI()
    {
        int rows = inventoryData.Rows;
        int columns = inventoryData.Columns;
        uiSlots = new UISlot[rows, columns];

        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject slotObj = Instantiate(slotTemplate, slotParent);
                slotObj.name = $"Slot_{r + 1}_{c + 1}";
                UISlot uiSlot = slotObj.GetComponent<UISlot>();
                uiSlot.Initialize(inventoryData.GetSlot(r, c));
                uiSlots[r, c] = uiSlot;
            }
        }
    }

    public void UpdateInventoryUI()
    {
        for (int r = 0; r < inventoryData.Rows; r++)
        {
            for (int c = 0; c < inventoryData.Columns; c++)
            {
                uiSlots[r, c].Refresh();
            }
        }
    }
}
