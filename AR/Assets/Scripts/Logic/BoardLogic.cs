using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Models;
using UnityEngine;

namespace Logic
{
    public class BoardLogic:MonoBehaviour
    {
        // we have home and end bases as many as we have players - easier scalability
        private int noOfPlayers { get; set; }

        public BoardLogic(int noOfPlayers)
        {
            this.noOfPlayers = noOfPlayers;
        }

        public BoardPosition HandleCardPlayed(Pawn pawn, CardActionEnum cardAction)
        {
            switch (cardAction)
            {
                case CardActionEnum.Switch:
                    Console.WriteLine("Switch");
                    // OnSwitchPlayed(pawn);
                    return null;
                case CardActionEnum.One:
                    Console.WriteLine("1");
                    return OnMoveForwardPlayed(1, pawn);
                case CardActionEnum.Two:
                    Console.WriteLine("2");
                    return OnMoveForwardPlayed(2, pawn);
                case CardActionEnum.Three:
                    Console.WriteLine("3");
                    return OnMoveForwardPlayed(3, pawn);
                case CardActionEnum.FourBackwards:
                    Console.WriteLine("4 backwards");
                    return OnMoveBackwardsPlayed(4, pawn);
                case CardActionEnum.Five:
                    Console.WriteLine("5");
                    return OnMoveForwardPlayed(5, pawn);
                case CardActionEnum.Six:
                    Console.WriteLine("6");
                    return OnMoveForwardPlayed(6, pawn);
                case CardActionEnum.SevenTimesOne:
                    Console.WriteLine("7x1");
                    //TODO: Need to get an array of pawns to move one step, if the same pawn moves multiple times it needs to be duplicated in the array - Aldís 09.10.23
                    // foreach (var pawn in pawns)
                    // {
                    //     OnMoveForwardPlayed(1, pawn);
                    // }
                    return null;
                case CardActionEnum.Eight:
                    Console.WriteLine("8");
                    return OnMoveForwardPlayed(8, pawn);
                case CardActionEnum.Nine:
                    Console.WriteLine("9");
                    return OnMoveForwardPlayed(9, pawn);
                case CardActionEnum.Ten:
                    Console.WriteLine("10");
                    return OnMoveForwardPlayed(10, pawn);
                case CardActionEnum.Heart:
                    Console.WriteLine("Heart");
                    return GetPawnFromHome(pawn);
                case CardActionEnum.Twelve:
                    Console.WriteLine("12");
                    return OnMoveForwardPlayed(12, pawn);
                case CardActionEnum.Thirteen:
                    Console.WriteLine("13");
                    //TODO: Display prompt to decide which to use - Aldís 24.09.23
                    return OnMoveForwardPlayed(13, pawn);
                case CardActionEnum.Fourteen:
                    Console.WriteLine("14");
                    return OnMoveForwardPlayed(14, pawn);
                default:
                    Console.WriteLine($"Unknown card: {cardAction}");
                    return null;
            }
        }

        private void OnSwitchPlayed(Pawn[] pawns)
        {
            var pos0 = pawns[0].boardPosition;
            var pos1 = pawns[1].boardPosition;
            pawns[0].boardPosition = new BoardPosition(pos1.quadrantEnum, pos1.tileNo, GetTileVectorPosition(pos1.quadrantEnum, pos1.tileNo));
            pawns[1].boardPosition = pos0;
        }

        private bool IsInHomeBase(Pawn pawn)
        {
            return pawn.boardPosition.tileNo == TileNumberEnum.HomeBase;
        }
        
        private BoardPosition OnMoveForwardPlayed(int nrOfSteps, Pawn pawn)
        {
            if (IsInHomeBase(pawn))
                return pawn.boardPosition;
            
            try
            {
                pawn.boardPosition = GetNewBoardPositionForward(nrOfSteps, pawn);
                return pawn.boardPosition;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                //TODO: Call a method to move a pawn back to home base - Aldís 23.10.23
            }
            
            return pawn.boardPosition;
        }

        private BoardPosition GetNewBoardPositionForward(int nrOfSteps, Pawn pawn)
        {
            var quadrant = pawn.boardPosition.quadrantEnum;
            TileNumberEnum tileNumber;
            var totalSteps = pawn.boardPosition.tileNo.GetTileNumberInt() + nrOfSteps;

            Debug.Log("Current position of pawn: "+ pawn.boardPosition.tileNo.GetTileNumberInt());
            Debug.Log("Steps to take: "+ nrOfSteps);
            Debug.Log("Total steps: "+ totalSteps);
            Debug.Log("Total steps -15: "+ (totalSteps - 15));
            var nextQuadrant= quadrant.GetNextQuadrant();
            
            if (totalSteps > 14)
            {
                // Move it to the arrow tile if the pawn is finishing it's circuit of the board or move it to the heart tile of the next quadrant on the board
                if (pawn.teamEnum.ToQuadrant() == nextQuadrant)
                {
                    tileNumber = TileNumberEnum.Arrow;
                }
                else
                {
                    tileNumber = GetEnumFromNumber(totalSteps - 15);
                }
                // tileNumber = pawn.teamEnum.ToQuadrant() == nextQuadrant ? TileNumberEnum.Arrow : tileNumberDictionary[totalSteps - 15];

                quadrant = nextQuadrant;
            }
            else
            {
                if (pawn.teamEnum.ToQuadrant() == nextQuadrant)
                {
                    tileNumber = TileNumberEnum.Arrow;
                }
                else
                {
                    tileNumber = GetEnumFromNumber(totalSteps);
                }
            }
            
            return new BoardPosition(quadrant, tileNumber, GetTileVectorPosition(quadrant, tileNumber));
        }

        private TileNumberEnum GetEnumFromNumber(int number)
        {

            switch (number)
            {
                case 0:
                    Debug.Log("Til enum: "+ TileNumberEnum.Heart);
                    return TileNumberEnum.Heart;
                case 1:
                    Debug.Log("Til enum: "+ TileNumberEnum.One);
                    return TileNumberEnum.One;
                case 2:
                    Debug.Log("Til enum: "+ TileNumberEnum.Two);
                    return TileNumberEnum.Two;
                case 3:
                    Debug.Log("Til enum: "+ TileNumberEnum.Three);
                    return TileNumberEnum.Three;
                case 4:
                    Debug.Log("Til enum: "+ TileNumberEnum.Four);
                    return TileNumberEnum.Four;
                case 5:
                    Debug.Log("Til enum: "+ TileNumberEnum.Five);
                    return TileNumberEnum.Five;
                case 6:
                    Debug.Log("Til enum: "+ TileNumberEnum.Six);
                    return TileNumberEnum.Six;
                case 7:
                    Debug.Log("Til enum: "+ TileNumberEnum.Seven);
                    return TileNumberEnum.Seven;
                case 8:
                    Debug.Log("Til enum: "+ TileNumberEnum.Eight);
                    return TileNumberEnum.Eight;
                case 9:
                    Debug.Log("Til enum: "+ TileNumberEnum.Nine);
                    return TileNumberEnum.Nine;
                case 10:
                    Debug.Log("Til enum: "+ TileNumberEnum.Ten);
                    return TileNumberEnum.Ten;
                case 11:
                    Debug.Log("Til enum: "+ TileNumberEnum.Eleven);
                    return TileNumberEnum.Eleven;
                case 12:
                    Debug.Log("Til enum: "+ TileNumberEnum.Twelve);
                    return TileNumberEnum.Twelve;
                case 13:
                    Debug.Log("Til enum: "+ TileNumberEnum.Thirteen);
                    return TileNumberEnum.Thirteen;
                case 14:
                    Debug.Log("Til enum: "+ TileNumberEnum.Fourteen);
                    return TileNumberEnum.Fourteen;
                default:
                    Debug.Log("Til enum: "+ TileNumberEnum.HomeBase);
                    return TileNumberEnum.HomeBase;
            }
        }

        private BoardPosition OnMoveBackwardsPlayed(int nrOfSteps, Pawn pawn)
        {
            if (IsInHomeBase(pawn))
                return pawn.boardPosition;
            
            try
            {
                pawn.boardPosition = GetNewBoardPositionBackwards(nrOfSteps, pawn);
                return pawn.boardPosition;
            }
            catch (InvalidOperationException e) //This exception means that the tile already has a pawn on it
            {
                Console.WriteLine(e);
                //TODO: Call a method to move a pawn back to home base - Aldís 23.10.23
            }

            return null;
        }

        private BoardPosition GetNewBoardPositionBackwards(int nrOfSteps, Pawn pawn)
        {
            var quadrant = pawn.boardPosition.quadrantEnum;
            TileNumberEnum tileNumber;
            var landingPosition = pawn.boardPosition.tileNo.GetTileNumberInt() - nrOfSteps;

            if (landingPosition < 0)
            {
                quadrant = quadrant.GetPreviousQuadrant();

                tileNumber = GetEnumFromNumber(landingPosition + 15);
            }
            else
            {
                tileNumber = GetEnumFromNumber(landingPosition);
            }

            return new BoardPosition(quadrant, tileNumber, GetTileVectorPosition(quadrant, tileNumber));
        }

        private BoardPosition GetPawnFromHome(Pawn pawn)
        {
            var quadrant = pawn.teamEnum.ToQuadrant();
            pawn.boardPosition = new BoardPosition(quadrant, TileNumberEnum.Heart, GetTileVectorPosition(quadrant, TileNumberEnum.Heart));
            return pawn.boardPosition;
        }

        /// <summary>
        /// Gets the first Vector3 position of a GameObject tagged with the argument strings that does not already have a pawn on it.
        /// </summary>
        /// <param name="quadrant"></param>
        /// <param name="tileNo"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown if no GameObject was found with the given strings in their tag</exception>
        /// <exception cref="InvalidOperationException">Thrown if there is already a pawn on the tile</exception>
        private Vector3 GetTileVectorPosition(QuadrantEnum quadrant, TileNumberEnum tileNo)
        {
            try
            {
                var tiles = GameObject.FindGameObjectsWithTag($"{quadrant}{tileNo}");
                foreach (var tile in tiles)
                {
                    var position = tile.transform.position;
                    Debug.Log($"Tile found for {quadrant}{tileNo}. Position: {position}");
                    return position;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentException($"No tile found with '{quadrant}{tileNo}' tag.", e);
            }

            throw new InvalidOperationException($"Tile with '{quadrant}{tileNo}' tag is occupied.");
        }

        public List<Vector3> GetAvailableTilePositions(QuadrantEnum quadrant, TileNumberEnum tileNo)
        {
            var availablePositions = new List<Vector3>();
            try
            {
                var tiles = GameObject.FindGameObjectsWithTag($"{quadrant}{tileNo}");
                foreach (var tile in tiles)
                {
                    availablePositions.Add(tile.transform.position);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"No tile found with '{quadrant}{tileNo}' tag." + e);
            }

            return availablePositions;
        }
    }
}