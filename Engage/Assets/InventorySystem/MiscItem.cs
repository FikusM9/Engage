// Sample class for misc-type items

using UnityEngine;

[CreateAssetMenu(fileName = "MiscItem", menuName = "Inventory/MiscItem")]
public class MiscItem : Item
{
    public const int MISC_STACK_SIZE = 1;

    private void OnEnable()
    {
        itemType = ItemType.Misc;
        maxStackSize = MISC_STACK_SIZE;
    }
}
