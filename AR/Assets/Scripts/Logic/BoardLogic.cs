using System;
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

        //What methods will the GameLogic call on this class? - need to be added

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
            (pawns[0].transform.position, pawns[1].transform.position) =
                (pawns[1].transform.position, pawns[0].transform.position);
        }

        private BoardPosition OnMoveForwardPlayed(int nrOfSteps, Pawn pawn)
        {
            //TODO: Call board to know where to physically move the pawn - Aldís 09.10.23
            // pawn.position = boardPosition.getTileXStepsAhead(int nrOfSteps)
            //BIANCA do the math :) <3
            return null;
        }

        private BoardPosition OnMoveBackwardsPlayed(int nrOfSteps, Pawn pawn)
        {
            //TODO: Call board position attribute of pawn to know where to physically move the pawn - Aldís 09.10.23
            // pawn.position = boardPosition.getTileXStepsBack(int nrOfSteps)
            return null;
        }

        private QuadrantEnum GetQuadrant(TeamEnum team)
        {
            switch (team)
            {
                case TeamEnum.BlueOrWater:
                    return QuadrantEnum.Blue;
                case TeamEnum.RedOrHeart:
                    return QuadrantEnum.Red;
                case TeamEnum.YellowOrStar:
                    return QuadrantEnum.Yellow;
                case TeamEnum.GreenOrEmerald:
                    return QuadrantEnum.Green;
                default:
                    return QuadrantEnum.Blue;
            }
        }

        private BoardPosition GetPawnFromHome(Pawn pawn)
        {
            pawn.position.quadrantEnum = GetQuadrant(pawn.teamEnum);
            pawn.position.tileNo = TileNumberEnum.Heart;
            return pawn.position;
        }
    }
}