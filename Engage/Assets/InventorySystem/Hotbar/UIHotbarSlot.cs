// This derived class simply adds a check to disable mouse interactivity
// while the main inventory window is closed.

using UnityEngine.EventSystems;

public class UIHotbarSlot : UISlot
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!InventoryUIController.InventoryOpen) return;
        base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!InventoryUIController.InventoryOpen) return;
        base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (!InventoryUIController.InventoryOpen) return;
        base.OnEndDrag(eventData);
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (!InventoryUIController.InventoryOpen) return;
        base.OnDrop(eventData);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!InventoryUIController.InventoryOpen) return;
        base.OnPointerClick(eventData);
    }
}
