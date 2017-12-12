using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour {

    public bool testInProgress;
    public GameObject[] objectsToToggle;
    public GameObject failureScreen;
    public GameObject ghost;

    GameObject camera;
    GameObject topWall;
    GameObject bottomWall;
    GameObject bluePrint;
    GameObject playButton;
    GameObject trashcan;
    GameObject scrollBar;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
        topWall = GameObject.Find("TopWall");
        bottomWall = GameObject.Find("BottomWall");
        bluePrint = GameObject.Find("Blueprint");
        playButton = GameObject.Find("Play");
        trashcan = GameObject.Find("Trashcan");
        scrollBar = GameObject.Find("ScrollBar");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartTest()
    {
        testInProgress = true;

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

        trashcan.SetActive(false);
        scrollBar.SetActive(false);
        playButton.transform.localPosition = new Vector2(321, 800);
    }

    public void QuitTest()
    {
        testInProgress = false;

        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            objectsToToggle[i].SetActive(false);
        }

        GameObject[] trash = GameObject.FindGameObjectsWithTag("Spawnables");

        for (int j = 0; j < trash.Length; j++)
        {
            Destroy(trash[j]);
        }

        camera.transform.position = new Vector3(0, 1, -10);
        camera.GetComponent<Touch>().enabled = false;
        camera.GetComponent<CameraController>().offset = Vector3.zero;
        camera.GetComponent<CameraController>().enabled = false;

        topWall.GetComponent<WallMesh>().enabled = false;
        bottomWall.GetComponent<WallMesh>().enabled = false;

        bluePrint.transform.position = new Vector3(0, 2, 9);

        trashcan.SetActive(true);
        scrollBar.SetActive(true);
        playButton.transform.localPosition = new Vector2(321, 189);

        Destroy(ghost);

        objectsToToggle[0].GetComponent<GameOverMenu>().Unfreeze();

        failureScreen.SetActive(false);
    }
}
