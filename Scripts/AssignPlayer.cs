using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssignPlayer : MonoBehaviour
{
    public void onRandomButtonPress() {
        int playerValue = Random.Range(1, 3);

        if (playerValue == 1) {
            SceneManager.LoadScene("BlankCanvas");
        } else {
            SceneManager.LoadScene("CommunicationCenter");
        }

        Destroy(this);

    }
}
