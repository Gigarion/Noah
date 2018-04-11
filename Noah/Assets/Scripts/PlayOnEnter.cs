using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayOnEnter : MonoBehaviour {
    public AudioSource m_audio;

    public void OnTriggerEnter(Collider other)
    {
        m_audio.Play();
    }
}
