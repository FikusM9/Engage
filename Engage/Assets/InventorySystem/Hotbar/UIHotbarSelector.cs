// This script allows for controlling the hotbar selection square using the mouse wheel.

using UnityEngine;
using UnityEngine.UI;

public class UIHotbarSelector : MonoBehaviour
{
    [SerializeField] private RectTransform selectorRect;
    [SerializeField] private float spacing = 117.5f;    // distance between slots
    [SerializeField] private float padding = 66f;   // x-padding
    [SerializeField] private int maxSlots = 6;

    private int currentIndex = 0;
    
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            int oldIndex = currentIndex;
            currentIndex = (oldIndex - 1 + maxSlots) % maxSlots;
            MoveSelector(currentIndex);
        }
        else if (scroll < 0f)
        {
            int oldIndex = currentIndex;
            currentIndex = (oldIndex + 1) % maxSlots;
            MoveSelector(currentIndex);
        }
    }

    public void MoveSelector(int index)
    {
        currentIndex = Mathf.Clamp(index, 0, maxSlots - 1);
        selectorRect.anchoredPosition = new Vector2(index * spacing + padding, selectorRect.anchoredPosition.y);
    }

    public int GetSelectedIndex() => currentIndex;
}
