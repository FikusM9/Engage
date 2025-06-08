using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Sprite icon = null;
    public int id = -1;

    public GameObject pickup2d;

    public void Destroy2d()
    {
        Destroy(pickup2d.gameObject);
    }
}
