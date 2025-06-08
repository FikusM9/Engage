using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController : MonoBehaviour
{
    public int maxItems = 6;
    public GameObject slotsParent;
    public GameObject hotBar;

    List<int> inventory;
    int numOfItems = 0;

    UIHotbarSelector hotBarSelector;

    void Start()
    {
        inventory = new List<int>();
        hotBarSelector = hotBar.GetComponent<UIHotbarSelector>();
        
        hotBarSelector.MoveSelector(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RemoveItem(hotBarSelector.GetSelectedIndex() );
        }
        
    }
     
    public void AddItem(Sprite icon, int itemId)
    {
        if (inventory.Contains(itemId))
        {
            int slotId = inventory.IndexOf(itemId);
            SlotCotroller slot = slotsParent.transform.GetChild(slotId).GetComponent<SlotCotroller>();
            slot.AddItem(icon, false);
        }
        else
        {
            if (numOfItems >= maxItems) return;
            SlotCotroller slot = slotsParent.transform.GetChild(numOfItems).GetComponent<SlotCotroller>();
            slot.AddItem(icon, true);
            numOfItems++;
            inventory.Add(itemId);
        }        
    }

    public void RemoveItem(int slotId)
    {
        if (slotId < 0 || slotId >= maxItems) return;
        SlotCotroller slot = slotsParent.transform.GetChild(slotId).GetComponent<SlotCotroller>();
        slot.RemoveItem();
    }

}
