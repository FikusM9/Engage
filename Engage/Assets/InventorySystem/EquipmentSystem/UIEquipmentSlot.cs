// The UIEquipmentSlot extends the basic UISlot allowing it to
// fit the purpose of representing the player's loadout in the UI.
// It checks for item compatibility and ensures UI slots interact correctly.

using UnityEngine;

public class UIEquipmentSlot : UISlot
{
    [SerializeField] protected Loadout loadout;
    public Loadout Loadout => loadout;

    public override void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (draggedFrom == null || draggedFrom == this || loadout == null) return;

        var fromItem = draggedFrom.BoundItem;
        if (fromItem == null) return;

        if (fromItem is ArmorItem armorItem)
        {
            // Misc type used only for testing
            if (armorItem.ArmorType == ArmorType.Misc) return;
        }

        if (boundSlot is not EquipmentSlot eqSlot) return;
        if (!eqSlot.CanAddItem(fromItem)) return;

        if (loadout.IsSlotTaken(eqSlot.AllowedArmorType))
        {
            var previousItem = eqSlot.Item;

            loadout.UnequipItem(previousItem);
            draggedFrom.BoundSlot.SetItem(previousItem);
        }
        else
        {
            draggedFrom.BoundSlot.ClearSlot();
        }

        loadout.EquipItem(fromItem);

        draggedFrom.Refresh();
        Refresh();
    }
}
