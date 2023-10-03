namespace Board
{
    public class BoardLogic
    {
        private int noOfPlayers { get; set; } // we have home and end bases as many as we have players - easier scalability
        private HomeBase[] homeBases;
        private EndBase[] endBases;

        public BoardLogic(int noOfPlayers)
        {
            this.noOfPlayers = noOfPlayers;
            homeBases = new HomeBase[noOfPlayers];
            endBases = new EndBase[noOfPlayers];
        }
    
        //What methods will the GameLogic call on this class? - need to be added
    }
}

