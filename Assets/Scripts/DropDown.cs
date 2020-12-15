using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public void HandleInputData(int value)
    {
        if(value == 0)
        {
            PlayerPrefs.SetInt("level", 1);
            Debug.Log("Player level is " + PlayerPrefs.GetInt("level"));
        }else if(value == 1)
        {
            PlayerPrefs.SetInt("level", 2);
            Debug.Log("Player level is " + PlayerPrefs.GetInt("level"));
        }else if(value == 2)
        {
            PlayerPrefs.SetInt("level", 3);
            Debug.Log("Player level is " + PlayerPrefs.GetInt("level"));
        }
    }
}
