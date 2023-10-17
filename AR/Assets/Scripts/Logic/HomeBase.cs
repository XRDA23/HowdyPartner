using Enums;
using Models;

namespace Logic
{
    public class HomeBase
    {
        private TeamEnum teamEnum;
        private int noOfPawns; //Do we want to store the pawns in here or just the number?
    
        public HomeBase() {}
        
        public void Leave(Pawn pawn)
        {
            noOfPawns--;
        }

        public void Return(Pawn pawn)
        {
            noOfPawns++;
        }
    }
}

