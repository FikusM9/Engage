// The EquipmentSlot class extends the ItemSlot such that it allows for ensuring
// compatibility between the added item and the slot itself

using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    [SerializeField] private ArmorType allowedArmorType;
    [SerializeField] private bool acceptsWeapon;

    public ArmorType AllowedArmorType => allowedArmorType;
    public bool AcceptsWeapon => acceptsWeapon;

    public EquipmentSlot(ArmorType type, bool isWeapon = false) : base()
    {
        allowedArmorType = type;
        acceptsWeapon = isWeapon;
    }

    public bool CanAddItem(Item item)
    {
        if (item == null) return false;

        if (item is ArmorItem armor)
        {
            return armor.ArmorType == allowedArmorType;
        }

        if (item is WeaponItem && acceptsWeapon)
        {
            return true;
        }

        return false;
    }

    public new void AddItem(Item item, int quantity = 1)
    {
        if (!CanAddItem(item)) return;
        base.AddItem(item, quantity);
    }
}
