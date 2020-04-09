using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : MonoBehaviour
{
    [SerializeField] CXController gameController;

   
    // Update is called once per frame
    void Update()
    {
        //if it is this player and it isn't animating
        if(gameController.game.CurrentPlayer == gameController.computerPlayer && !gameController.animating && gameController.computerPlaying)
        {
            gameController.MakeComputerMove();
        }
    }
}
