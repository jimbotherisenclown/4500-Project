using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleScript : MonoBehaviour {
    public GameObject drawToolPrefab; //Holds the imported Unity prefab that is used as a template for how our line should look
    public GameObject currentLine; //This is the Unity object that holds the line that is currently being drawn
    public LineRenderer lineRenderer; //Holds the Unity line renderer component needed to render the line in currentLine
    public List<Vector2> brushPositions; //Holds a list of every position for the current line
    bool lineStart; //Flag that is set to true if the line start position has been set
    public float timeDelay = -1; //Negative value to be added to the delay; allows for syncing with Time.time
    public float mouseDelay = 0.01f; //Determines the time before an endpoint can be set

    // Start is called before the first frame update
    void Start() {
        lineStart = false;
    }

    // Update is called once per frame
    void Update() {
        //If the mouse button is pressed, create a dot sized line. If the line start has been created, this will not run.
        if (Input.GetMouseButtonDown(0) && !lineStart) {
            //Create the start of the line
            CreateLineStart();

            //Declare that the line start point has been set
            lineStart = true;

            //Update the delay before the next mouse click is allowed.
            timeDelay = Time.time + mouseDelay;
        }

        //If the mouse button is pressed, a line start point has been created, and enough time has passed between clicks, create the endpoint for the line
        if (Input.GetMouseButtonDown(0) && lineStart && (Time.time >= timeDelay)) {
            //
            CreateEndpoint();
        }

        /*TO BE IMPLEMENTED:
        *Check if click is on canvas
        *If click is off canvas, delete line startpoint.
        *IF TIME ALLOWS, change the script so the line endpoint is dynamically generated to the current mouse position until clicked.
        */

    }

    //Unity doesn't pass variables between scripts well, so this is copied wholesale from BasicDraw.cs
    void CreateLineStart() {
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


    //Create the endpoint for the line
    void CreateEndpoint() {
        //Update the lineRenderer to accomodate a third position for the endpoint
        lineRenderer.positionCount = 5;

        //Set the endpoint to be the current mouse position
        brushPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        Debug.Log(brushPositions[1].x);

        //Sets the first corner
        lineRenderer.SetPosition(1, new Vector3(brushPositions[0].x, brushPositions[2].y, 0));

        //Sets the second corner
        lineRenderer.SetPosition(2, brushPositions[2]);

        //Sets the third corner
        lineRenderer.SetPosition(3, new Vector3(brushPositions[2].x, brushPositions[0].y, 0));

        //Brings the line back in to the start
        lineRenderer.SetPosition(4, brushPositions[0]);

        //Allows a new line to be placed
        lineStart = false;
    }
}