using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerPlayer : MonoBehaviour
{
    Controller gameController;

    //[SerializeField] Button computerIcon;

    public bool computerPlayer = false;
    private bool computerPlaying = true;

    public void ToggleComputerPlaying()
    {
        computerPlaying = !computerPlaying;

    }
    private void Start()
    {
        gameController = FindObjectOfType<Controller>();
        computerPlaying = true;
        ButtonImageToggle brain = GetComponent<ButtonImageToggle>();
        brain.Toggle(computerPlaying);
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
