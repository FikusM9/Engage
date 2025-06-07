using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    
    
    public static GameManager Instance;
    public static event System.Action OnStart2d;
    public static event System.Action OnStart3d;
    public static int CurrentBulletCount = 10;
    public static int CurrentStopWatchCount = 10;
    public bool is2d = true;
    public static float Timer;
    public float time2d = 10f;
    public float time3d = 10f;
    public Camera myCamera;
    public GameObject scene2d;
    public GameObject scene3d;
    public GameObject player3d;
    public GameObject player2d;

    private void Awake()
    {
        Start2d();
        //MyCamera = FindObjectOfType<Camera>();
    }
   

    private void Start2d()
    {
        OnStart2d?.Invoke();
        Timer=time2d;
        is2d = true;
        scene2d.SetActive(true);
        myCamera.transform.position = new Vector3(player2d.transform.position.x, player2d.transform.position.y, -5f);
        myCamera.transform.rotation = Quaternion.identity;
        myCamera.transform.SetParent(scene2d.transform);
        myCamera.orthographic = true;
        scene3d.SetActive(false);
    }
    
    private void Start3d()
    {
        OnStart3d?.Invoke();
        Timer=time3d;
        is2d = false;
        scene3d.SetActive(true);
        myCamera.transform.position = player3d.transform.position;
        myCamera.transform.rotation = Quaternion.identity;
        myCamera.transform.SetParent(player3d.transform);
        myCamera.orthographic = false;
        scene2d.SetActive(false);
    }

    private void FixedUpdate()
    {
        Timer-=Time.fixedDeltaTime;
        if (Timer <= 0f)
        {
            if(is2d) Start3d();
            else Start2d();
        }
    }
}
