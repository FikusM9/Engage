using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    public static PlayerPoints Instance { get; private set; }
    [SerializeField] private int points = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddPoints(int amount)
    {
        points += amount;
    }

    public void SetPoints(int amount)
    {
        points = amount;
    }

    public int GetPoints()
    {
        return points;
    }
}
