using System.Collections.Generic;

public class Team
{
    public PawnSymbol Symbol { get; private set; }
    public List<Pawn> Pawns { get; private set; } 

    public Team(PawnSymbol symbol)
    {
        Symbol = symbol;
        Pawns = new List<Pawn>();
    }

}