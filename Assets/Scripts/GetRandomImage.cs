using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRandomImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("setImage", 5);
    }
    void setImage()
    {
        GetComponent<Image>().sprite = ImageGenerator.randomSprite;
        Debug.Log("getting image");
    }
}
