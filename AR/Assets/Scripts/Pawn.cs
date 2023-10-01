using UnityEngine;

public class Pawn : MonoBehaviour
{
    public GameManager gameManager;

    void OnMouseDown()
    {
        gameManager.SelectPawn(gameObject);
    }
}