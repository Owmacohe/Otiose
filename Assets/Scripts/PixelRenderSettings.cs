using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelRenderSettings : MonoBehaviour
{
    enum PixelationTypes { None, Medium, Large, Huge, Enormous };
    [Tooltip("Amount of pixelation to be applied to the camera resolution")]
    [SerializeField] PixelationTypes pixelation = PixelationTypes.Large;

    PixelationTypes lastPixelation; // The pixelation value when it was last changed

    void Start()
    {
        SetPixelation();
    }

    /*
    void FixedUpdate()
    {
        if (lastPixelation != pixelation)
        {
            SetPixelation();
        }
    }
    */

    /// <summary>
    /// Method to set the value of the PixelRenderTexture
    /// </summary>
    void SetPixelation()
    {
        // The resolution looks best when at 1920x1080, but it's relatively forgiving for other aspects
        int width = Screen.width;
        int height = Screen.height;
        float divisor = 1;
        
        switch (pixelation)
        {
            case PixelationTypes.None:
                divisor = 1;
                break;
            case PixelationTypes.Medium:
                divisor = 4;
                break;
            case PixelationTypes.Large:
                divisor = 6;
                break;
            case PixelationTypes.Huge:
                divisor = 12;
                break;
            case PixelationTypes.Enormous:
                divisor = 24;
                break;
        }

        // Setting the resolution
        RawImage image = GetComponent<RawImage>();

        try
        {
            image.texture.width = (int)(width / divisor);
            image.texture.height = (int)(height / divisor);
        }
        catch { }

        lastPixelation = pixelation;
    }
}
