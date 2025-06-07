// This class was written to allow the developer to manually add items to a 
// PlayerInventory object using the context menu during runtime, for testing purposes

using UnityEngine;

public class AddItemToInventory : MonoBehaviour
{
    public PlayerInventory inventory;
    public Item testItem;
    public int row, col, count;

    [ContextMenu("Add Test Items to Inventory")]
    private void AddTestItems()
    {
        if (inventory == null || testItem == null)
        {
            Debug.LogWarning("Inventory or testItem not assigned.");
            return;
        }

        inventory.GetSlot(row, col)?.SetItem(testItem, count);
        Debug.Log("Test items added to inventory.");
    }

    [ContextMenu("Clear Inventory")]
    private void ClearInventory()
    {
        if (inventory == null)
        {
            Debug.LogWarning("Inventory not assigned.");
            return;
        }

        inventory.ClearInventory();
        Debug.Log("Inventory cleared.");
    }
}
