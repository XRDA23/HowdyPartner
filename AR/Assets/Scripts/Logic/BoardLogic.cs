using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Models;
using UnityEngine;

namespace Logic
{
    public class BoardLogic
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

        private BoardPosition OnMoveForwardPlayed(int nrOfSteps, Pawn pawn)
        {
            try
            {
                pawn.transform.position = pawn.boardPosition.vector3Position; //Plus the number of steps
                //TODO: Add the number of steps to it when the math is done - Aldís 23.10.23
                return pawn.boardPosition;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                //TODO: Call a method to move a pawn back to home base - Aldís 23.10.23
            }

            return null;
        }

        private BoardPosition OnMoveBackwardsPlayed(int nrOfSteps, Pawn pawn)
        {
            try
            {
                pawn.transform.position = pawn.boardPosition.vector3Position; //Minus the number of steps
                //TODO: Subtract the number of steps to it when the math is done - Aldís 23.10.23
                return pawn.boardPosition;
            }
            catch (InvalidOperationException e) //This exception means that the tile already has a pawn on it
            {
                Console.WriteLine(e);
                //TODO: Call a method to move a pawn back to home base - Aldís 23.10.23
            }

            return null;
        }

        private QuadrantEnum GetQuadrant(TeamEnum team)
        {
            var quadrants = EnumToList<QuadrantEnum>();
            return quadrants[(int) team];
        }

        private BoardPosition GetPawnFromHome(Pawn pawn)
        {
            var quadrant = GetQuadrant(pawn.teamEnum);
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
        public Vector3 GetTileVectorPosition(QuadrantEnum quadrant, TileNumberEnum tileNo)
        {
            try
            {
                var tiles = GameObject.FindGameObjectsWithTag($"{quadrant}{tileNo}");
                foreach (var tile in tiles)
                {
                    if (IsTileFree(tile))
                    {
                        return tile.transform.position;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ArgumentException($"No tile found with '{quadrant}{tileNo}' tag.", e);
            }
            
            throw new InvalidOperationException($"Tile with '{quadrant}{tileNo}' tag is occupied.");
        }

        private bool IsTileFree(GameObject tile)
        {
            return tile.GetComponent<Pawn>() == null;
        }
                
        private List<T> EnumToList<T>()
        {
            return new List<T>((T[])Enum.GetValues(typeof(T)));
        }
    }
}