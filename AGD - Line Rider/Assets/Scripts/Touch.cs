using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour
{
    public GameObject meshGeneratorPrefab;          // Line mesh generator prefab
    public GameObject currentMeshGenerator;         // this is the current line mesh generator
    public bool canDraw = true;
    public bool isDrawing;
    public bool isTouching;

    private Vector3 pos;
    private Transform lineRend;

	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject())
            {
                isTouching = true; // Set touching to true to track player input
                if (canDraw)
                {
                    isDrawing = true;
                    pos.z = 0;

                    // Instantiate and set the current mesh generator on button down
                    currentMeshGenerator = Instantiate(meshGeneratorPrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                    lineRend = currentMeshGenerator.transform;
                }
                else
                {
                    isTouching = false; // Stop drawing lines when player cant draw
                }
            }
            else
            {
                SoundManager.PlaySFX(GameManager.menuSFX, "GUI");
            }
        }

        if (isTouching)
        {
            if (canDraw)
            {
                isDrawing = true;
                pos = Input.mousePosition;
                pos.z = 10; //because for some reason it sets the z position to 10
                pos = Camera.main.ScreenToWorldPoint(pos);
                lineRend.position = pos;
            }
            else
            {
                isTouching = false; // Stop drawing lines when player cant draw
            }
        }

        // Set values to false when player doenst touch the screen
        if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
            isDrawing = false;
        }
    }

    private bool IsPointerOverUIObject()
    {
        // Gather touch point events
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        // Cast a raycast on UI elements
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        // Return value if touch input is over UI element
        return results.Count > 0;
    }
}
