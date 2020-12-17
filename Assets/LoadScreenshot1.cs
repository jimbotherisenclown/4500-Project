using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadScreenshot1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        getPhoto();
    }

    // Update is called once per frame
    void getPhoto()
    {
        string url = Application.persistentDataPath + "/" + "CameraScreenshot.png";
        var bytes = File.ReadAllBytes(url);
        Texture2D texture = new Texture2D(2, 2);
        bool imageLoadSuccess = texture.LoadImage(bytes);
        while (!imageLoadSuccess)
        {
            print("image load failed");
            bytes = File.ReadAllBytes(url);
            imageLoadSuccess = texture.LoadImage(bytes);
        }
        print("Image load success: " + imageLoadSuccess);
        GetComponent<Image>().overrideSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0f, 0f), 100f);
    }
}
