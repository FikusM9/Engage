// This class simply populates the slotParent object with slotTemplate
// children once the game is run. It was briefly used during development.

using UnityEngine;

public class PopulateGrid : MonoBehaviour
{
    [SerializeField] private GameObject slotTemplate;
    [SerializeField] private Transform slotParent;
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 9;
    [SerializeField] private float scale = 1.0f;

    void Awake()
    {
        Populate();
    }

    public void Populate()
    {
        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < rows * columns; i++)
        {
            GameObject slot = Instantiate(slotTemplate, slotParent);

            int row = i / columns + 1;
            int col = i % columns + 1;
            slot.name = $"Slot({row},{col})";

            RectTransform rt = slot.GetComponent<RectTransform>();
            rt.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
