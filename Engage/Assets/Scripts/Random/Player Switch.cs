//prikacite skriptu za neki game object npr. empty game object
// za player1 i player2 postavite igrace izmedju kojih zelite da se menjate, po default-u prvo je aktivan player1
// bitno je da action map koji se koristi za player1 i player2 nisu isti
//u update-u se input ocitava pomocu starog input systema

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwitcher : MonoBehaviour
{
    [Header("Player objects:")]
    public PlayerInput player1;
    private string player1ActionMap = "Player1";
    public PlayerInput player2;
    private string player2ActionMap = "Player2";
    private bool isPlayer1Active = true;  

    void Start()
    {
        SwitchToPlayer1();
    }

    void Update()
    {
        // u ovom primeru izmena se desava klikom tab dugmeta
        if (Keyboard.current.tabKey.wasPressedThisFrame) { SwitchPlayerControl(); }
    }

    void SwitchPlayerControl()
    {
        if (isPlayer1Active) { SwitchToPlayer2(); }
        else { SwitchToPlayer1(); }
    }

    void SwitchToPlayer1()
    {
        player1.enabled = true;
        player2.enabled = false;

        player1.SwitchCurrentActionMap(player1ActionMap);
        isPlayer1Active = true;

    }

    void SwitchToPlayer2()
    {
        player1.enabled = false;
        player2.enabled = true;

        player2.SwitchCurrentActionMap(player2ActionMap);
        isPlayer1Active = false;

    }
}
