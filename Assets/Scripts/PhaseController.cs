using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseController : MonoBehaviour
{
    private float timer; //Holds the current time since scene was loaded
    private int displayedTime; //Holds the integer rounded value of the timer, which is to be passed to timerText via ToString()
    public Text timerText; //This is the Unity Text object used to display the timer value..
    int currentPhase; //Holds the integer value of the current phase.

    /*vvv CHANGE THESE VALUES TO CHANGE THE GAME TIME vvv*/
    const int phase2Threshold = 90; //Time from start of game until Phase 2 begins
    const int phase3Threshold = 180; //Time from start of game until Phase 3 begins
    const int gameOverThreshold = 270; //Time from start of game until game ends.
    /*^^^ CHANGE THESE VALUES TO CHANGE THE GAME TIME ^^^*/


    // Start is called before the first frame update
    void Start()
    {
        //Sets the starting phase to 1 to initialize and flush holdover values from previous scene loads
        currentPhase = 1;

        //Initializes the starting timer
        timerText.text = "Time until phase 2: 90 seconds";

        //Resets the timer value to flush holdover values from previous scene loads
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Adds the time since last frame to the timer. Since this is added cumulatively, when Update() begins, it effectively keeps a running time since game start
        timer += Time.deltaTime;
        
        //Casts the float value contained in timer as an int and stores the integer value in displayedTime
        displayedTime = (int)timer;

        //Sets the current phase according to how much time has passed. Threshold values are used here so that values only need to be changed at the start of the script if phase times change.
        if (timer >= phase2Threshold && timer < phase3Threshold) { 
            currentPhase = 2;
        } else if (timer >= phase3Threshold && timer < gameOverThreshold) {
            currentPhase = 3;

        //If current time exceeds total game time, set the phase to 4.
        } else if (timer >= gameOverThreshold) { 
            currentPhase = 4;
        }

        //Displays the amount of time remaining in the current phase, along with a context-appropriate message.
        if (currentPhase == 1) {
            timerText.text = "Time Till\nPhase 2:\n" + (90 - displayedTime).ToString();
        } else if (currentPhase == 2) {
            timerText.text = "Time Till\nPhase 3:\n" + (180 - displayedTime).ToString();
        } else if (currentPhase == 3) {
            timerText.text = "Time Till\nGame Over:\n" + (270 - displayedTime).ToString();
        } else {
            timerText.fontSize = 16;
            timerText.text = "Time's\nup!";
        }

    }
}
