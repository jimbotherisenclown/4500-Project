using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeShot : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPostRender()
    {
        Debug.Log("hello");
        StartCoroutine(takeScreenshot());
    }

    // Update is called once per frame
    /**void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 89 && timer < 270)
        {
            StartCoroutine(takeScreenshot());
        }else if (timer >= 270) 
        {
            StartCoroutine(takeScreenshot());
        }
    }*/
    private IEnumerator takeScreenshot()
    {
        Debug.Log("Taking Screenshot");
        ScreenCapture.CaptureScreenshot("CameraScreenshot.png");
        //So that the screenshot is taken
        yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(3f);
        //GetPhoto();
    }

    
}
