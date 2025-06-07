using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionAnimation : MonoBehaviour
{
    
    public static TransitionAnimation TransitionInstance;
    private GameManager _gameManager;
    public RawImage noiseBlockerImage;
    public RawImage transitionImage;
    public Camera myCamera;
    public float transitionDuration;
    public Color noiseBlockerStart;
    public Color noiseBlockerEnd;
    public float cameraSizeStart;
    public float cameraSizeEnd;
    private float _transition1Timer;
    private float _transition2Timer;
    private bool _transitionInProgress;
    private CameraShake _cameraShake;


    private void Awake()
    {
        
    }
    

    private void OnEnable()
    {
        GameManager.OnStart2d += Start2d;
        GameManager.OnStart3d += Start3d;
    }
    
    public void Start2d()
    {
        _transitionInProgress = false;
        noiseBlockerImage.enabled = true;
        StartTransition1();
    }
    
    public void Start3d()
    {
        _transitionInProgress = false;
        StartTransition1();
    }
    void Start()
    {
        _cameraShake = FindObjectOfType<CameraShake>();
        _gameManager= FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(GameManager.Timer <=transitionDuration+0.01f && !_transitionInProgress)
        {
            StartTransition2();
        }
        
        noiseBlockerImage.color=Color.Lerp(noiseBlockerStart, noiseBlockerEnd, 
            (_gameManager.time2d-GameManager.Timer)/_gameManager.time2d);
        
        if (_gameManager.is2d)
        {
            myCamera.orthographicSize = Mathf.Lerp(cameraSizeStart, cameraSizeEnd,
                (_gameManager.time2d - GameManager.Timer) / _gameManager.time2d);
        }

        if (_transition1Timer > 0f)
        {
            _transition1Timer -= Time.deltaTime;
            transitionImage.color = Color.Lerp(Color.white, Color.clear, 
                1 - (_transition1Timer / transitionDuration));
        }
        if(_transition2Timer> 0f)
        {
            _transition2Timer -= Time.deltaTime;
            transitionImage.color = Color.Lerp(Color.clear, Color.white, 
                1 - (_transition2Timer / transitionDuration));
            noiseBlockerImage.color=Color.Lerp(noiseBlockerEnd,Color.clear,
                1 - (_transition2Timer / transitionDuration));
        }
    }

    public void StartTransition1()
    {
        print("Trans1");
        _transition1Timer = transitionDuration;
    }
    
    public void StartTransition2()
    {
        print("Trans2");
        _cameraShake.Shake(transitionDuration, 0.2f);
        _transitionInProgress = true;
        _transition2Timer = transitionDuration;
    }
}
