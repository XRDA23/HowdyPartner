using Board;

public interface IUILogicManager
{
    void ShowTurn(Team team);
    void PromptForCardScan(Team team);
    void DisplayMoveOutcome(bool success, string message);
    void DisplayPawnInEndZone(PawnLogic pawn);
    void DisplayPlayerCompletion(Team team);
    void AnnounceWinningTeam(Partners partners);
    void HighlightPawn(PawnLogic pawn);  // Visual feedback for selected pawn.
    void FocusCameraOnPawn(PawnLogic pawn);  // Adjust camera focus to the selected pawn.
    void PromptCardOptionSelection(CardTypeEnum cardType);
    void ResetUIForNewTurn();
}