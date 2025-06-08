using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public uint CurrentHealth = 5;
    [SerializeField] private TextMeshProUGUI healthText;
    
    public int SwitchTime = 15;
    [SerializeField] private TextMeshProUGUI switchText;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(CurrentHealth);
        UpdateTime(SwitchTime);
    }

    public void UpdateHealth(uint newHealth)
    {
        CurrentHealth = newHealth;
        healthText.text = CurrentHealth.ToString();
    }

    public void UpdateTime(int newTime)
    {
        SwitchTime = (int)newTime;
        switchText.text = SwitchTime.ToString();
    }
}
