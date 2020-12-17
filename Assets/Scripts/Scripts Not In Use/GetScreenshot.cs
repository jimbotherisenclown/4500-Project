using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScreenshot : MonoBehaviour
{  
    void Start()
    {
        getSreenshot();
    }

    // Update is called once per frame
    void getSreenshot()
    {
        ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);
    }
}
