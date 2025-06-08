using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2d : MonoBehaviour
{
    public GameObject item3d;

    private void OnEnable()
    {
        if (!item3d.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
