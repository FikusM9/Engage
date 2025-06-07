// Similar to the ArmorItem class, the WeaponItem class is used to make equippable
// weapons for the player.
// Currently the only stat those items have is damage, but this can be expanded

using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Inventory/WeaponItem")]
public class WeaponItem : Item
{
    [SerializeField] protected int damage;
    public const int WEAPON_STACK_SIZE = 1;

    public int Attack => damage;

    private void OnEnable()
    {
        itemType = ItemType.Weapon;
        maxStackSize = WEAPON_STACK_SIZE;
    }

}
