using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAmbient : MonoBehaviour
{
    public AudioSource audio;
    private bool one = true;
    private void OnTriggerEnter(Collider other)
    {
            audio.Play();
            one = false;
    }

    private void OnTriggerExit(Collider other)
    {
        audio.Stop();
    }
}
