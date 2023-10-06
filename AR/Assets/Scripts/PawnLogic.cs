using Board;
using UnityEngine;

public class PawnLogic : MonoBehaviour
{
    public Team team;
    public GameManager gameManager;

    void OnMouseDown()
    {
        gameManager.SelectPawn(gameObject);
    }
}