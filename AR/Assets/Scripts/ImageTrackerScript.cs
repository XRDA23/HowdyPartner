using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/manual/tracked-image-manager.html
public class ImageTrackerScript : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imgManager;
    private Dictionary<string, CardTypeEnum> stringToCardTypeDictionary;
    public event Action<CardTypeEnum> OnCardScanned;

    void Start()
    {
        InitDictionary();
    }
    
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
            if (stringToCardTypeDictionary.TryGetValue(newImg.referenceImage.name, out CardTypeEnum cardType))
            {
                OnCardScanned?.Invoke(cardType);
            }
            else
            {
                Console.WriteLine($"{newImg.referenceImage.name} was not found in the image tracker dictionary");
            }
        }
    }
}