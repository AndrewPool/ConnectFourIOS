
using System.Collections.Generic;

public class CXGameModel
{

  //  public CXGameModel clone() {
    //return
      //      }
    private IDictionary<string, Location> travelTokenVectors = new Dictionary<string, Location>();


    public IDictionary<int, int> ValidMovesWithGrades//= new Dictionary<int, VectorGrade>
    {
        get;
        private set;
    }


    public bool[,] GameState
    {
        get;
        private set;
    }

    public int turn = 0;

    //shows what locations are relevent in gameState
    public int[] NumberTokensInColumn
    {
        get;
        private set;
    }

    public string Winner
    {
        get;
        private set;
    }

    public bool Over
    {
        get; private set;
    } = false;

    public bool CurrentPlayer
    {
        get; private set;
    }


    public static readonly int numberRows = 6;
    public static readonly int numberColumns = 7;
    public static readonly bool RED = true;
    public static readonly bool BLACK = false;
    public static readonly bool COMPUTERPLAYER = false;



    public CXGameModel()
    {

        NumberTokensInColumn = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        GameState = new bool[numberRows, numberColumns];
        CurrentPlayer = RED;
        travelTokenVectors.Add("Horizontal", new Location(0, 1));
        travelTokenVectors.Add("Diagnol1", new Location(1, 1));
        travelTokenVectors.Add("Diagnol2", new Location(1, -1));
        travelTokenVectors.Add("Vertical", new Location(1, 0));
        ValidMovesWithGrades = new Dictionary<int, int>();
        Winner = "none";
        SetValidMovesWithGrades();

    }

    /// <summary>
    /// Makes the move with column.
    /// </summary>
    /// <returns><c>true</c>, if move with column was made, <c>false</c> otherwise.</returns>
    /// <param name="column">Column.</param>
    public bool MakeMoveWithColumn(int column)
    {

        if (ValidMovesWithGrades.ContainsKey(column) && !Over)
        {

            turn++;
            int row = NumberTokensInColumn[column];
            GameState[row, column] = CurrentPlayer;

            NumberTokensInColumn[column] = NumberTokensInColumn[column] + 1;
            CurrentPlayer = !CurrentPlayer;

            if (ValidMovesWithGrades[column] == 200)
            {
                Over = true;
                if (CurrentPlayer == true)
                { Winner = "red"; }
                else { Winner = "black"; }

            }
            if (turn == 6 * 7)
            {
                Over = true;
            }

            SetValidMovesWithGrades();
            return true;
        }
        //else
        return false;
    }


    private bool SpaceEmpty(Location location)
    {
        if (location.row >= NumberTokensInColumn[location.column]) return true;

        return false;

    }
    private void SetValidMovesWithGrades()
    {
        ValidMovesWithGrades = new Dictionary<int, int>();
        for (int i = 0; i < numberColumns; i++)
        {
            if (ValidateMove(i))
            {
                ValidMovesWithGrades.Add(i, AggregateScoreFromVectorGrades(i));
            }
        }
    }
    private int AggregateScoreFromVectorGrades(int column)
    {
        bool player = CurrentPlayer;

        Location rootLocation = new Location(NumberTokensInColumn[column], column);

        Location aboveLocation = new Location(rootLocation.row + 1, column);

        Location aboveTwiceLocation = new Location(aboveLocation.row + 1, column);

        VectorGrade grade = BestVectorGradeForColumn(rootLocation, player, true);
        //do not check below because board state is not updated 
        VectorGrade gradeAbove = BestVectorGradeForColumn(aboveLocation, player, false);
        //one more time.
        VectorGrade gradeAboveTwice = BestVectorGradeForColumn(aboveTwiceLocation, player, false);

        player = !player;

        VectorGrade opponentGrade = BestVectorGradeForColumn(rootLocation, player, true);
        //do not check below because board state is not updated
        VectorGrade opponentGradeAbove = BestVectorGradeForColumn(aboveLocation, player, false);
        //if (opponentGradeAbove.lengthOfChain >= 4) {
        //Console.WriteLine(opponentGradeAbove.lengthOfChain + " length of chain"); }

        //this is a must make play do not bother with math!
        if (grade.lengthOfChain >= 4) return 200;
        if (opponentGrade.lengthOfChain >= 4) return 199;
        if (opponentGradeAbove.lengthOfChain >= 4) return -200;

        //this is another win move, remember we already checked and guarded against oppenent wins above
        if(gradeAbove.lengthOfChain >= 4 && gradeAboveTwice.lengthOfChain >= 4)
        {
            return 198;
        }

        //else take the sum, of your score with the difference of the opponents play
        //Console.WriteLine("myscore");
        int sumOfScores = grade.score + (opponentGrade.score - opponentGradeAbove.score);
        //deweight score if the over spot is a win for you
        if (gradeAbove.lengthOfChain >= 4)
        {
            sumOfScores -= 40;
        }
        return sumOfScores;
    }


    private VectorGrade BestVectorGradeForColumn(Location location, bool hypotheticalCurrentPlayer, bool checkBelow)
    {
        VectorGrade[] gradeDetails = new VectorGrade[4];

        int insertionIndex = 0;
        //score for
        foreach (KeyValuePair<string, Location> direction in travelTokenVectors)
        {
            Location vectorToGrade = direction.Value;


            //do not check below if the direction is vertical and checkbelow is false
            //check below is false when you are evaluating future moves above current row
            VectorGrade vectorGrade = GradeVector(
                rootLocation: location,
                direction: vectorToGrade,
                player: hypotheticalCurrentPlayer,
                checkBelow: !(direction.Key == "Vertical" && checkBelow == false)
                );

            gradeDetails[insertionIndex] = vectorGrade;

            insertionIndex++;


        }
        VectorGrade best = gradeDetails[0];
        for (int i = 1; i < gradeDetails.Length; i++)
        {
            if (best.score < gradeDetails[i].score) best = gradeDetails[i];
        }
        return best;
    }

    //vector grade below 

    private VectorGrade GradeVector(Location rootLocation, Location direction, bool player, bool checkBelow)
    {

        bool hypotheticalCurrentPlayer = player;
        bool continousChain = true;
        int maxLengthInChain = 1;
        int lengthOfChain = 1;
        bool openEndedOtherWay = false;
        bool openEndedOneWay = false;
        Location currentLocation = rootLocation;
        //this little bit could be recursive, but it doesn't play super nice with readonly VectorGrades
        for (int i = 0; i < 3; i++)
        {
            currentLocation.row += direction.row;
            currentLocation.column += direction.column;
            if (ValidateSpace(currentLocation))
            {

                //if space is unoccupied
                if (SpaceEmpty(currentLocation))
                {
                    continousChain = false;
                    openEndedOneWay = true;
                    maxLengthInChain++;
                }
                //if space is yours
                else if (GameState[currentLocation.row, currentLocation.column] == hypotheticalCurrentPlayer)
                {
                    maxLengthInChain++;
                    if (continousChain) { lengthOfChain++; }

                }
                //if space is opponents
                else
                {
                    //this will break out of loop
                    i = 3;

                }
            }
            else { i = 3; }

        }

        //and the other way
        if (checkBelow == true)
        {
            continousChain = true;
            Location reversedDirection = direction.VectorReversed();
            currentLocation = rootLocation;
            for (int i = 0; i < 3; i++)
            {

                currentLocation.row += reversedDirection.row;
                currentLocation.column += reversedDirection.column;

                if (ValidateSpace(currentLocation))
                {


                    //if space is unoccupied
                    if (SpaceEmpty(currentLocation))
                    {

                        continousChain = false;
                        openEndedOtherWay = true;
                        maxLengthInChain++;
                    }
                    //if space is yours
                    else if (GameState[currentLocation.row, currentLocation.column] == hypotheticalCurrentPlayer)
                    {

                        maxLengthInChain++;
                        if (continousChain) { lengthOfChain++; }
                    }
                    //if space is opponents
                    else
                    {
                        //this will break out of loop
                        i = 3;
                    }
                }
                else { i = 3; }

            }
        }

        bool openEndedBothWays = false;
        if (openEndedOneWay && openEndedOtherWay) openEndedBothWays = true;

        VectorGrade returnGrade = new VectorGrade(lengthOfChain, maxLengthInChain, openEndedBothWays);

        return returnGrade;
    }




    private bool ValidateMove(int indexForColumn)
    {
        return (NumberTokensInColumn[indexForColumn] < numberRows);
    }


    //checks to see if space is on board 
    private bool ValidateSpace(Location location)
    {
        if (location.row < 0 ||
            location.column < 0 ||
            location.row >= numberRows ||
            location.column >= numberColumns)
        { return false; }
        //else
        return true;
    }
}


//detailed stats on the move
struct VectorGrade
{
    public readonly int lengthOfChain;
    public readonly int maxLengthOfChain;
    public readonly bool openEndedBothWays;
    public readonly int score;

    public VectorGrade(int lengthOfChain, int maxLengthOfChain, bool openEndedBothWays)
    {
        score = SetScore(lengthOfChain, maxLengthOfChain, openEndedBothWays);
        this.openEndedBothWays = openEndedBothWays;
        this.lengthOfChain = lengthOfChain;
        this.maxLengthOfChain = maxLengthOfChain;
    }
    private static int SetScore(int length, int maxLength, bool openEnded)
    {
        if (maxLength < 4) return 0;
        if (length >= 4) return 200;
        if (length >= 3 && openEnded) return 100;

        int returnScore = length * 10 + maxLength;

        if (openEnded) returnScore += 5;

        return returnScore;
    }
}


//location on board
struct Location
{
    public int row;
    public int column;
    public Location(int row, int column)
    {
        this.row = row;
        this.column = column;
    }


    public Location VectorReversed()
    {
      
        return new Location(-row, -column);
    }
}

