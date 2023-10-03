namespace Board
{
    
public class EndBase
{
    private Team team;
    /**
     * There are 5 tiles in the end base
     * 0 -> arrow
     * 1-4 -> colored ones (4 beeing the last that you can reach)
     * A pawn entering will go 0-1-2-3-4
     */
    private Pawn[] tiles = new Pawn[5];

    public EndBase(Team team)
    {
        this.team = team;
    }

    public void Enter(Pawn pawn, int noOfMoves)
    {
        // should we double check if the pawn is for this specific team or would this be handeled by another class?
        
        int firstPositionOccupied = FirstPositionOccupied(0);

        switch (firstPositionOccupied)
        {
            case -1: // no pawns are in the end base
                if (noOfMoves <= 5) // pawn has to go forward less than 5 tiles so it doesn't need to turn back
                {
                    tiles[noOfMoves] = pawn;
                }
                else // pawn has to go forward more than 5 tiles so it was to turn back at the top
                {
                    int position = (noOfMoves - 5) % 4; // double check math (I think it's fine but I wrote it so...)
                    tiles[position] = pawn;
                }
                break;
            case 0: // pawn is on the arrow 
                //pawns cannot enter -- what should happened? or is this situation handeled in another class?
                break;
            case 1: // pawn is on the first place in the base (a new pawn can be place only on the arrow)
                if (noOfMoves == 1)
                {
                    tiles[0] = pawn;
                }
                else
                {
                    //pawn cannot enter -- what should happened? or is this situation handeled in another class?
                }
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                int pos= (noOfMoves - firstPositionOccupied - 1) % (firstPositionOccupied - 2); // double check math (I think it's fine but I wrote it so...)
                tiles[pos] = pawn;
                break;
        }
    }

    public void GoFurther(Pawn pawn, int noOfMoves)
    {
        int positionOfPawn = FindPositionOfThePawnInEndBase(pawn);
        if (positionOfPawn == -1)
        {
            Enter(pawn, noOfMoves);
            return;
        }
        
        int firstPositionOccupied = FirstPositionOccupied(positionOfPawn);
        
        //TODO do the fucking mathh -_- 
    }

    /**
     * Returns first position occupied after the startPosition
     * startPosition = 0 for a pawn that enters for the first time
     * startPostionn = positionOfPawn+1 for a pawn that is already in the endBase
     */
    private int FirstPositionOccupied(int startPosition)
    {
        for (int i = startPosition; i < 5; i++)
        {
            if (tiles[i] != null)
                return i;
        }

        return -1; // no pawns are in the next positions
    }

    /**
     * Returns the position of the pawn in endBase
     * This method can be deleted if the pawn/boarPosition has also taken into account the endBase
     */
    private int FindPositionOfThePawnInEndBase(Pawn pawn)
    {
        for (int i = 0; i < 5; i++)
        {
            if (tiles[i] == pawn)
                return i;
        }

        return -1; // pawn is not in the end base
    }
}
}
