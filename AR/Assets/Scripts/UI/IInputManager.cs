using System;
using Enums;
using Models;

namespace UI
{
    public interface IInputManager
    {
        event Action<CardTypeEnum> OnCardScanned;
        event Action<Pawn> OnPawnSelected;
        event Action<Option> OnOptionSelected;
        event Action<TeamEnum> OnFoldAction;
        void ScanCard();  // Initiates a card scan.
        void SelectPawn(Pawn pawn);  // Player chooses a specific pawn.
        void SelectOption(Option option);  // Player chooses an option from a card with multiple options.
        void FoldCards(TeamEnum teamEnum);  // Player chooses to fold.
    }
}