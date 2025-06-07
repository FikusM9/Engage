// Sample class for material-type items

using UnityEngine;

[CreateAssetMenu(fileName = "MaterialItem", menuName = "Inventory/MaterialItem")]
public class MaterialItem : Item
{
    public const int MATERIAL_STACK_SIZE = 20;

    private void OnEnable()
    {
        itemType = ItemType.Material;
        maxStackSize = MATERIAL_STACK_SIZE;
    }
}
