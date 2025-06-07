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
        healthText.text = CurrentHealth.ToString();
        switchText.text = SwitchTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
