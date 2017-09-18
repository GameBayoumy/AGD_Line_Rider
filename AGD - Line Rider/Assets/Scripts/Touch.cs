using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

    // this is the line mesh generator
    public GameObject lineRend;

    private Vector3 pos;

	// Use this for initialization
	void Start () {

         pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        // GetMouseButton(0) works for both the mouse and touch 
        if (Input.GetMouseButton(0))
        {
            pos = Input.mousePosition;
            pos.z = 10; //because for some reason it sets the z position to 10
            pos = Camera.main.ScreenToWorldPoint(pos);
            lineRend.transform.position = pos;

        }

    }
}
