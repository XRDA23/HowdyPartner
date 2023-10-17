using System.Collections.Generic;
using System.Linq;
using Enums;
using Models;
using UI;
using UnityEngine;

namespace Logic
{
    public class GameLogic : MonoBehaviour, IGameLogic
    {
        [SerializeField] private GameObject redPawnPrefab;
        [SerializeField] private GameObject bluePawnPrefab;
        [SerializeField] private GameObject yellowPawnPrefab;
        [SerializeField] private GameObject greenPawnPrefab;
        [SerializeField] private GameObject boardPrefab;
        [SerializeField] private Renderer boardRenderer;
        private Renderer pawnRenderer;
        private List<Pawn> pawns = new();
        private Pawn selectedPawn;
        private BoardLogic boardLogic;

        public void StartGame()
        {
            // Spawn the board prefab 
            SpawnBoardPrefab();

            // Get pawn renderer
            pawnRenderer = GetComponent<Renderer>();

            // Spawn the pawns
            SpawnPawns();
        }

        public BoardPosition PlayCard(Pawn chosenPawn, CardActionEnum cardAction)
        {
            return boardLogic.HandleCardPlayed(chosenPawn, cardAction);
        }

        void SpawnBoardPrefab()
        {
            // Setting the position of board
            Vector3 boardPosition = new Vector3(0, -7f, 9f); 

            // Instantiate the board prefab
            GameObject boardInstance = Instantiate(boardPrefab, boardPosition, Quaternion.identity);
        
            // Adjust the rotation of the board
            boardInstance.transform.rotation = Quaternion.Euler(0, -180, 0);
           
            // Get board renderer
            boardRenderer = boardInstance.GetComponent<Renderer>();
        }
    
        public void ToggleBoardVisibility()
        {
            if (boardRenderer != null)
            {
                boardRenderer.enabled = !boardRenderer.enabled;
            }
        }

        public void TogglePawnVisibility(bool isVisible)
        {
            foreach (var pawn in pawns)
            {
                pawn.GetComponentInChildren<Renderer>().enabled = isVisible;
            }
        }
    
        //TODO: Create a bool method that returns whether a pawn is able to move or not 17.10.23
        
        void SpawnPawns()
        {
            GameObject[] redTiles = GameObject.FindGameObjectsWithTag("BaseRedTile");
            GameObject[] blueTiles = GameObject.FindGameObjectsWithTag("BaseBlueTile");
            GameObject[] yellowTiles = GameObject.FindGameObjectsWithTag("BaseYellowTile");
            GameObject[] greenTiles = GameObject.FindGameObjectsWithTag("BaseGreenTile");

            SpawnPawnsForColor(redTiles, TeamEnum.RedOrHeart);
            SpawnPawnsForColor(blueTiles, TeamEnum.BlueOrWater);
            SpawnPawnsForColor(yellowTiles, TeamEnum.YellowOrStar);
            SpawnPawnsForColor(greenTiles, TeamEnum.GreenOrEmerald);
        }

        void SpawnPawnsForColor(GameObject[] baseTiles, TeamEnum color)
        {
            for (var i = 0; i < 4; i++)
            {
                var pawnPrefab = GetPawnPrefab(color);

                var pawn = Instantiate(pawnPrefab, baseTiles[i].transform.position, Quaternion.identity).GetComponent<Pawn>();
                pawn.gameLogic = this;
                pawn.teamEnum = color;
                
                pawns.Add(pawn);
            }
        }
        
        GameObject GetPawnPrefab(TeamEnum color)
        {
            switch (color)
            {
                case TeamEnum.RedOrHeart:
                    return redPawnPrefab;
                case TeamEnum.BlueOrWater:
                    return bluePawnPrefab;
                case TeamEnum.YellowOrStar:
                    return yellowPawnPrefab;
                case TeamEnum.GreenOrEmerald:
                    return greenPawnPrefab;
                default:
                    return null;
            }
        }
    }
}