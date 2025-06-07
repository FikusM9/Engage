// This is a sample ItemEffect that uses the PlayerStats class to Heal the player
// To create custom HealEffects (as well as other ItemEffects), use the Unity editor
// Right click -> Create -> Inventory -> ItemEffects -> HealEffect
// Then you can adjust the healAmount variable of the created ScriptableObject

using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Inventory/ItemEffects/HealEffect")]
public class HealEffect : ItemEffect
{
    [SerializeField] protected int healAmount = 25;

    public override void Apply(GameObject user)
    {
        var health = user.GetComponent<PlayerStats>();
        if (health != null)
        {
            health.Heal(healAmount);
        }
    }
}
