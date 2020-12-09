using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticPlayerData 
{
    public static int level;
    public static string username;

    public static int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public static string Username
    {
        get
        {
            return username;
        }
        set
        {
            username = value;
        }
    }
}
