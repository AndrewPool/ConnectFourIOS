using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private bool on = true;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Toggle()
    {
        on = !on;
        Toggle(on);
    }
    public void Toggle(bool on)
    {
       
        audioSource.mute = on;        
    }
}
