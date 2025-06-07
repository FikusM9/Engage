using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotCotroller : MonoBehaviour
{
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddItem(Sprite image, bool isNew)
    {
        if (!isNew)
        {
            TextMeshProUGUI itemCount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            int count = int.Parse(itemCount.text);
            count++;
            itemCount.text = count.ToString();

        }
        else
        {
            Image slotImage = transform.GetChild(0).GetComponent<Image>();
            slotImage.sprite = image;
            slotImage.gameObject.SetActive(true);
            TextMeshProUGUI itemCount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            itemCount.text = "1";
        }
        
    }

    public void RemoveItem()
    {
        Image slotImage = transform.GetChild(0).GetComponent<Image>();
        slotImage.sprite = null;
        slotImage.gameObject.SetActive(false);
    }

}
