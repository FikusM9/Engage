// tajmeri koji se koriste u skriptama vezanim sa boss-em

using UnityEngine;

public class BossBase : MonoBehaviour
{
    /// <summary>
    ///  Checks if the timer has reached zero
    /// </summary>
    /// <param name="timer"> reference to the timer from which the time is subtracted </param>
    /// <param name="lowerLimit"> lower limit for random method </param>
    /// <param name="upperLimit"> upper limit for random method </param>
    /// <returns> whether the timer has reached zero </returns>
    protected bool Timer(ref float timer, float lowerLimit, float upperLimit)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return false;
        }
        else if (timer <= 0)
        {
            timer = Random.Range(lowerLimit, upperLimit);
            return true;
        }
        else { Debug.LogError("GRESKA"); return false; }
    }
    protected bool Timer(ref float timer)
    {
        timer -= Time.deltaTime;
        if (timer <= 0) { return true; }
        else { return false; }
        
    }
    
}
