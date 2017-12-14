using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : MonoBehaviour {

    public bool testInProgress;
    public GameObject[] objectsToToggle;
    public GameObject failureScreen;
    public GameObject ghost;
    public GameObject advancedMenu;

    GameObject _camera;
    GameObject _topWall;
    GameObject _bottomWall;
    GameObject _bluePrint;
    GameObject _playButton;
    GameObject _trashcan;
    GameObject _scrollBar;

	// Use this for initialization
	void Start () {
        _camera = GameObject.Find("Main Camera");
        _topWall = GameObject.Find("TopWall");
        _bottomWall = GameObject.Find("BottomWall");
        _bluePrint = GameObject.Find("Blueprint");
        _playButton = GameObject.Find("Play");
        _trashcan = GameObject.Find("Trashcan");
        _scrollBar = GameObject.Find("ScrollBar");
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


        _camera.transform.position = new Vector3(-53, 0, -10);
        _camera.GetComponent<Touch>().enabled = true;
        _camera.GetComponent<CameraController>().enabled = true;

        _topWall.GetComponent<WallMesh>().enabled = true;
        _bottomWall.GetComponent<WallMesh>().enabled = true;

        _bluePrint.transform.position = new Vector3(transform.position.x, transform.position.y, -15);

        _trashcan.SetActive(false);
        _scrollBar.SetActive(false);
        _playButton.transform.localPosition = new Vector2(321, 800);

        advancedMenu.SetActive(false);
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

        _camera.transform.position = new Vector3(0, 1, -10);
        _camera.GetComponent<Touch>().enabled = false;
        _camera.GetComponent<CameraController>().offset = Vector3.zero;
        _camera.GetComponent<CameraController>().enabled = false;

        _topWall.GetComponent<WallMesh>().enabled = false;
        _bottomWall.GetComponent<WallMesh>().enabled = false;

        _bluePrint.transform.position = new Vector3(0, 2, 9);

        _trashcan.SetActive(true);
        _scrollBar.SetActive(true);
        _playButton.transform.localPosition = new Vector2(321, 189);

        Destroy(ghost);

        objectsToToggle[0].GetComponent<GameOverMenu>().Unfreeze();

        failureScreen.SetActive(false);
    }
}
