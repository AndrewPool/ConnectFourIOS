using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSlot : MonoBehaviour
{
    //config param
    [SerializeField] PlaySpace[] playSpaces;


    //refrence cache
    CXController gameController;

    //state variables and properties
    public int Identity {  get; private set;  }


    //getter for playSpaces
    public PlaySpace GetPlaySpace(int index)
    {
        return playSpaces[index];
    }

    //what heppens when you hit this button
    public void HitButton()
    {
        Debug.Log("button hit "+Identity);
        gameController.SelectColumn(Identity);
    }

    //set identity
    public void SetIdentity(int identity, CXController cXController)
    {
        gameController = cXController;
        Identity = identity;
    }

    


    private void Start()
    {
       
        
    }
}
