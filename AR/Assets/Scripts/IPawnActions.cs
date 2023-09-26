using UnityEngine;

public interface IPawnActions
{
    void MoveForward(GameObject pawn, int nrOfSteps);
    
    void MoveBackward(GameObject pawn, int nrOfSteps);
    
    void SwitchPositions(GameObject pawn1, GameObject pawn2);
    
    /// <summary>
    /// When a pawn is killed and needs to be sent back to home base.
    /// </summary>
    /// <param name="pawn"></param>
    void MoveHome(GameObject pawn);
    
    /// <summary>
    /// When a heart card is played, a pawn moves to the spawn point and becomes playable.
    /// </summary>
    /// <param name="pawn"></param>
    void GetPawnFromHome(GameObject pawn);
}
