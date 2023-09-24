using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/manual/tracked-image-manager.html
public class ImageTrackerScript : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imgManager;

    private void OnEnable()
    {
        imgManager.trackedImagesChanged += OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImg in eventArgs.added)
        {
            Console.WriteLine($"Added new image: {newImg.name}");
            //TODO: PoC, to be replaced with actual code - Aldís 24.09.23 
        }

        foreach (var updateImg in eventArgs.updated)
        {
            Console.WriteLine($"Updated image: {updateImg.name}");
            //TODO: PoC, to be replaced with actual code - Aldís 24.09.23 
        }

        foreach (var removedImg in eventArgs.removed)
        {
            Console.WriteLine($"Removed image: {removedImg.name}");
            //TODO: PoC, to be replaced with actual code - Aldís 24.09.23 
        }
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
