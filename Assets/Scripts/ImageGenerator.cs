using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGenerator : MonoBehaviour
{
    public Sprite[] images = new Sprite[98];
    public Image randomImage;
    
    // Creates a sprite arrray of 98 images
    void Start()
    {
        randomImage = gameObject.GetComponent<Image>();
        generateImage();
    }

    void generateImage()
    {
        int num;

        //gets the level associated with each player and displays a corresponding image
        if (StaticPlayerData.Level == 0)
        {
            num = UnityEngine.Random.Range(65, images.Length);
        } else if (StaticPlayerData.Level == 1)
        {
            num = UnityEngine.Random.Range(32, 66);
        } else if (StaticPlayerData.Level == 2) { 
            num = UnityEngine.Random.Range(0, 32);
        }
        num = UnityEngine.Random.Range(0, images.Length);
        randomImage.sprite = images[num];
    }
}
