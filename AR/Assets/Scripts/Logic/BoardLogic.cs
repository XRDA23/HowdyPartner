using System;
using Enums;
using UnityEngine;

namespace Logic
{
    public class BoardLogic
    {
        // we have home and end bases as many as we have players - easier scalability
        private int noOfPlayers { get; set; } 
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

    public GameObject[] HandleCardPlayed(TeamEnum team, GameObject[] pawns, CardActionEnum cardAction)
    {
        switch (cardAction)
        {
            case CardActionEnum.Switch:
                Console.WriteLine("Switch");
                OnSwitchPlayed(pawns);
                break;
            case CardActionEnum.One:
                Console.WriteLine("1");
                OnMoveForwardPlayed(1, pawns[0]);
                break;
            case CardActionEnum.Two:
                Console.WriteLine("2");
                OnMoveForwardPlayed(2, pawns[0]);
                break;
            case CardActionEnum.Three:
                Console.WriteLine("3");
                OnMoveForwardPlayed(3, pawns[0]);
                break;
            case CardActionEnum.FourBackwards:
                Console.WriteLine("4 backwards");
                OnMoveBackwardsPlayed(4, pawns[0]);
                break;
            case CardActionEnum.Five:
                Console.WriteLine("5");
                OnMoveForwardPlayed(5, pawns[0]);
                break;
            case CardActionEnum.Six:
                Console.WriteLine("6");
                OnMoveForwardPlayed(6, pawns[0]);
                break;
            case CardActionEnum.SevenTimesOne:
                Console.WriteLine("7x1");
                //TODO: Need to get an array of pawns to move one step, if the same pawn moves multiple times it needs to be duplicated in the array - Aldís 09.10.23
                foreach (var pawn in pawns)
                {
                    OnMoveForwardPlayed(1, pawn);
                }
                break;
            case CardActionEnum.Eight:
                Console.WriteLine("8");
                OnMoveForwardPlayed(8, pawns[0]);
                break;
            case CardActionEnum.Nine:
                Console.WriteLine("9");
                OnMoveForwardPlayed(9, pawns[0]);
                break;
            case CardActionEnum.Ten:
                Console.WriteLine("10");
                OnMoveForwardPlayed(10, pawns[0]);
                break;
            case CardActionEnum.Heart:
                Console.WriteLine("Heart");
                SpawnPawn(team);
                break;
            case CardActionEnum.Twelve:
                Console.WriteLine("12");
                OnMoveForwardPlayed(12, pawns[0]);
                break;
            case CardActionEnum.Thirteen:
                Console.WriteLine("13");
                //TODO: Display prompt to decide which to use - Aldís 24.09.23
                OnMoveForwardPlayed(13, pawns[0]);
                break;
            case CardActionEnum.Fourteen:
                Console.WriteLine("14");
                OnMoveForwardPlayed(14, pawns[0]);
                break;
            default:
                Console.WriteLine($"Unknown card: {cardAction}");
                break;
        }
        return pawns;
    }

    private void OnSwitchPlayed(GameObject[] pawns)
    {
        (pawns[0].transform.position, pawns[1].transform.position) = (pawns[1].transform.position, pawns[0].transform.position);
    }

    private void OnMoveForwardPlayed(int nrOfSteps, GameObject pawn)
    {
        //TODO: Call board to know where to physically move the pawn - Aldís 09.10.23
        // pawn.position = boardPosition.getTileXStepsAhead(int nrOfSteps)
    }

    private void OnMoveBackwardsPlayed(int nrOfSteps, GameObject pawn)
    {
        //TODO: Call board position attribute of pawn to know where to physically move the pawn - Aldís 09.10.23
        // pawn.position = boardPosition.getTileXStepsBack(int nrOfSteps)
    }

    private void SpawnPawn(TeamEnum team)
    {
        //TODO: Move pawn from Team's home base to heart spawn point - Aldís 09.10.23
        // board.getHomeBaseOfPlayer(team).Leave(pawn)
    }

    private void KillPawn(TeamEnum team, GameObject pawn)
    {
        // TODO: Move pawn back to Team's home base. Home base methods would need to return a position - Aldís 09.10.23
        // pawn.position = board.getHomeBaseOfPlayer(team).Return(pawn)
    }
    }
}