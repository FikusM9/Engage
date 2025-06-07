// The ArmorItem class is used to make equippable items which the player can wear
// Currently the only stat those items have is defense, but this can be expanded

using UnityEngine;

public enum ArmorType
{
    Helmet,
    Chestplate,
    Leggings,
    Boots,
    Shield,
    Misc
}

[CreateAssetMenu(fileName = "NewArmor", menuName = "Inventory/Armor Item")]
public class ArmorItem : Item
{
    [SerializeField] protected int defense;
    [SerializeField] protected ArmorType armorType;
    public int Defense => defense;
    public ArmorType ArmorType => armorType;

    public const int ARMOR_STACK_SIZE = 1;

    private void OnEnable()
    {
        itemType = ItemType.Armor;
        maxStackSize = ARMOR_STACK_SIZE;
    }
}
