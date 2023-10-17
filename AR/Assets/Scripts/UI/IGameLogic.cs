using Enums;
using Models;

namespace UI
{
    public interface IGameLogic
    {
        // Starts the game and determines the first player to play.
        void StartGame();

        // Process a single-action card move. 
        BoardPosition PlayCard(Pawn chosenPawn, CardActionEnum cardAction);
    }
}