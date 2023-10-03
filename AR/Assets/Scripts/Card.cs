using System.Collections.Generic;

public class Card
{
    public CardTypeEnum CardType { get; private set; }
    public List<Option> AvailableOptions { get; private set; }

    public Card(CardTypeEnum cardType)
    {
        CardType = cardType;
        AvailableOptions = GetOptionsForCard(cardType);
    }

    private List<Option> GetOptionsForCard(CardTypeEnum cardType)
    {
        // Based on the card type, return the possible options.
        switch (cardType)
        {
            case CardTypeEnum.OneOrFourteen:
                return new List<Option>
                {
                    new Option(OptionTypeEnum.Move1Space, "Move 1 space"),
                    new Option(OptionTypeEnum.Move14Spaces, "Move 14 spaces")
                };
            // ... other card types with options ...
            default:
                return new List<Option>();
        }
    }

}