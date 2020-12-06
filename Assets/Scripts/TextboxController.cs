using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextboxController : MonoBehaviour
{
    public Text drawingTips;

    // Start is called before the first frame update
    void Start() {
        drawingTips.text = "Waiting on drawing tips!";
    }

    // Update is called once per frame
    void Update() {
    }

    public void setDrawingTips(string tips) {
        drawingTips.text = tips;
    }
}
