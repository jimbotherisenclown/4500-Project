using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour{
    ChangeCursor changeCursor;

    private void Start() {
        changeCursor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeCursor>();
    }


    void OnPointerEnter() {
        changeCursor.SetCursorEnter();
    }

    void OnPointerExit() {
        changeCursor.SetCursorExit();
    }
}
