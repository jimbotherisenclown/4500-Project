using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveName: MonoBehaviour
{
    public InputField textBox;
    
    public void clickSaveButton()
    {
        PlayerPrefs.SetString("username", textBox.text);
        Debug.Log("Your username is " + PlayerPrefs.GetString("username"));
    }
}
