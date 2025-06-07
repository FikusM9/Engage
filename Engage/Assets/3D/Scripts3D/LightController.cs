using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightController : MonoBehaviour
{

    public bool isOn = true;

    public Light pointLight;
    float minIntensity = 18f;
    float maxIntensity = 20f;
    float minTime = 0.2f;
    float maxTime = 1f;
    void Start()
    {
        if (isOn)
        {
            StartCoroutine(Flicker());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isOn) return;
        if (other.CompareTag("Player3D"))
        {
            isOn = true;
            pointLight.gameObject.SetActive(true);
            StartCoroutine(Flicker());
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player3D"))
        {
            if (!isOn) return;
            isOn = false;
            pointLight.gameObject.SetActive(false);
        }
        
    }

    private IEnumerator Flicker()
    {
        while (isOn)
        {
            pointLight.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }
}
