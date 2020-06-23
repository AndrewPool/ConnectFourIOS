using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    private SoundPlayer soundPlayer;
    [SerializeField] Sprite[] emitterImages;
    private bool landed = false;
    Vector2 newPosition;
    private static float speed = 800f;

    public void SetNewPosition(Vector2 pos)
    {
        Vector2 lossyScale = GetComponent<RectTransform>().lossyScale;
        newPosition = new Vector2(pos.x * lossyScale.x, pos.y *lossyScale.y);
    }

   public void SetSoundPlayer(SoundPlayer sp)
    {
        soundPlayer = sp;
    }

    // Update is called once per frame
    void Update()
    {

        if (!landed)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
           
            if (new Vector2(transform.position.x, transform.position.y) == newPosition)
            {
                Debug.Log("happened");
                landed = true;
                soundPlayer.PlayTokenContact();
            }
        }

    }


}
