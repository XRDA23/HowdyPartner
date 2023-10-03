public interface IUILogicManager
{
    void ShowTurn(Team team);
    void PromptForCardScan(Team team);
    void DisplayMoveOutcome(bool success, string message);
    void DisplayPawnInEndZone(Pawn pawn);
    void DisplayPlayerCompletion(Team team);
    void AnnounceWinningTeam(Partners partners);
    void HighlightPawn(Pawn pawn);  // Visual feedback for selected pawn.
    void FocusCameraOnPawn(Pawn pawn);  // Adjust camera focus to the selected pawn.
    void PromptCardOptionSelection(CardTypeEnum cardType);
    void ResetUIForNewTurn();
}