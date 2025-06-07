// implementirana lista objekata koji su nagazili na plocu, ukupan broj njih i bool da li je bilo sta na ploci

using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private Vector3 boxSize = new Vector3(1f, 0.2f, 1f);
    private Vector3 heightVector = new Vector3(0f, 0.5001f, 0f);
    private Vector3 boxCords;
    public int pressedby = 0;
    private int tempCounter = 0;
    private bool isActive = false;
    void Update()
    {
        boxCords = transform.position + heightVector;
        Collider[] hits = Physics.OverlapBox(boxCords, boxSize, transform.rotation);
        tempCounter = 0;
        foreach (Collider col in hits) { tempCounter++;}
        pressedby = tempCounter;
        isActive = (pressedby != 0) ? true : false;
        Debug.Log(isActive);
    }

    //funkcija za debuging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(boxCords, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize * 2);
    }
}