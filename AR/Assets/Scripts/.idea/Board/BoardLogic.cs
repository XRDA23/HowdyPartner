namespace DefaultNamespace;

public class BoardLogic
{
    private int noOfPlayers; // we have home and end bases as many as we have players - easier scalability
    private HomeBase[noOfPlayers] homeBases;
    private EndBase[noOfPlayers] endBases

    public BoardLogic(int noOfPlayers)
    {
        this.noOfPlayers = noOfPlayers;
    }
    
    //What methods will the GameLogic call on this class? - need to be added
}