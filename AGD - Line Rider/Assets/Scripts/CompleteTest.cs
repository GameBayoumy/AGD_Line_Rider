using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CompleteTest : MonoBehaviour {

    public GameObject successScreen;
    public GameObject failureScreen;
    public GameOverMenu controller;
    public TestLevel test;
    public GameObject player;

    GameObject customSet;
    GameObject ghostSet;
    Vector3 originalPos;
    float horizontalPos;
    public int timer;

	// Use this for initialization
	void Start () {
        customSet = GameObject.Find("CustomSet");
        originalPos = transform.position;
        horizontalPos = -22;
        timer = 20;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.SetInt("PrefabNo", 1);
        }

        if (player.transform.position.x > transform.position.x)
        {
            controller.Freeze();
            successScreen.SetActive(true);
            failureScreen.SetActive(false);
        }

        if (test.testInProgress && timer >= 0)
        {
            timer -= 1;
        }

        if (timer == 15)
        {
            CreateDuplicate();
        }

        if (timer == 0)
        {
            SetEndPosition();
        }

        if (!test.testInProgress)
        {
            SetOriginalPosition();
            successScreen.SetActive(false);
        }
	}

    public void CreateDuplicate()
    {
        ghostSet = Instantiate(customSet, Vector3.zero, Quaternion.identity);
        ghostSet.SetActive(false);
        ghostSet.gameObject.name = "GhostSet";
        test.ghost = ghostSet;
    }

    public void SetEndPosition()
    {
        horizontalPos = -22;

        foreach (Transform t in customSet.transform)
        {
            if (t.transform.position.x > horizontalPos && t.gameObject.name != "NullObject(Clone)")
            {
                horizontalPos = t.transform.position.x;
            }
        }

        transform.position = new Vector2(horizontalPos + 3, -4.38f);

    }

    public void SetOriginalPosition()
    {
        transform.position = originalPos;
        timer = 20;
    }

    public void SaveCustomSet()
    {
        ghostSet.SetActive(true);
        Object newPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Sets/custom/custom_set" + PlayerPrefs.GetInt("PrefabNo") + ".prefab");
        PrefabUtility.ReplacePrefab(ghostSet, newPrefab, ReplacePrefabOptions.ConnectToPrefab);
        PlayerPrefs.SetInt("PrefabNo", PlayerPrefs.GetInt("PrefabNo") + 1);
        ghostSet.SetActive(false);
    }
}
