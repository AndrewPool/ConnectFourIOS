using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{

    [SerializeField] Sprite[] emitterImages;

    Vector3 newPosition;
    private static float speed = 1000f;
 
    public void SetNewPosition(Vector3 pos)
    {
        Vector3 lossyScale = GetComponent<RectTransform>().lossyScale;
        newPosition = new Vector3(pos.x * lossyScale.x, pos.y *lossyScale.y, pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("something is happening");
         transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            //yield return WaitForFixedUpdate;
        
    }


}
