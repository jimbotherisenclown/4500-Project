using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTransfer : MonoBehaviour
{
    public string username;
    public GameObject inputField;

    public void StoreName()
    {
        username = inputField.GetComponent<Text>().text;
        StaticPlayerData.Username = username;
    }
}
