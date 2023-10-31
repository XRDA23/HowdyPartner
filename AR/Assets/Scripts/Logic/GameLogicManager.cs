using System;
using System.Collections.Generic;
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
        private Renderer pawnRenderer; //Is this needed? - Aldís 23.10.23
        public List<Pawn> pawns = new();
        private Pawn selectedPawn;
        [SerializeField]   private BoardLogic boardLogic;

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
            try
            {
                return boardLogic.HandleCardPlayed(chosenPawn, cardAction);
            }
            catch (InvalidOperationException e) //This exception means that the tile already has a pawn on it
            {
                Console.WriteLine(e);
                //TODO: Call a method to check the pawns' colors, move a pawn back to home base if appropriate and return the position - Aldís 23.10.23
                return null;
            }
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
            for (int i = 0; i < pawns.Count; i++)
            {
                if (pawns[i] != null)
                {
                    Renderer[] renderers = pawns[i].GetComponentsInChildren<Renderer>();
                    foreach (Renderer renderer in renderers)
                    {
                        renderer.enabled = isVisible;
                    }
                }
            }
        }
        
    
        //TODO: Create a bool method that returns whether a pawn is able to move or not 17.10.23
        
        void SpawnPawns()
        {
            SpawnPawnsForColor(TeamEnum.BlueOrWater);
            SpawnPawnsForColor(TeamEnum.RedOrHeart);
            SpawnPawnsForColor(TeamEnum.YellowOrStar);
            SpawnPawnsForColor(TeamEnum.GreenOrEmerald);
        }

        void SpawnPawnsForColor(TeamEnum color)
        {
            Debug.Log("spawn pawns");
            var quadrants = EnumToList<QuadrantEnum>();

            var pawnPrefab = GetPawnPrefab(color);

            for (var i = 0; i < 4; i++)
            {
                var quadrant = quadrants[i];
                var position = new BoardPosition(quadrant, TileNumberEnum.HomeBase, boardLogic.GetTileVectorPosition(quadrant, TileNumberEnum.HomeBase));
                var vector = boardLogic.GetTileVectorPosition(quadrant, TileNumberEnum.Heart);
                var pawn = Instantiate(pawnPrefab, vector, Quaternion.identity).GetComponent<Pawn>();

                pawn.gameLogic = this;
                pawn.teamEnum = color;
                pawn.boardPosition = position;

                pawns.Add(pawn);
            }
        }
        
        private GameObject GetPawnPrefab(TeamEnum color)
        {
            return color switch
            {
                TeamEnum.RedOrHeart => redPawnPrefab,
                TeamEnum.BlueOrWater => bluePawnPrefab,
                TeamEnum.YellowOrStar => yellowPawnPrefab,
                TeamEnum.GreenOrEmerald => greenPawnPrefab,
                _ => null
            };
        }

        private List<T> EnumToList<T>()
        {
            return new List<T>((T[])Enum.GetValues(typeof(T)));
        }
    }
}