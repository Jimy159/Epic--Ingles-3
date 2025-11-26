using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAmbient : MonoBehaviour
{
    public AudioSource audio;
    private void OnTriggerEnter(Collider other)
    {
        audio.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        audio.Stop();
    }
}
