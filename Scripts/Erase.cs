using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Erase : MonoBehaviour {
    public GameObject drawToolPrefab; //Holds the imported Unity prefab that is used as a template for how our line should look
    public GameObject currentLine; //This is the Unity object that holds the line that is currently being drawn
    public LineRenderer lineRenderer; //Holds the Unity line renderer component needed to render the line in currentLine
    public List<Vector2> brushPositions; //Holds a list of every position for the current line


    void Update() {

        //If the mouse button is pressed, create a dot sized line
        if (Input.GetMouseButtonDown(0)) {
            CreateLine();
        }

        //While the left mouse button is held down and within canvas borders, set the brush position to the current mouse position and add points to the line
        if (Input.GetMouseButton(0)) {
            Vector2 tempBrushPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(tempBrushPosition, brushPositions[brushPositions.Count - 1]) > .1f) {
                UpdateLine(tempBrushPosition);
            }
        }
    }

    //Create a dot sized line if the player just taps the brush button
    void CreateLine() {
        //Create a new line object by using the draw tool prefab with position to be determined later in this function
        currentLine = Instantiate(drawToolPrefab, Vector3.zero, Quaternion.identity);

        //Create a line renderer to render the current line
        lineRenderer = currentLine.GetComponent<LineRenderer>();

        //Create a new list of positions the brush has touched the screen
        brushPositions.Clear();

        //Set the initial position of the brush stroke as the first item in the list
        brushPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Set the end position of the brush stroke as the second item in the list
        brushPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Sets the beginning of the brush stroke using the first item in the list
        lineRenderer.SetPosition(0, brushPositions[0]);

        //Sets the end of the brush stroke using the second item in the list
        lineRenderer.SetPosition(1, brushPositions[1]);
    }

    //Adds new points so a line can be created
    void UpdateLine(Vector2 newBrushPosition) {
        brushPositions.Add(newBrushPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newBrushPosition);
    }


}
