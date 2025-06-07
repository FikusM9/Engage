// This is a test class used to simulate player stats on a smaller scale,
// showcasing the interactive abilities of several parts of the inventory system

using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] protected int attack = 0;
    [SerializeField] protected int defense = 0;

    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected int currentHealth;

    public int Attack => attack;
    public int Defense => defense;
    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    public void SetAttack(int attack)
    {
        this.attack = attack;
    }

    public void SetDefense(int defense)
    {
        this.defense = defense;
    }

    private void Awake()
    {
        currentHealth = maxHealth / 2;
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }
}