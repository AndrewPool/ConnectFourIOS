using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CXController : MonoBehaviour
{
    //----------------config paramaters-----------------
#pragma warning disable 0649
    [SerializeField] DropSlot[] dropSlots;
    [SerializeField] Image nextToken;
    [SerializeField] GameObject player1Token;
    [SerializeField] GameObject player2Token;
    [SerializeField] GameObject tokenParent;
    //  [SerializeField] Text winText;
#pragma warning restore 0649

    //cached values

    Canvas canvas;


    //UI elements


    public bool animating = false;



    private IList<Token> piecesOnBoard = new List<Token>();


    //State Variables and Properties
    public CXGameModel game = new CXGameModel();


    //public void PlayerSelectColumn(int column)
    //{
    //    if(game.CurrentPlayer == !computerPlayer || computerPlaying == false)
    //    {
    //        SelectColumn(column);
    //    }
    //}


    //game loop is in here once player picks a slot
    public void SelectColumn(int column)
    {
        if (!game.Over)
        {
            animating = true;
            Debug.Log("selcting column " + column);

            if (game.MakeMoveWithColumn(column))
            {
                //adds a token of current player to board.
                AddPlayerTokenToBoard(column);
                StartCoroutine(WaitForAnimation());


            }

            SetNextPlayerToken();

        }


    }
    
    //TODO
    private void SetNextPlayerToken()
    {
        //nextToken.Clear();
        //nextToken.SetToken(!game.CurrentPlayer);
    }


    public void MakeComputerMove()
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

        
        GameObject newToken = Instantiate<GameObject>(TokenForCurrentPlayer(), transform.position,Quaternion.identity);

        
        //Vector2 size = dropSlots[0].transform.lossyScale;
        ////size.x = 1000f;
        ////size.x = (size.x * dropSlots[0].transform.localScale.x) / dropSlots[0].transform.lossyScale.x;
        ////size.y = dropSlots[0].transform.localScale.y;
        //newToken.transform.localScale = size;

        newToken.transform.SetParent(tokenParent.transform);
        newToken.transform.position = dropSlots[column].GetPlaySpace(6).transform.position;

        Token token= newToken.GetComponent<Token>();
        token.SetNewPosition(dropSlots[column].GetPlaySpace(game.NumberTokensInColumn[column] - 1).transform.position);
        newToken.GetComponent<RectTransform>().localScale = tokenParent.transform.lossyScale;
        piecesOnBoard.Add(token);
       
       
    }
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1);
        animating = false;
        yield return null;
    }
    private GameObject TokenForCurrentPlayer()
    {
        if (game.CurrentPlayer)
        {
            return player1Token;
        }
        //else
        return player2Token;
    }

    public void NewGame()
    {

       // winText.text = "";
        game = new CXGameModel();
        

        foreach(Token t in piecesOnBoard)
        {
            Destroy(t.gameObject);
        }
        piecesOnBoard = new List<Token>();
        SetNextPlayerToken();
    }


    // Start is called before the first frame update
    void Start()
    {
      //  winText.text = "";
        Debug.Log("start cxcontroller");
        for (int i = 0; i < dropSlots.Length; i++)
        {
            dropSlots[i].SetIdentity(i, this);
        }
        // NewGame();
     

        canvas = FindObjectOfType<Canvas>();
    }

}