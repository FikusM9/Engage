// The Loadout class represents a sample of what a player's loadout could look like,
// and implements it. It has basic stat calculation and is linked to the PlayerStats object.

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLoadout", menuName = "Inventory/PlayerLoadout")]
public class Loadout : ScriptableObject
{
    public EquipmentSlot Helmet { get; private set; }
    public EquipmentSlot Chestplate { get; private set; }
    public EquipmentSlot Leggings { get; private set; }
    public EquipmentSlot Boots { get; private set; }
    public EquipmentSlot Shield { get; private set; }
    public EquipmentSlot Weapon { get; private set; }

    public PlayerStats playerStats { get; private set; }
    public int TotalDefense { get; private set; }
    public int TotalAttack { get; private set; }

    public void Initialize(PlayerStats playerStats)
    {
        this.playerStats = playerStats;

        Helmet = new EquipmentSlot(ArmorType.Helmet);
        Chestplate = new EquipmentSlot(ArmorType.Chestplate);
        Leggings = new EquipmentSlot(ArmorType.Leggings);
        Boots = new EquipmentSlot(ArmorType.Boots);
        Shield = new EquipmentSlot(ArmorType.Shield);
        Weapon = new EquipmentSlot(ArmorType.Misc, true);

        RecalculateStats();
    }

    public void EquipItem(Item item)
    {
        if (item is ArmorItem armor)
        {
            switch (armor.ArmorType)
            {
                case ArmorType.Helmet: Helmet.SetItem(armor); break;
                case ArmorType.Chestplate: Chestplate.SetItem(armor); break;
                case ArmorType.Leggings: Leggings.SetItem(armor); break;
                case ArmorType.Boots: Boots.SetItem(armor); break;
                case ArmorType.Shield: Shield.SetItem(armor); break;
            }
        }
        else if (item is WeaponItem weapon)
        {
            Weapon.SetItem(weapon);
        }

        RecalculateStats();
    }

    public void UnequipItem(Item item)
    {
        if (item == Helmet.Item) Helmet.ClearSlot();
        else if (item == Chestplate.Item) Chestplate.ClearSlot();
        else if (item == Leggings.Item) Leggings.ClearSlot();
        else if (item == Boots.Item) Boots.ClearSlot();
        else if (item == Shield.Item) Shield.ClearSlot();
        else if (item == Weapon.Item) Weapon.ClearSlot();

        RecalculateStats();
    }

    public bool IsSlotTaken(ArmorType type)
    {
        switch (type)
        {
            case ArmorType.Helmet: return Helmet != null && !Helmet.IsEmpty;
            case ArmorType.Chestplate: return Chestplate != null && !Chestplate.IsEmpty;
            case ArmorType.Leggings: return Leggings != null && !Leggings.IsEmpty;
            case ArmorType.Boots: return Boots != null && !Boots.IsEmpty;
            case ArmorType.Shield: return Shield != null && !Shield.IsEmpty;
            case ArmorType.Misc: return Weapon != null && !Weapon.IsEmpty;
            default: return false;
        }
    }

    public void RecalculateStats()
    {
        TotalDefense = 0;
        TotalAttack = 0;

        if (Helmet != null && Helmet.Item != null) TotalDefense += ((ArmorItem)Helmet.Item).Defense;
        if (Chestplate != null && Chestplate.Item != null) TotalDefense += ((ArmorItem)Chestplate.Item).Defense;
        if (Leggings != null && Leggings.Item != null) TotalDefense += ((ArmorItem)Leggings.Item).Defense;
        if (Boots != null && Boots.Item != null) TotalDefense += ((ArmorItem)Boots.Item).Defense;
        if (Shield != null && Shield.Item != null) TotalDefense += ((ArmorItem)Shield.Item).Defense;

        if (Weapon != null && Weapon.Item != null) TotalAttack += ((WeaponItem)Weapon.Item).Attack;

        ApplyToPlayer();
    }

    private void ApplyToPlayer()
    {
        playerStats.SetAttack(TotalAttack);
        playerStats.SetDefense(TotalDefense);
    }
}
