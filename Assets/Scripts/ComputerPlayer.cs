using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ComputerPlayer : MonoBehaviour
{
    [SerializeField] CXController gameController;

    [SerializeField] Button computerIcon;

    public bool computerPlayer = false;
    public bool computerPlaying = false;

    public void ToggleComputerPlaying()
    {
        computerPlaying = !computerPlaying;

        SetComputerIcon();
    }

    private void SetComputerIcon()
    {

        if (computerPlaying)
        {
            computerIcon.image.color = Color.green;
        }
        else
        {
            computerIcon.image.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ////if it is this player and it isn't animating
        if (gameController.game.CurrentPlayer == computerPlayer && !gameController.animating && computerPlaying && !gameController.game.Over)//your turn,not animating, and game isn't over
        {

                gameController.MakeComputerMove();

        }
    }
}
