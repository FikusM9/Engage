// ConsumableItem defines items which can be consumed by the player,
// having differing effects on them.
// These effects are defined and implemented by classes derived from ItemEffect

using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItem", menuName = "Inventory/ConsumableItem")]
public class ConsumableItem : Item
{
    public const int CONSUMABLE_STACK_SIZE = 10;

    [SerializeField] protected ItemEffect effect;

    private void OnEnable()
    {
        itemType = ItemType.Consumable;
        maxStackSize = CONSUMABLE_STACK_SIZE;
    }

    public void Consume(GameObject user)
    {
        effect?.Apply(user);
    }
}
