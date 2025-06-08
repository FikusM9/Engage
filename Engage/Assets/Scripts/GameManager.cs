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
    public static int Health = 3;
    public bool is2d = true;
    public static float Timer;
    public float time2d = 10f;
    public float time3d = 10f;
    public Camera myCamera;
    public GameObject scene2d;
    public GameObject scene3d;
    public GameObject player3d;
    public GameObject player2d;
    public Vector3 camera3dOffset = new Vector3(0f, 2.6f, 0f);
    public static int CurrentCheckPointIndex;
    private Transform _chekpoints2d;
    private Transform _chekpoints3d;

    private void Awake()
    {
        _chekpoints2d = GameObject.Find("CheckPoints2d").transform;
        _chekpoints3d = GameObject.Find("CheckPoints3d").transform;
        Start3d();
        //MyCamera = FindObjectOfType<Camera>();
    }
   

    private void Start2d()
    {
        OnStart2d?.Invoke();
        Timer=time2d;
        is2d = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Camera.main.nearClipPlane + 10f; 
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouseWorld.z = 0f;
        scene2d.SetActive(true);
        player2d.transform.position = _chekpoints2d.GetChild(CurrentCheckPointIndex).position;
        myCamera.GetComponent<CameraFollow>().enabled = true;
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
        Cursor.visible = false;
        scene3d.SetActive(true);
        player3d.transform.position = _chekpoints3d.GetChild(CurrentCheckPointIndex).position + new Vector3(0f,10.6f,0f);
        myCamera.GetComponent<CameraFollow>().enabled = false;
        myCamera.transform.position = player3d.transform.position;
        myCamera.transform.rotation = Quaternion.identity;
        myCamera.transform.SetParent(player3d.transform);
        myCamera.orthographic = false;
        myCamera.transform.localPosition = camera3dOffset;
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
