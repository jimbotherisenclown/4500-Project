using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGenerator : MonoBehaviour
{
    public Sprite[] images = new Sprite[99];
    public static Sprite[] image = new Sprite[99];
    public Image randomImage;
    public static int num;

       
    // Creates a sprite arrray of 98 images
    void Start()
    {
        randomImage = gameObject.GetComponent<Image>();
        image = images;
        generateImage();
    }

    void generateImage()
    {
        //gets the level associated with each player and displays a corresponding image
        if (StaticPlayerData.Level == 0)
        {
            num = UnityEngine.Random.Range(0, 32);
        } else if (StaticPlayerData.Level == 1)
        {
            num = UnityEngine.Random.Range(32, 66);
        } else if (StaticPlayerData.Level == 2) { 
            num = UnityEngine.Random.Range(32, images.Length-1);
        }
        if ((NetworkController.LEVEL <= 32))
        {
            num = UnityEngine.Random.Range(0, 32);
        }
        else if (NetworkController.LEVEL >= 33 || NetworkController.LEVEL <= 65)
        {
            num = UnityEngine.Random.Range(33, 65);

        }
        else
        {
            num = UnityEngine.Random.Range(66, images.Length-1);
        }
        Debug.Log(num);
        randomImage.sprite = images[num];
    }
}
