using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRankings : MonoBehaviour
{
   public void HandleRanking(int value)
    {
        if(value == 0)
        {
            PlayerPrefs.SetInt("ranking", 1);
            Debug.Log("Player ranking is " + PlayerPrefs.GetInt("ranking"));
        }else if(value == 1)
        {
            PlayerPrefs.SetInt("ranking", 2);
            Debug.Log("Player ranking is " + PlayerPrefs.GetInt("ranking"));
        }else if (value == 2)
        {
            PlayerPrefs.SetInt("ranking", 3);
            Debug.Log("Player ranking is " + PlayerPrefs.GetInt("ranking"));
        }else if (value == 3){
                PlayerPrefs.SetInt("ranking", 4);
                Debug.Log("Player ranking is " + PlayerPrefs.GetInt("ranking"));
        }else if (value == 4)
        {
            PlayerPrefs.SetInt("ranking", 5);
            Debug.Log("Player ranking is " + PlayerPrefs.GetInt("ranking"));
        }

    }
}
