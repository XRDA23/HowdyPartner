using System;
using Enums;
using UnityEngine;

namespace Logic
{
    public class BoardLogic
    {
        private int
            noOfPlayers { get; set; } // we have home and end bases as many as we have players - easier scalability

        private HomeBase[] homeBases;
        private EndBase[] endBases;
        [SerializeField] private GameObject currentPawn;
        [SerializeField] private GameObject otherPawn;

        public BoardLogic(int noOfPlayers)
        {
            this.noOfPlayers = noOfPlayers;
            homeBases = new HomeBase[noOfPlayers];
            endBases = new EndBase[noOfPlayers];
        }

        //What methods will the GameLogic call on this class? - need to be added

        public void HandleCardPlayed(CardTypeEnum cardType)
        {
            //TODO: Set pawns as appropriate - Aldís 24.09.23
            switch (cardType)
            {
                case CardTypeEnum.Switch:
                    Console.WriteLine("Switch");
                    OnSwitchPlayed();
                    break;
                case CardTypeEnum.OneOrFourteen:
                    Console.WriteLine("1 or 14");
                    //TODO: Display prompt to decide which number to use - Aldís 24.09.23
                    OnMoveForwardPlayed(14);
                    break;
                case CardTypeEnum.Two:
                    Console.WriteLine("2");
                    OnMoveForwardPlayed(2);
                    break;
                case CardTypeEnum.Three:
                    Console.WriteLine("3");
                    OnMoveForwardPlayed(3);

                    break;
                case CardTypeEnum.FourBackwards:
                    Console.WriteLine("4 backwards");
                    OnMoveBackwardsPlayed(4);

                    break;
                case CardTypeEnum.Five:
                    Console.WriteLine("5");
                    OnMoveForwardPlayed(5);

                    break;
                case CardTypeEnum.Six:
                    Console.WriteLine("6");
                    OnMoveForwardPlayed(6);
                    break;
                case CardTypeEnum.SevenTimesOne:
                    Console.WriteLine("7x1");
                    //TODO: Need info on what pawns and how many steps each - Aldís 24.09.23
                    OnMoveForwardPlayed(7);
                    break;
                case CardTypeEnum.HeartOrEight:
                    Console.WriteLine("Heart or 8");
                    //TODO: Display prompt to decide which to use - Aldís 24.09.23
                    OnMoveForwardPlayed(8);
                    SpawnPawn();
                    break;
                case CardTypeEnum.Nine:
                    Console.WriteLine("9");
                    OnMoveForwardPlayed(9);
                    break;
                case CardTypeEnum.Ten:
                    Console.WriteLine("10");
                    OnMoveForwardPlayed(10);
                    break;
                case CardTypeEnum.Heart:
                    Console.WriteLine("Heart");
                    SpawnPawn();
                    break;
                case CardTypeEnum.Twelve:
                    Console.WriteLine("12");
                    OnMoveForwardPlayed(12);
                    break;
                case CardTypeEnum.HeartOrThirteen:
                    Console.WriteLine("Heart or 13");
                    //TODO: Display prompt to decide which to use - Aldís 24.09.23
                    OnMoveForwardPlayed(13);
                    SpawnPawn();
                    break;
                default:
                    Console.WriteLine($"Unknown card: {cardType}");
                    break;
            }
        }

        private void OnSwitchPlayed()
        {
        }

        private void OnMoveForwardPlayed(int nrOfSteps)
        {
        }

        private void OnMoveBackwardsPlayed(int nrOfSteps)
        {
        }

        private void SpawnPawn()
        {
        }

        private void KillPawn()
        {
        }
    }
}