using Board;
using UnityEngine;

public class PawnLia : MonoBehaviour 
{
    public Team Owner { get; private set; }
    
    public PawnLia(Team owner)
    {
        Owner = owner;
    }

}