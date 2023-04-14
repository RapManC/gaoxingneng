using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCamer : MonoBehaviour
{
    public GameObject oneCam;
    public GameObject twoCam;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public void SetCam()
    {
        oneCam.SetActive(false);
        twoCam.SetActive(true);
    }

    public  void SetSound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.volume = 0.45f;
    }
}
