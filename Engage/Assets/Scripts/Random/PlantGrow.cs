using UnityEngine;

public class PlantGrow : AgeableObject
{
    [SerializeField] private bool isReadyToGrow = false; // Is the plant ready to grow?
    [SerializeField] private int rewardPoints; // How many points the player gets upon harvesting the plant
    [SerializeField] private GameObject player; // Reference to the player GameObject
    [SerializeField] private float interactDistance; // How far away from the plant the player can interact with it
    [SerializeField] private GameObject waterPromptUI; // Child canvas object of the plant object (A canvas object should be created as a child object of the plant and should then be put into this slot)

    private bool isPlayerInRange = false;

   /* new void Start()
    {
        growthSpeed = 70;
        interactDistance = 3f;
        rewardPoints = 10;
        base.Start();

        if (waterPromptUI != null)
        {
            // Position the watering UI above the plant
            waterPromptUI.transform.position = transform.position + Vector3.up * 2f;
            waterPromptUI.SetActive(false);
        }
    }
   */
    /*new void Update()
    {
        CheckPlayerDistance();

        // Handle growth
        if (isReadyToGrow)
        {
            isReadyToGrow = false;
            isPlayerInRange = false;
            Age();
        }

        // Water with E key
        if (isPlayerInRange && !isReadyToGrow && Input.GetKeyDown(KeyCode.E))
        {
            WaterPlant();
        }

        UpdateVisual();
    }*/

    private void CheckPlayerDistance()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool currentlyInRange = distance <= interactDistance;

        if (currentlyInRange && !isPlayerInRange)
        {
            // Player just entered range
            isPlayerInRange = true;
            if (!isReadyToGrow && waterPromptUI != null) waterPromptUI.SetActive(true);
        }
        else if (!currentlyInRange && isPlayerInRange)
        {
            // Player just left range
            isPlayerInRange = false;
            if (waterPromptUI != null) waterPromptUI.SetActive(false);
        }
    }

    private void WaterPlant()
    {
        isReadyToGrow = true;
        if (waterPromptUI != null) waterPromptUI.SetActive(false);
    }

    protected void OnDeath()
    {
        PlayerPoints.Instance?.AddPoints(rewardPoints);
        Destroy(gameObject);
    }
}