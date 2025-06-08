using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip music2D;
    public AudioClip music3D;

    AudioSource audio;

    bool currentMusic = true;

    float time2D = 0;
    float time3D = 0;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = music3D;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentMusic = !currentMusic;
            int id = currentMusic ? 1 : 0;
            SwitchMusic(id);
        }
    }

    public void SwitchMusic(int id)
    {
        audio.Pause();
        if (id == 0)
        {
            time3D = audio.time;

            audio.clip = music2D;
            audio.time = time2D;
            audio.Play();
        }
        else
        {
            time2D = audio.time;

            audio.clip = music3D;
            audio.time = time3D;
            audio.Play();
        }
    }
}
