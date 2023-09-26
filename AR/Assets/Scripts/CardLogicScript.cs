using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CardLogicScript : MonoBehaviour
{
    [SerializeField] private IPawnActions pawnActions;
    [SerializeField] private GameObject currentPawn;
    [SerializeField] private GameObject otherPawn;

    public void HandleCardPlayed(CardTypeEnum cardType, GameObject pawn1, [CanBeNull] GameObject pawn2)
    {
        //TODO: Set pawns as appropriate - Aldís 24.09.23
        switch (cardType)
        {
            case CardTypeEnum.Switch:
                OnSwitchPlayed();
                break;
            case CardTypeEnum.OneOrFourteen:
                //TODO: Display prompt to decide which number to use - Aldís 24.09.23
                OnMoveForwardPlayed(14);
                break;
            case CardTypeEnum.Two:
                OnMoveForwardPlayed(2);
                break;
            case CardTypeEnum.Three:
                OnMoveForwardPlayed(3);
                break;
            case CardTypeEnum.FourBackwards:
                OnMoveBackwardsPlayed(4);
                break;
            case CardTypeEnum.Five:
                OnMoveForwardPlayed(5);
                break;
            case CardTypeEnum.Six:
                OnMoveForwardPlayed(6);
                break;
            case CardTypeEnum.SevenTimesOne:
                //TODO: Need info on what pawns and how many steps each - Aldís 24.09.23
                OnMoveForwardPlayed(7);
                break;
            case CardTypeEnum.HeartOrEight:
                //TODO: Display prompt to decide which to use - Aldís 24.09.23
                OnMoveForwardPlayed(8);
                SpawnPawn();
                break;
            case CardTypeEnum.Nine:
                OnMoveForwardPlayed(9);
                break;
            case CardTypeEnum.Ten:
                OnMoveForwardPlayed(10);
                break;
            case CardTypeEnum.Heart:
                SpawnPawn();
                break;
            case CardTypeEnum.Twelve:
                OnMoveForwardPlayed(12);
                break;
            case CardTypeEnum.HeartOrThirteen:
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
        pawnActions.SwitchPositions(currentPawn, otherPawn);
    }

    private void OnMoveForwardPlayed(int nrOfSteps)
    {
        pawnActions.MoveForward(currentPawn, nrOfSteps);
    }

    private void OnMoveBackwardsPlayed(int nrOfSteps)
    {
        pawnActions.MoveBackward(currentPawn, nrOfSteps);
    }

    private void SpawnPawn()
    {
        pawnActions.GetPawnFromHome(currentPawn);
    }

    private void KillPawn()
    {
        pawnActions.MoveHome(otherPawn);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
