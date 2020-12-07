using UnityEngine;
using System.Collections;

public class ChangeCursor : MonoBehaviour {    
    public Texture2D drawTexture;
    public Texture2D mouse;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;    

    public void SetCursorEnter() {
        Cursor.SetCursor(drawTexture, hotSpot, cursorMode);
    }
    
    public void SetCursorExit() {
        Cursor.SetCursor(mouse, Vector2.zero, CursorMode.Auto);
    }
}