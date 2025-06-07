// The UISlot class represents the visual UI element of the ItemSlot.
// It implements the necessary event interactions to make the UI function,
// and it achieves this using the ItemSlot class' methods and Unity's interfaces

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TMP_Text countText;    // references to slot's item and count
    protected ItemSlot boundSlot;

    protected static GameObject draggedIcon;
    protected static UISlot draggedFrom;

    public ItemSlot BoundSlot => boundSlot;
    public Item BoundItem => boundSlot?.Item;

    public void Initialize(ItemSlot slot)
    {
        boundSlot = slot;
        boundSlot.OnSlotChanged += Refresh;
        Refresh();
    }

    public void Refresh()
    {
        if (boundSlot != null && !boundSlot.IsEmpty)
        {
            itemImage.sprite = boundSlot.Item.Icon;
            itemImage.enabled = true;
            countText.text = boundSlot.Quantity > 1 ? boundSlot.Quantity.ToString() : "";
        }
        else
        {
            itemImage.enabled = false;
            countText.text = "";
        }
    }

    public void UseItem()
    {
        if (boundSlot.Item is ConsumableItem consumable)
        {
            // IMPORTANT: place the "Player" tag on the player object
            consumable.Consume(GameObject.FindGameObjectWithTag("Player"));
            boundSlot.RemoveItem(1);
        }
    }

    public static void CancelDragging()
    {
        if (draggedIcon != null)
        {
            GameObject.Destroy(draggedIcon);
            draggedIcon = null;
            draggedFrom = null;
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (boundSlot == null || boundSlot.IsEmpty) return;

        draggedFrom = this;
        draggedIcon = new GameObject("DraggedItem");
        Canvas dragCanvas = GameObject.Find("ContextMenuCanvas").GetComponent<Canvas>();
        draggedIcon.transform.SetParent(dragCanvas.transform, false);
        var image = draggedIcon.AddComponent<Image>();
        image.sprite = itemImage.sprite;
        image.raycastTarget = false;

        var canvasGroup = draggedIcon.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (draggedIcon != null)
        {
            draggedIcon.transform.position = eventData.position;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if (draggedIcon != null)
        {
            Destroy(draggedIcon);
        }

        draggedIcon = null;
        draggedFrom = null;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (draggedFrom == null || draggedFrom == this) return;

        ItemSlot fromSlot = draggedFrom.boundSlot;
        ItemSlot toSlot = boundSlot;

        // Try to stack items
        if (!toSlot.IsEmpty && toSlot.Item == fromSlot.Item)
        {
            int remaining = boundSlot.AddItem(fromSlot.Item, fromSlot.Quantity);
            if (remaining == 0) {
                fromSlot.ClearSlot();
            } else if (remaining > 0)
            {
                fromSlot.RemoveItem(fromSlot.Quantity - remaining);
            }
        }
        else
        {
            // Swap slots
            ItemSlot temp = toSlot.TakeAll();
            toSlot.SetItem(fromSlot.Item, fromSlot.Quantity);

            if (temp != null && !temp.IsEmpty)
            {
                fromSlot.SetItem(temp.Item, temp.Quantity);
            }
            else
            {
                fromSlot.ClearSlot();
            }
        }

        if (draggedFrom is UIEquipmentSlot uiEquipmentSlot)
        {
            uiEquipmentSlot.Loadout.RecalculateStats();
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && boundSlot != null && !boundSlot.IsEmpty)
        {
            bool showUse = boundSlot.Item is ConsumableItem;
            bool showDescription = true;

            UIItemContextMenu.Instance.Show(eventData.position, this, showUse, showDescription);
        }
    }
}
