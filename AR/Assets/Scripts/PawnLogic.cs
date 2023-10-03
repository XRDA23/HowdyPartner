using UnityEngine;

public class PawnLogic : MonoBehaviour
{
    public enum PawnColor
    {
        Red,
        Blue,
        Yellow,
        Green
    }

    public PawnColor pawnColor;
    public GameManager gameManager;

    void OnMouseDown()
    {
        gameManager.SelectPawn(gameObject);
    }
}