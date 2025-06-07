// The PlayerInventory is a matrix of ItemSlots which stores all inventory information
// Multiple independent inventories can be added via
// Right click -> Create -> Inventory -> PlayerInventory

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Inventory/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{
    [SerializeField] protected int rows = 3;
    [SerializeField] protected int columns = 4;
    [SerializeField] protected ItemSlot[,] slots;

    public int Rows => rows;
    public int Columns => columns;

    private void OnEnable()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        slots = new ItemSlot[rows, columns];
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                slots[r, c] = new ItemSlot();
            }
        }
    }

    public void ClearSlot(int row, int column)
    {
        if (IsValidPosition(row, column))
        {
            slots[row, column].ClearSlot();
        }
    }

    public void ClearInventory()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                slots[r, c].ClearSlot();
            }
        }
    }

    private bool IsValidPosition(int row, int column)
    {
        return row >= 0 && row < rows && column >= 0 && column < columns;
    }

    public ItemSlot GetSlot(int row, int column)
    {
        if (IsValidPosition(row, column)) return slots[row, column];
        return null;
    }
}
