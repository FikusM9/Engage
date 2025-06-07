using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public bool isOn = true;

    public Light pointLight;
    float minIntensity = 0.8f;
    float maxIntensity = 1.2f;
    float minTime = 1f;
    float maxTime = 2f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(isOn) return;
        isOn = true;
        gameObject.SetActive(true);
        StartCoroutine(Flicker());
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isOn) return;
        isOn = false;
        gameObject.SetActive(false);
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
