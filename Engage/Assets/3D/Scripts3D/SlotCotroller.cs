using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotCotroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private Image itemImage;
    
    protected bool isFree = true;

    void Start()
    {
        
    }

    public void AddItem(Sprite image, bool isNew)
    {
        if (!isNew)
        {
            TextMeshProUGUI targetCount = itemCountText ?? transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            
            int count = int.Parse(targetCount.text);
            count++;
            targetCount.text = count.ToString();

        }
        else
        {
            Image targetImage = itemImage ?? transform.GetChild(0).GetComponent<Image>();
            targetImage.sprite = image;
            targetImage.gameObject.SetActive(true);
            TextMeshProUGUI itemCount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            itemCount.text = "1";
            isFree = false;
        }
        
    }

    public void RemoveItem()
    {
        TextMeshProUGUI itemCount = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (itemCount.text == "") return;
        int count = int.Parse(itemCount.text);
        if(--count <= 0)
        {
            Image slotImage = transform.GetChild(0).GetComponent<Image>();
            slotImage.sprite = null;
            slotImage.gameObject.SetActive(false);
            itemCount.text = "";
            itemCount.gameObject.SetActive(false);
            isFree = true;
        }
        else
        {
            itemCount.text = count.ToString();
        }
        
    }

}
