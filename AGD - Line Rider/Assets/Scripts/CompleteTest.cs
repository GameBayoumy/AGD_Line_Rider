using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class CompleteTest : MonoBehaviour {

    public GameObject successScreen;
    public GameObject failureScreen;
    public GameOverMenu controller;
    public TestLevel test;
    public GameObject player;
    public int goToNo;

    GameObject customSet;
    GameObject ghostSet;
    GameObject setEnd;
    Vector3 originalPos;
    float horizontalPos;
    public int timer;

    public int setToRemove;
    public int objectToRemove;

    void Awake()
    {
        if (PlayerPrefs.GetInt("PrefabNo") == 0)
        {
            PlayerPrefs.SetInt("PrefabNo", 1);
        }
    }

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
            PlayerPrefs.SetInt("PrefabNo", goToNo);
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            RemoveFromPrefs();
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
        setEnd = ghostSet.transform.Find("set_end").gameObject;
        ghostSet.SetActive(false);
        ghostSet.gameObject.name = "GhostSet";
        test.ghost = ghostSet;
    }

    public void SetEndPosition()
    {
        horizontalPos = -22;

        foreach (Transform t in customSet.transform)
        {
            if (t.transform.position.x > horizontalPos)
            {
                horizontalPos = t.transform.position.x;
            }
        }

        transform.position = new Vector2(horizontalPos + 3, -4.38f);
        setEnd.transform.position = new Vector2(horizontalPos + 30, 0);

    }

    public void SetOriginalPosition()
    {
        transform.position = originalPos;
        timer = 20;
    }

    public void SaveCustomSet()
    {
        ghostSet.SetActive(true);

#if UNITY_EDITOR
        Object newPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Sets/custom/custom_set" + PlayerPrefs.GetInt("PrefabNo") + ".prefab");
        PrefabUtility.ReplacePrefab(ghostSet, newPrefab, ReplacePrefabOptions.ConnectToPrefab);
#endif

        if (!Application.isEditor)
        {
            PlayerPrefs.SetInt("Set" + PlayerPrefs.GetInt("PrefabNo") + "Amount", ghostSet.transform.childCount);

            for (int i = 0; i < ghostSet.transform.childCount; i++)
            {
                Transform currentChild = ghostSet.transform.GetChild(i);
                string objectName = currentChild.name;
                if (i != 0)
                    objectName = objectName.Substring(0, objectName.Length - 7); //This line removes the word '(Clone)' from the object's name.

                PlayerPrefs.SetString("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1), objectName);
                PlayerPrefs.SetFloat("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1) + "X", ghostSet.transform.GetChild(i).position.x);
                PlayerPrefs.SetFloat("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1) + "Y", ghostSet.transform.GetChild(i).position.y - 2.2088f);
            }
        }

        PlayerPrefs.SetInt("PrefabNo", PlayerPrefs.GetInt("PrefabNo") + 1);
        ghostSet.SetActive(false);
        controller.Unfreeze();
        SceneManager.LoadScene(0);
    }

    void RemoveFromPrefs()
    {
        PlayerPrefs.DeleteKey("Set" + setToRemove + "Amount");
        PlayerPrefs.DeleteKey("Set" + setToRemove + "Object" + objectToRemove);
        PlayerPrefs.DeleteKey("Set" + setToRemove + "Object" + objectToRemove + "X");
        PlayerPrefs.DeleteKey("Set" + setToRemove + "Object" + objectToRemove + "Y");
    }
}
