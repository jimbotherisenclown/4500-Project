using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int level;
    private string username;
    
    public int Level
    {
        get
        {
            //Debug.Print("Accessing level");
            return level;
        }
        set
        {
            //Debug.Print("Writing to level");
            level = value;
        }
    }

    public string Username
    {
        get
        {
            //Debug.Print("Accessing username");
            return username;
        }
        set
        {
            //Debug.Print("Writing to username");
            username = value;
        }
    }
}
