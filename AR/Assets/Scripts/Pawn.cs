using UnityEngine;

public class Pawn : MonoBehaviour
{
    public enum Team
    {
        Red,
        Blue,
        Yellow,
        Green
    }

    public Team team;
    public GameManager gameManager;

    void OnMouseDown()
    {
        gameManager.SelectPawn(gameObject);
    }
}