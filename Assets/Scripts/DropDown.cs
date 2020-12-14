using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public void HandleInputData(int value)
    {
        PlayerPrefs.SetInt("level", value);
        Debug.Log("Player level is " + PlayerPrefs.GetInt("level"));
    }
}
