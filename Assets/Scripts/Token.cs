using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    private SoundPlayer soundPlayer;

    //[SerializeField] Sprite[] emitterImages;

    private bool landed = false;
    Vector2 newPosition;
    private static float speed = 800f;

    public void SetNewPosition(Vector2 pos)
    {
        Vector2 lossyScale = GetComponent<RectTransform>().lossyScale;
        newPosition = new Vector2(pos.x * lossyScale.x, pos.y *lossyScale.y);
    }

    private void Start()
    {
        soundPlayer = FindObjectOfType<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!landed)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);


            var selfLoc = new Vector2(transform.position.x, transform.position.y);
            Debug.Log(selfLoc);
            Debug.Log(newPosition);
            var magnitude = Vector2.SqrMagnitude(selfLoc-newPosition);
            if (magnitude < 1.0f)
            {
                Debug.Log("happened");
                landed = true;
                soundPlayer.PlayTokenContact();
            }
        }

    }


}
