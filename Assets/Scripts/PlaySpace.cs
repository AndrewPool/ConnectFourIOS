using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySpace : MonoBehaviour
{
    //config params
#pragma warning disable CS0649
//#pragma warning disable
    [SerializeField] Sprite playerOneToken;
    [SerializeField] Sprite playerTwoToken;
#pragma warning restore CS0649

    //refrence cache
    Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        if (image == null)
        {
            Debug.Log("still null start");
        }


    }

    public void Clear()
    {
        //Debug.Log("clearing space");
        image.sprite = null;
    }

    public void SetToken(bool player)
    {
        if (image == null)
        {
            Debug.Log("still null");
            image = gameObject.GetComponent<Image>();
        }
       
        image.sprite = player ? playerOneToken : playerTwoToken;
      

    }
}
