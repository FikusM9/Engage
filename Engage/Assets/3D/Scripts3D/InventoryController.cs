using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public int maxItems = 6;
    public GameObject slotsParent;


    List<int> inventory;

    int numOfItems = 0;

    void Start()
    {
        inventory = new List<int>();
    }

    void Update()
    {
        
    }
     
    public void AddItem(Sprite icon, int itemId)
    {
        if (inventory.Contains(itemId))
        {
            Debug.Log(itemId);
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

    public void RemoveItem(int id)
    {

    }

}
