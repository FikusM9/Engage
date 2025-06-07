// This script allows you to freeze or unfreeze the player's movement.
// Calling ToggleFreeze will enable or disable the player's movement.

using UnityEngine;
using UnityEngine.UI;

public class FrozenPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MonoBehaviour controllPlayer;
    private bool isFrozen;
    void Start()
    {
        isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        // You can specify a condition here to call ToggleFreeze.
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFreeze();
        }*/
    }
    public static void FreezePlayer(MonoBehaviour controllPlayer)
    {
        if (controllPlayer != null)
        {
            controllPlayer.enabled = false;
        }
    }
    public static void UnfreezePlayer(MonoBehaviour controllPlayer)
    {
        if (controllPlayer != null)
        {
            controllPlayer.enabled = true;
        }
    }
    private void  FreezeToggle(MonoBehaviour controllPlayer) //when the ToggleFreeze function is called, the player's ability to move will be toggled (enabled or disabled).
    {
        if (isFrozen)
        {
            UnfreezePlayer(controllPlayer);
        }
        else
        {
            FreezePlayer(controllPlayer);
        }
        isFrozen = !isFrozen;
    }

}