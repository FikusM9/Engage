// The base Item class for the inventory system
// Contains the basic data for each item
// Items are added through the Unity editor using the derived classes
// Right click -> Create -> Inventory -> ...

using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Misc,
    Material
}

public abstract class Item : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;
    [SerializeField] protected Sprite icon;
    // [SerializeField] protected GameObject prefab;

    private static int idCounter = 0;
    protected int itemId;
    protected ItemType itemType;
    protected int maxStackSize;

    // Public read-only accessors
    public int ItemId => itemId;
    public string ItemName => itemName;
    public string Description => description;
    public Sprite Icon => icon;
    // public GameObject Prefab => prefab;
    public ItemType Type => itemType;
    public int MaxStackSize => maxStackSize;

    private void OnEnable()
    {
        // Automatically generates a unique integer ID if it's not set
        if (itemId == 0)
        {
            itemId = ++idCounter;
        }
    }
}
