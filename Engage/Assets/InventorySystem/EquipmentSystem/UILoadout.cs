// Similarly to UIInventory, the UILoadout class initializes and links all objects
// in the UI's EquipmentPanel with the Loadout ScriptableObject.

using UnityEngine;

public class UILoadout : MonoBehaviour
{
    [SerializeField] protected Loadout loadout;

    [SerializeField] protected GameObject helmetPanel;
    [SerializeField] protected GameObject chestplatePanel;
    [SerializeField] protected GameObject leggingsPanel;
    [SerializeField] protected GameObject bootsPanel;
    [SerializeField] protected GameObject shieldPanel;
    [SerializeField] protected GameObject weaponPanel;

    protected UIEquipmentSlot helmetSlot;
    protected UIEquipmentSlot chestplateSlot;
    protected UIEquipmentSlot leggingsSlot;
    protected UIEquipmentSlot bootsSlot;
    protected UIEquipmentSlot shieldSlot;
    protected UIEquipmentSlot weaponSlot;

    void Start()
    {
        // IMPORTANT: place the "Player" tag on the player object
        PlayerStats stats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        loadout.Initialize(stats);

        helmetSlot = helmetPanel.GetComponent<UIEquipmentSlot>();
        chestplateSlot = chestplatePanel.GetComponent<UIEquipmentSlot>();
        leggingsSlot = leggingsPanel.GetComponent<UIEquipmentSlot>();
        bootsSlot = bootsPanel.GetComponent<UIEquipmentSlot>();
        shieldSlot = shieldPanel.GetComponent<UIEquipmentSlot>();
        weaponSlot = weaponPanel.GetComponent<UIEquipmentSlot>();
       
        helmetSlot.Initialize(loadout.Helmet);
        chestplateSlot.Initialize(loadout.Chestplate);
        leggingsSlot.Initialize(loadout.Leggings);
        bootsSlot.Initialize(loadout.Boots);
        shieldSlot.Initialize(loadout.Shield);
        weaponSlot.Initialize(loadout.Weapon);
    }


    public void RefreshAll()
    {
        helmetSlot.Refresh();
        chestplateSlot.Refresh();
        leggingsSlot.Refresh();
        bootsSlot.Refresh();
        shieldSlot.Refresh();
        weaponSlot.Refresh();
    }
}
