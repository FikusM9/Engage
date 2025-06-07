using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    public static event System.Action OnStart2d;
    public static event System.Action OnStart3d;
    private bool _is2d = true;
    public static float Timer;
    public float time2d = 10f;
    public float time3d = 10f;

    private void Awake()
    {
        Start2d();
    }

    private void Start2d()
    {
        OnStart2d?.Invoke();
        Timer=time2d;
        _is2d = true;
    }
    
    private void Start3d()
    {
        OnStart3d?.Invoke();
        Timer=time3d;
        _is2d = false;
    }

    private void FixedUpdate()
    {
        Timer-=Time.fixedDeltaTime;
        if (Timer <= 0f)
        {
            if(_is2d) Start3d();
            else Start2d();
        }
    }
}
