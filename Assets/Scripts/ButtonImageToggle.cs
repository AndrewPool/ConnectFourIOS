using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageToggle : MonoBehaviour
{

    //config params
#pragma warning disable CS0649
    //#pragma warning disable
    [SerializeField] Sprite trueImg;
    [SerializeField] Sprite falseImg;
#pragma warning restore CS0649

    //refrence cache
    Image image;

    //State, probably could be internal, debug is a thing
    public bool On { get; private set; }

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void Toggle()
    {
        Toggle(!On);
    }

    public void Toggle(bool on)
    {
        On = on;
        if (On)
        {
            image.sprite = trueImg;
        }
        else
        {
            image.sprite = falseImg;
        }
    }



}
