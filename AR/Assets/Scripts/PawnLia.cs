using UnityEngine;

public class PawnLia : MonoBehaviour 
{
    public TeamLia Owner { get; private set; }
    public PawnSymbolEnum Symbol => Owner.Symbol;  // Derived from the owner's symbol.

    public PawnLia(TeamLia owner)
    {
        Owner = owner;
    }

}