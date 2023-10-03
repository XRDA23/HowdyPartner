public interface IGameLogicManager
{
    // Starts the game and determines the first player to play.
    void StartGame();

    // Ends the game and determines the winning team.
    void EndGame();

    // Process a single-action card move. 
    bool PlayCard(TeamLia team, PawnLia chosenPawn, CardTypeEnum cardType);

    // Process a card move when the card has multiple possible actions.
    bool PlayCardWithOptions(TeamLia team, PawnLia chosenPawn, CardTypeEnum cardType, Option choice);

    // Checks if the current turn is over.
    bool IsCurrentTurnOver();

    // Allows a player to fold their hand.
    void FoldCards(TeamLia team);

    // Checks if a player has folded in the current round.
    bool HasPlayerFolded(TeamLia team);

    // Determines if a player has all their pawns safely in the end zone.
    bool HasPlayerCompleted(TeamLia team);

    // Returns the number of pawns a player has in their end zone.
    int GetPawnsInEndZone(TeamLia team);

    // Determines if the entire team has completed their objectives.
    bool IsTeamPartnersCompleted(Partners partnersTeam);

    // Determines the winning team.
    TeamLia GetWinnerPartnersTeam();

    // Helps a partner using a card.
    bool AssistPartner(TeamLia team, PawnLia chosenPawn, CardTypeEnum cardType);

    // Advances to the next turn or change players.
    void NextTurn();
}