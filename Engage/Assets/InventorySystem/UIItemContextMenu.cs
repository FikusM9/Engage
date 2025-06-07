// This script is responsible for controlling the context menu object and making it responsive

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemContextMenu : MonoBehaviour
{
    [SerializeField] protected Button useButton;
    [SerializeField] protected Button descriptionButton;
    [SerializeField] protected Button closeButton;
    [SerializeField] protected GameObject descriptionText;

    protected UISlot currentSlot;

    public static UIItemContextMenu Instance { get; protected set; }

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
        closeButton.onClick.AddListener(Hide);
        Hide();
    }

    public void ShowDescriptionOnly(string text)
    {
        useButton.gameObject.SetActive(false);
        descriptionButton.gameObject.SetActive(false);
        descriptionText.SetActive(true);

        descriptionText.GetComponent<TMP_Text>().text = text;
    }

    public void ShowDefaultButtons(bool showUse, bool showDescription)
    {
        descriptionText.SetActive(false);
        useButton.gameObject.SetActive(showUse);
        descriptionButton.gameObject.SetActive(showDescription);
    }

    public void Show(Vector2 screenPosition, UISlot slot, bool showUse, bool showDescription)
    {
        if (currentSlot == slot && gameObject.activeSelf) return;

        currentSlot = slot;
        gameObject.SetActive(true);
        transform.position = screenPosition;

        ShowDefaultButtons(showUse, showDescription);

        useButton.onClick.RemoveAllListeners();
        useButton.onClick.AddListener(OnUseClicked);

        descriptionButton.onClick.RemoveAllListeners();
        descriptionButton.onClick.AddListener(OnDescriptionClicked);
    }

    public void Hide()
    {
        currentSlot = null;
        ShowDefaultButtons(true, true);
        gameObject.SetActive(false);
    }

    private void OnUseClicked()
    {
        currentSlot.UseItem();
        Hide();
    }

    private void OnDescriptionClicked()
    {
        ShowDescriptionOnly(currentSlot.BoundItem.Description);
    }
}
