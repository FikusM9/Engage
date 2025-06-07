//pokretna traka funkcionise kao npr one u fabrikama

using UnityEngine;
using UnityEngine.InputSystem.HID;

public class ConveyorBelt : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(5, 1, 35);
    [SerializeField] private LayerMask detectionMask; // u slucaju da zelis da neke stvari ne mogu da se pomeraju na pokretnoj traci napravi masku sa svim objektima koji mogu

    void Update()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, boxSize, transform.rotation, detectionMask);

        foreach (Collider col in hits)
        {
            Transform obj = col.transform;
            obj.position += transform.forward * 10f * Time.deltaTime;
        }
    }

    /*  za debug
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize * 2); // Multiply by 2 because OverlapBox uses half extents
    }
    */
}