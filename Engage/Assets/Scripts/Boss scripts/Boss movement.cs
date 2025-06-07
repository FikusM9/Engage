// osnovno kretanje bossa kao sto su nasumicno kretanje, dashovanje teleportovanje na unapred odredjene lokacije nasumicno 

// ostalo videti kako uzeti friction od podloge jer ona usporava telo i onda nadograditi dash

using System.ComponentModel;
using UnityEditor.Rendering;
using UnityEngine;

public class Bossmovement : BossBase
{
    bool test = true;
    Rigidbody rg;
    [Header("Cooldown range: ")]
    [SerializeField] private float[] dashLimits = new float[2]; 
    [SerializeField] private float[] teleportationLimits = new float[2];


    [SerializeField] private float dashForce = 5f;
    [SerializeField] private Transform playerCords;
    private float dashTimer;
    [SerializeField] private Transform[] teleportationLocations;
    private int numberOfTPPlaces;

    float tpTimer;
    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
        dashTimer = Random.Range(dashLimits[0], dashLimits[1]);
        tpTimer = Random.Range(teleportationLimits[0], teleportationLimits[1]);

    }
    private void Update()
    {
        DashHandler();
        HandleTeleport();
    }

    private void DashHandler()
    { 
        if (Timer(ref dashTimer, dashLimits[0], dashLimits[1]))
        {
            //DashRandom();
            test = false;
            if (test)
            {
                DashTowardsPlayer(transform, playerCords, 10f); // debugovati ovu funkciju 
            }
            DashRandom();
        }
    }
    private void DashRandom()
    {
        Vector3 dashVector = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f));
        rg.AddForce(dashVector * dashForce, ForceMode.Impulse);
    }
    private void DashTowardsPlayer(Transform bossUnit, Transform playerCords, float dashVelocity)
    {
        //rade
        Vector3 direction = (playerCords.position - bossUnit.position).normalized;
        float distance = Vector3.Distance(bossUnit.position, playerCords.position);
        //ne rade
        float multiplyer = distance/(rg.mass * dashVelocity);
        rg.AddForce(direction * 20, ForceMode.Impulse);
    }
    private void HandleTeleport()
    {
        if (Timer(ref tpTimer, teleportationLimits[0], teleportationLimits[1]))
        {
         TeleportToRandomLocation();   
        }
    }
    private void TeleportToRandomLocation()
    {
        if (teleportationLocations.Length != 0)
        {
            numberOfTPPlaces = teleportationLocations.Length;
            int index = Random.Range(0, numberOfTPPlaces);
            rg.velocity = Vector3.zero;
            transform.position = teleportationLocations[index].position;
        }
    }
    
    
}
