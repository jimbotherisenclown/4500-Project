using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public void HandleInputData(int value)
    {
        if(value == 0)
        {
            StaticPlayerData.Level = 0;
        }else if(value == 1)
        {
            StaticPlayerData.Level = 1;
        }
        else if(value == 2)
        {
            StaticPlayerData.Level = 2;
        }
    }
}
