using Enums;
using Logic;
using UnityEngine;

namespace Models
{
    public class Pawn : MonoBehaviour
    {
        public TeamEnum teamEnum;
        public GameLogicManager gameLogicManager;

        void OnMouseDown()
        {
            gameLogicManager.SelectPawn(gameObject);
        }
    }
}