using Enums;
using Models;

public interface IUILogicManager
{
    void ShowTurn(TeamEnum teamEnum);
    void PromptForCardScan(TeamEnum teamEnum);
    void DisplayMoveOutcome(bool success, string message);
    void DisplayPawnInEndZone(Pawn pawn);
    void DisplayPlayerCompletion(TeamEnum teamEnum);
    void AnnounceWinningTeam(Partners partners);
    void HighlightPawn(Pawn pawn);  // Visual feedback for selected pawn.
    void FocusCameraOnPawn(Pawn pawn);  // Adjust camera focus to the selected pawn.
    void PromptCardOptionSelection(CardTypeEnum cardType);
    void ResetUIForNewTurn();
}