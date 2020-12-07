using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    //Text used to test functions, loops, and conditionals for entry.
    public Text testText;

    //Scripters used to activate the corresponding tool - activated and deactivated through the button scripts.
    public GameObject drawScripter;
    public GameObject eraseScripter;
    public GameObject fillScripter;
    public GameObject lineScripter;
    public GameObject circleScripter;
    public GameObject squareScripter;
    public GameObject colorScripter;

    private void Start() {
        //Begin with the draw tool activated
        setActiveScripter(drawScripter);
    }

    //Activates when the draw tool toggle is pressed.
    public void onDrawButton() {
        setActiveScripter(drawScripter);
    }

    //Activates when the eraser tool toggle is pressed.
    public void onEraseButton() {
        setActiveScripter(eraseScripter);
    }

    //Activates when the fill tool toggle is pressed.
    public void onFillButton() {
        setActiveScripter(fillScripter);
    }

    //Activates when the line tool toggle is pressed.
    public void onLineButton() {
        setActiveScripter(lineScripter);
    }

    //Activates when the circle tool toggle is pressed.
    public void onCircleButton() {
        setActiveScripter(circleScripter);
    }

    //Activates when the square tool toggle is pressed.
    public void onSquareButton() {
        setActiveScripter(squareScripter);
    }

    //Activates when the color selector button is pressed.
    public void onColorButton() {
        setActiveScripter(colorScripter);
    }

    //The desired scripter is passed as a parameter. All scripters are deactivated, then the desired scripter is reactivated. 
    //Deactivating all scripters first ensures there is only one active scripter at a time.
    private void setActiveScripter(GameObject scripter) {
        drawScripter.SetActive(false);
        eraseScripter.SetActive(false);
        fillScripter.SetActive(false);
        lineScripter.SetActive(false);
        circleScripter.SetActive(false);
        squareScripter.SetActive(false);
        colorScripter.SetActive(false);
        scripter.SetActive(true);
    }
}
