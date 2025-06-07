//vezuje se za vrata dok igrac koji interreaguje sa vratima treba da ima vezanu skriptu "Player Interaction"

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Door properties")]
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private Transform childTranform; // pivot point

    private Vector3 pivotPoint;
    private Vector3 RotationVector = -Vector3.up; //rotation direction
    bool isRotating = false;

    private void Awake() { pivotPoint = childTranform.transform.position; }

    private void Update() { InteractWithDoor(); }

    public void Interact()
    {
        isRotating = !isRotating;
        RotationVector = (isRotating) ? -RotationVector : RotationVector;
    }
    void InteractWithDoor()
    {
        if (isRotating)
        {
            transform.RotateAround(pivotPoint, RotationVector, rotationSpeed * Time.deltaTime);
            if (transform.eulerAngles.y > 300)
            {
                isRotating = false;
                transform.eulerAngles = Vector3.zero;
                return;
            }
            if (transform.eulerAngles.y >= 90)
            {
                isRotating = false;
                transform.eulerAngles = new Vector3(0f, 90f, 0f);
            }
        }
    }
}
