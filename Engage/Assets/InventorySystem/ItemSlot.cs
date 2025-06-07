// The ItemSlot class implements the logic behind the inventory slots,
// supplying the necessary operations for slot interaction

using System;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    [SerializeField] protected Item item;
    [SerializeField] protected int quantity;

    public bool IsEmpty => item == null;
    public bool IsFull => item != null && quantity >= item.MaxStackSize;

    public Item Item => item;
    public int Quantity => quantity;

    public event Action OnSlotChanged;

    public ItemSlot()
    {
        if (item != null && quantity == 0)
        {
            quantity = 1;
        }
    }

    public void SetItem(Item newItem, int newQuantity = 1)
    {
        if (newQuantity <= 0)
        {
            return;
        }

        item = newItem;
        quantity = newQuantity;
        OnSlotChanged?.Invoke();
    }

    /// <summary>
    /// Attempts to add the specified amount of an item to the slot.
    /// Returns the number of items that could not be added, or -1 if the operation is invalid
    /// </summary>
    public int AddItem(Item newItem, int amount)
    {
        if (newItem == null || amount < 0 || IsFull) return -1;

        if (item == null)
        {
            item = newItem;
            quantity = Mathf.Min(amount, item.MaxStackSize);
            OnSlotChanged?.Invoke();
            return amount - quantity;
        }
        else if (item == newItem && quantity < item.MaxStackSize)
        {
            int added = Mathf.Min(amount, item.MaxStackSize - quantity);
            quantity += added;
            OnSlotChanged?.Invoke();
            return amount - added;
        }

        return -1;
    }

    public ItemSlot TakeAll()
    {
        if (item == null || quantity <= 0) return null;

        ItemSlot takenSlot = new ItemSlot();
        takenSlot.SetItem(item, quantity);

        ClearSlot();
        return takenSlot;
    }

    public int RemoveItem(int amount)
    {
        if (item == null || amount <= 0) return 0;

        int removed = Mathf.Min(quantity, amount);
        quantity -= removed;

        if (quantity <= 0) ClearSlot();
        else OnSlotChanged?.Invoke();

        return removed;
    }

    public void ClearSlot()
    {
        item = null;
        quantity = 0;
        OnSlotChanged?.Invoke();
    }

}
