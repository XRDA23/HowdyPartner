public interface IUILogicManager
{
    void ShowTurn(TeamLia team);
    void PromptForCardScan(TeamLia team);
    void DisplayMoveOutcome(bool success, string message);
    void DisplayPawnInEndZone(PawnLia pawn);
    void DisplayPlayerCompletion(TeamLia team);
    void AnnounceWinningTeam(Partners partners);
    void HighlightPawn(PawnLia pawn);  // Visual feedback for selected pawn.
    void FocusCameraOnPawn(PawnLia pawn);  // Adjust camera focus to the selected pawn.
    void PromptCardOptionSelection(CardTypeEnum cardType);
    void ResetUIForNewTurn();
}