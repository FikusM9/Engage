// povezati sa dugmetom
// activeTime odredjuje koliko ce dugo trajati signal kada je dugme pritisnuto
// !!! dugme interaktuje sa igracem putem PlayerInteraction skripte koja se vezuje za igraca !!!

using UnityEngine;

public class PhysicalButton : MonoBehaviour, IInteractable
{
    bool isActive = true;
    private float activeTime = 2f;
    private float elapsedSeconds = 0f;
    public void Interact()
    {
        SwitchState();
    }
    private void Update()
    {
        if (isActive && elapsedSeconds<activeTime) { Timer(); }
        else if (elapsedSeconds > activeTime)
        {
            isActive = false;
            elapsedSeconds = 0f;
        }
    }
    private void SwitchState() { isActive = !isActive; }
    private void Timer()
    {
        elapsedSeconds += Time.deltaTime;
    }
    private void DoSomething() { }
}
