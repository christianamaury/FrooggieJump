using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    [Header("Screen Resolution Settings")]
    private int height = 2688;
    private int width = 1242;
    private int refreshRate = 30;
    private bool fullScreen = true;

    // Start is called before the first frame update
    void Start()
    {
        //Same screen screen resolution for both platforms..
    #if UNITY_ANDROID || UNITY_IPHONE
        Screen.SetResolution(width, height, fullScreen, refreshRate);
    #endif
    }

}
