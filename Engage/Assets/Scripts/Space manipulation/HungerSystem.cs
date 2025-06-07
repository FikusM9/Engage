using System;
using UnityEngine;

// Implements a simple hunger system with tresholds for satiation and hungriness

public class HungerSystem : MonoBehaviour
{
    [SerializeField] private int maxSatiation = 100;
    [SerializeField] private int currentSatiation = 100;
    [SerializeField] private int hungerThreshold = 50;  // Arbitrary number

    public int MaxSatiation => maxSatiation;
    public int CurrentSatiation => currentSatiation;

    public Action OnStarve; // Can be set externally
    public Action OnFull;   // Can be set externally

    void Awake()
    {
        currentSatiation = Mathf.Clamp(currentSatiation, 0, maxSatiation);
    }

    public void IncreaseSatiation(int amount)
    {
        currentSatiation = Mathf.Min(currentSatiation + amount, maxSatiation);
        if (currentSatiation == maxSatiation) OnFull?.Invoke();
    }

    public void DecreaseSatiation(int amount)
    {
        currentSatiation = Mathf.Max(currentSatiation - amount, 0);
        if (currentSatiation == 0) OnStarve?.Invoke();
    }

    public bool IsHungry()
    {
        return currentSatiation <= hungerThreshold;
    }
}
