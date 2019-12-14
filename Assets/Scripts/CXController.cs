using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CXController : MonoBehaviour
{
    //----------------config paramaters-----------------
#pragma warning disable 0649
    [SerializeField] DropSlot[] dropSlots;
    [SerializeField] Button computerIcon;
    [SerializeField] PlaySpace nextToken;
    [SerializeField] Text winText;
#pragma warning restore 0649
    //UI elements



    //State Variables and Properties
    private CXGameModel game = new CXGameModel();

    private bool computerPlayer = false;
    private bool computerPlaying = false;
    public void toggleComputerPlaying()
    {
        computerPlaying = !computerPlaying;

        SetComputerIcon();
    }

    //game loop is in here once player picks a slot
    public void SelectColumn(int column) {
        Debug.Log("selcting column " + column);

        if (game.MakeMoveWithColumn(column))
        {
            //adds a token of current player to board.
            AddPlayerTokenToBoard(column);
            
        }
        if (!game.Over) {
        if (game.CurrentPlayer == computerPlayer && computerPlaying)
        {
            MakeComputerMove();
            }
        }
        SetNextPlayerToken();
        if (game.Over)
        {
            winText.text = "!!*:D*!!";
        }
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

    private void SetNextPlayerToken()
    {
        nextToken.Clear();
        nextToken.SetToken(!game.CurrentPlayer);
    }
    public override string ToString()
    {
        return "CXController" + game.ToString();
    }

    private void MakeComputerMove()
    {
        int bestMoveValue = -1000;
        int bestMove = -1;
        foreach (KeyValuePair<int, int> move in game.ValidMovesWithGrades)
        {
            if (move.Value > bestMoveValue)
            {
                bestMove = move.Key;
                bestMoveValue = move.Value;
            }
        }
        game.MakeMoveWithColumn(bestMove);
        AddPlayerTokenToBoard(bestMove);
    }

    private void AddPlayerTokenToBoard(int column)
    {

        dropSlots[column].GetPlaySpace(game.NumberTokensInColumn[column] - 1).SetToken(game.CurrentPlayer);

    }


    public void NewGame()
    {

        //gives each slot a fool proof identity.
        winText.text = "";
        game = new CXGameModel();
        foreach(DropSlot slot in dropSlots)
        {
            slot.Reset();
        }
        SetNextPlayerToken();
    }


    // Start is called before the first frame update
    void Start()
    {
        winText.text = "";
        Debug.Log("start cxcontroller");
        for (int i = 0; i < dropSlots.Length; i++)
        {
            dropSlots[i].SetIdentity(i, this);
        }
      // NewGame();
        SetComputerIcon();
        
    }

}
