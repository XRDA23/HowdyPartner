using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/manual/tracked-image-manager.html
public class ImageTrackerScript : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imgManager;
    [SerializeField] private CardLogicScript cardLogicScript;
    private Dictionary<string, CardTypeEnum> stringToCardTypeDictionary;

    private void InitDictionary()
    {
        stringToCardTypeDictionary = new Dictionary<string, CardTypeEnum>()
        {
            {"2", CardTypeEnum.Two},
            {"3", CardTypeEnum.Three},
            {"4-Backwards", CardTypeEnum.FourBackwards},
            {"5", CardTypeEnum.Five},
            {"6", CardTypeEnum.Six},
            {"7-Times-1", CardTypeEnum.SevenTimesOne},
            {"9", CardTypeEnum.Nine},
            {"10", CardTypeEnum.Ten},
            {"12", CardTypeEnum.Twelve},
            {"Heart-or-8", CardTypeEnum.HeartOrEight},
            {"Heart-or-13", CardTypeEnum.HeartOrThirteen},
            {"Heart", CardTypeEnum.Heart},
            {"1-or-14", CardTypeEnum.OneOrFourteen},
            {"Switch", CardTypeEnum.Switch}
        };
    }
    
    private void OnEnable()
    {
        imgManager.trackedImagesChanged += OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImg in eventArgs.added)
        {
            Console.WriteLine($"{newImg.referenceImage.name} card has been detected");
            //TODO: Display prompt to confirm using this card & selecting pawn(s) to move - Aldís 24.09.23
            GameObject pawn1 = new GameObject();
            GameObject pawn2 = new GameObject();
            ExecuteLogic(stringToCardTypeDictionary[newImg.referenceImage.name]);
        }

        foreach (var updateImg in eventArgs.updated)
        {
            Console.WriteLine($"{updateImg.referenceImage.name} has changed");
            //TODO: PoC, to be replaced with actual code - Aldís 24.09.23 
        }

        foreach (var removedImg in eventArgs.removed)
        {
            Console.WriteLine($"{removedImg.referenceImage.name} is no longer visible");
            //TODO: PoC, to be replaced with actual code - Aldís 24.09.23 
        }
    }

    private void ExecuteLogic(CardTypeEnum cardType)
    {
        cardLogicScript.HandleCardPlayed(cardType);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
