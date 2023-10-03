using UnityEngine;

public class Pawn : MonoBehaviour 
{
    public Team Owner { get; private set; }
    public PawnSymbol Symbol => Owner.Symbol;  // Derived from the owner's symbol.

    public Pawn(Team owner)
    {
        Owner = owner;
    }

}