using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public GameObject drawToolPrefab; //Holds the imported Unity prefab that is used as a template for how our line should look
    public GameObject currentLine; //This is the Unity object that holds the line that is currently being drawn
    public LineRenderer lineRenderer; //Holds the Unity line renderer component needed to render the line in currentLine
    public List<Vector2> brushPositions; //Holds a list of every position for the current line
    private Sorter sorter; //Holds the imported script used to determine sorting layer for all objects
    private ColorController colorController; //Holds the imported script used to determine the current color

    // Start is called before the first frame update
    void Start() {
        //Initialize the currently running instances of imported scripts
        sorter = GameObject.FindObjectOfType(typeof(Sorter)) as Sorter;
        colorController = GameObject.FindObjectOfType(typeof(ColorController)) as ColorController;
    }

    // Update is called once per frame
    void Update() {
        //If the mouse button is pressed, create a dot sized line. If the line start has been created, this will not run.
        if (Input.GetMouseButtonDown(0)) {
            //Create the start of the line
            CreateLineStart();
        }
        
        //If the mouse button is pressed, a line start point has been created, and enough time has passed between clicks, create the endpoint for the line
        if (Input.GetMouseButton(0)) {
            //Create a variable to hold the current mouse position
            Vector2 temporaryEndpoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Set the current mouse position as the temporary endpoint.
            CreateEndpoint(temporaryEndpoint);
        }
    }

    //Unity doesn't pass variables between scripts well, so this is copied wholesale from BasicDraw.cs
    void CreateLineStart() {
        //Increment the sorting layer by one for all drawn objects
        sorter.setNumberOfItems();

        //Create a new line object by using the draw tool prefab with position to be determined later in this function
        currentLine = Instantiate(drawToolPrefab, Vector3.zero, Quaternion.identity);

        //Create a line renderer to render the current line
        lineRenderer = currentLine.GetComponent<LineRenderer>();

        //Set the sorting order for the most recent line to the current number of items
        lineRenderer.sortingOrder = sorter.getNumberOfItems();

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

        //Allows the emission property to be settable
        lineRenderer.material.EnableKeyword("_EMISSION");

        //Changes the emission color to the currently selected color
        lineRenderer.material.SetColor("_EmissionColor", colorController.getCurrentColor());
    }
    

    //Create the endpoint for the line
    void CreateEndpoint(Vector2 endpointPosition) {
        //Update the lineRenderer to accomodate a third position for the endpoint
        lineRenderer.positionCount = 3;

        //Set the endpoint to be the current mouse position
        brushPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Add the new endpoint to the lineRenderer list
        lineRenderer.SetPosition(2, endpointPosition);
    }
}
