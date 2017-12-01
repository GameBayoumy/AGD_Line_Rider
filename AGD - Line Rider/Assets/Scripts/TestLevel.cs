using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour {

    public bool testInProgress;
    public GameObject[] objectsToToggle;

    GameObject camera;
    GameObject topWall;
    GameObject bottomWall;
    GameObject bluePrint;
    GameObject playButton;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
        topWall = GameObject.Find("TopWall");
        bottomWall = GameObject.Find("BottomWall");
        bluePrint = GameObject.Find("Blueprint");
        playButton = GameObject.Find("Play");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartTest()
    {
        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            objectsToToggle[i].SetActive(true);
        }


        camera.transform.position = new Vector3(-53, 0, -10);
        camera.GetComponent<Touch>().enabled = true;
        camera.GetComponent<CameraController>().enabled = true;

        topWall.GetComponent<WallMesh>().enabled = true;
        bottomWall.GetComponent<WallMesh>().enabled = true;

        bluePrint.transform.position = new Vector3(transform.position.x, transform.position.y, -15);

        playButton.SetActive(false);
    }
}
