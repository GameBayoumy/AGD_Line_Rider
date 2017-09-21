using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    public GameObject meshGeneratorPrefab;          // Line mesh generator prefab
    public GameObject currentMeshGenerator;         // this is the current line mesh generator
    public bool canDraw = true;
    public bool isDrawing;

    private Vector3 pos;
    private Transform lineRend;

	// Use this for initialization
	void Start () {
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        // Instantiate and set the current mesh generator on button down
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            pos.z = 0;
            currentMeshGenerator = Instantiate(meshGeneratorPrefab, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
            lineRend = currentMeshGenerator.transform;
        }

        // GetMouseButton(0) works for both the mouse and touch 
        if (Input.GetMouseButton(0))
        {
            isDrawing = true;
            pos = Input.mousePosition;
            pos.z = 10; //because for some reason it sets the z position to 10
            pos = Camera.main.ScreenToWorldPoint(pos);
            lineRend.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
        }
    }
}
