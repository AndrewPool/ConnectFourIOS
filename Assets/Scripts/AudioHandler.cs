using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
#pragma warning disable CS0108 // Member hides inherited member; inherited member is depreciated
    AudioSource audio;
#pragma warning restore CS0108 // Member hides inherited member; inherited member is depreciated
    // Start is called before the first frame update
    void Start()
    {
        
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audio.isPlaying)
        {
            Destroy(gameObject);
        }
    }
    
}
