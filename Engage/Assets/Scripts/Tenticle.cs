using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Tenticle : MonoBehaviour
{
    public int len;
    public LineRenderer lr;
    public Vector3[] segpos;
    public Transform targetdir;
    public float targetdist;
    public float smoothspeed;
    public float wigglespeed;
    public float wigglemagnitude;
    public float trailspeed;
    public Transform wiggledir;
    public float angle;
    public float minMagnitude;


    private Vector3[] _segspeed;

    private void Start()
    {
        lr.positionCount = len;
        segpos = new Vector3[len];
        _segspeed = new Vector3[len];
        ResetPos();
        
    }



    private void Update()
    {
        wiggledir.localRotation = Quaternion.Euler(0,0, angle + Mathf.Sin(Time.time * wigglespeed) * wigglemagnitude);
        segpos[0] = targetdir.position;
        segpos[0].z = 2;
        for (int i = 1; i < segpos.Length; i++)
        {
            Vector3 targetpos=segpos[i-1] +(segpos[i]-segpos[i-1]).normalized*targetdist;
            segpos[i] = Vector3.SmoothDamp(segpos[i], targetpos , ref _segspeed[i], smoothspeed);
            segpos[i].z = 2;
            print(segpos[i]);
        }
        lr.SetPositions(segpos);
    }

    public void ResetPos()
    {
        segpos[0] = targetdir.position;
        segpos[0].z = 2;
        for (int i = 1; i < len; i++)
        {
            segpos[i] = segpos[i - 1] + new Vector3(0,Time.deltaTime*10,0);
        }
        lr.SetPositions(segpos);
    }
}
