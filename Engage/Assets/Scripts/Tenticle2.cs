using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenticle2 : MonoBehaviour
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

    private Vector3[] segspeed;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.widthMultiplier = 1;
        lr.positionCount = len;
        print(lr);
        segpos = new Vector3[len];
        segspeed = new Vector3[len];
        ResetPos();

    }

    

    private void FixedUpdate()
    {
        wiggledir.localRotation = Quaternion.Euler(0, 0, angle+Mathf.Sin(Time.time * wigglespeed) * wigglemagnitude);
        segpos[0] = targetdir.position;
        segpos[0].z = 2;
        for (int i = 1; i < segpos.Length; i++)
        {
            segpos[i] = Vector3.SmoothDamp(segpos[i], segpos[i-1]+targetdir.right*targetdist, ref segspeed[i], smoothspeed+i/trailspeed);
            segpos[i].z = 2;
        }
        lr.SetPositions(segpos);
    }

  
    public void ResetPos()
    {
        segpos[0] = targetdir.position;
        segpos[0].z = 2;
        for (int i = 1; i < len; i++)
        {
            segpos[i] = segpos[i - 1] + targetdir.right * targetdist;
        }
        lr.SetPositions(segpos);
    }
}