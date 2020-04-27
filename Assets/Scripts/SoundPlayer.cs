using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] GameObject TokenContact;
    [SerializeField] GameObject looseSound;
    [SerializeField] GameObject ButtonTap;
    [SerializeField] GameObject winSound;
    private bool audioOn = true;

    public void Toggle()
    {
        audioOn = !audioOn;
    }
    private void Start()
    {
        audioOn = true;
        ButtonImageToggle img = GetComponent<ButtonImageToggle>();
        img.Toggle(audioOn);
    }

    public void PlayButtonTap()
    {
        if (audioOn)
        {
            GameObject newToken = Instantiate<GameObject>(ButtonTap, transform.position, Quaternion.identity);

            newToken.transform.SetParent(transform);

        }
    }

    public void PlayTokenContact()
    {
        if (audioOn)
        {
            GameObject newToken = Instantiate<GameObject>(TokenContact, transform.position, Quaternion.identity);

            newToken.transform.SetParent(transform);

        }
    }



    public void PlayWinSound()
    {


        if (audioOn)
        {
            GameObject newToken = Instantiate<GameObject>(winSound, transform.position, Quaternion.identity);

            newToken.transform.SetParent(transform);
        }
    }

    public void PlayLooseSound()
    {
        if (audioOn)
        {
            GameObject newToken = Instantiate<GameObject>(looseSound, transform.position, Quaternion.identity);

            newToken.transform.SetParent(transform);
        }
    }

}

