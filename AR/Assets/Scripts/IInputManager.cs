using System;
using Board;
using UnityEngine;
public interface IInputManager
{
    event Action<CardTypeEnum> OnCardScanned;
    event Action<PawnLia> OnPawnSelected;
    event Action<Option> OnOptionSelected;
    event Action<Team> OnFoldAction;
    void ScanCard();  // Initiates a card scan.
    void SelectPawn(PawnLia pawn);  // Player chooses a specific pawn.
    void SelectOption(Option option);  // Player chooses an option from a card with multiple options.
    void FoldCards(Team team);  // Player chooses to fold.
}