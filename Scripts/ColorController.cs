using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    Color32 currentColor;
    public Image image;

    // Start is called before the first frame update
    void Start() {
        currentColor = new Color32(0x00, 0x00, 0x00, 0xFF);
    }

    // Update is called once per frame
    void Update() {
        image.GetComponent<Image>().color = currentColor;
    }

    public Color getCurrentColor() {
        return currentColor;
    }

    public void blackToggle() {
        currentColor = new Color32(0x00, 0x00, 0x00, 0xFF);
    }

    public void brownToggle() {
        currentColor = new Color32(0x6E, 0x3B, 0x08, 0xFF);
    }

    public void darkRedToggle() {
        currentColor = new Color32(0x88, 0x00, 0x14, 0xFF);
    }

    public void orangeToggle() {
        currentColor = new Color32(0xFF, 0x80, 0x29, 0xFF);
    }

    public void yellowToggle() {
        currentColor = new Color32(0xFF, 0xF3, 0x00, 0xFF);
    }

    public void blueToggle() {
        currentColor = new Color32(0x3F, 0x49, 0xCB, 0xFF);
    }

    public void purpleToggle() {
        currentColor = new Color32(0x15, 0x49, 0xCB, 0xFF);
    }

    public void greyToggle() {
        currentColor = new Color32(0x7F, 0x80, 0x7F, 0xFF);
    }

    public void tanToggle() {
        currentColor = new Color32(0xBB, 0x7A, 0x58, 0xFF);
    }

    public void redToggle() {
        currentColor = new Color32(0xEF, 0x1C, 0x25, 0xFF);
    }

    public void pinkToggle() {
        currentColor = new Color32(0xFF, 0xAE, 0xCA, 0xFF);
    }

    public void greenToggle() {
        currentColor = new Color32(0x21, 0xB0, 0x4D, 0xFF);
    }

    public void limeToggle() {
        currentColor = new Color32(0xB5, 0xE7, 0x1E, 0xFF);
    }

    public void lightBlueToggle() {
        currentColor = new Color32(0x00, 0xA2, 0xE7, 0xFF);
    }
    
    public void whiteToggle() {
        currentColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    }
}
