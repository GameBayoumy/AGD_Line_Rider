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

    GameObject _customSet;
    GameObject _ghostSet;
    GameObject _setEnd;
    Vector3 _originalPos;
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
        _customSet = GameObject.Find("CustomSet");
        _originalPos = transform.position;
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
        _ghostSet = Instantiate(_customSet, new Vector2(0, -2.47f), Quaternion.identity);
        _setEnd = _ghostSet.transform.Find("set_end").gameObject;
        _ghostSet.SetActive(false);
        _ghostSet.gameObject.name = "GhostSet";
        test.ghost = _ghostSet;
    }

    public void SetEndPosition()
    {
        horizontalPos = -22;

        foreach (Transform t in _customSet.transform)
        {
            if (t.transform.position.x > horizontalPos)
            {
                horizontalPos = t.transform.position.x;
            }
        }

        transform.position = new Vector2(horizontalPos + 10, -4.38f);
        _setEnd.transform.position = new Vector2(horizontalPos + 30, 0);

    }

    public void SetOriginalPosition()
    {
        transform.position = _originalPos;
        timer = 20;
    }

    public void SaveCustomSet()
    {
        _ghostSet.SetActive(true);

#if UNITY_EDITOR
        Object newPrefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Sets/custom/custom_set" + PlayerPrefs.GetInt("PrefabNo") + ".prefab");
        PrefabUtility.ReplacePrefab(_ghostSet, newPrefab, ReplacePrefabOptions.ConnectToPrefab);
#endif

        if (!Application.isEditor)
        {
            PlayerPrefs.SetInt("Set" + PlayerPrefs.GetInt("PrefabNo") + "Amount", _ghostSet.transform.childCount);

            for (int i = 0; i < _ghostSet.transform.childCount; i++)
            {
                Transform currentChild = _ghostSet.transform.GetChild(i);
                string objectName = currentChild.name;
                if (i != 0)
                    objectName = objectName.Substring(0, objectName.Length - 7); //This line removes the word '(Clone)' from the object's name.

                PlayerPrefs.SetString("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1), objectName);
                PlayerPrefs.SetFloat("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1) + "X", _ghostSet.transform.GetChild(i).position.x);
                PlayerPrefs.SetFloat("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1) + "Y", _ghostSet.transform.GetChild(i).position.y - 2.2088f);
                PlayerPrefs.SetFloat("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1) + "Size", _ghostSet.transform.GetChild(i).localScale.y);
                PlayerPrefs.SetFloat("Set" + PlayerPrefs.GetInt("PrefabNo") + "Object" + (i + 1) + "Rotation", _ghostSet.transform.GetChild(i).localEulerAngles.z);
            }
        }

        PlayerPrefs.SetInt("PrefabNo", PlayerPrefs.GetInt("PrefabNo") + 1);
        _ghostSet.SetActive(false);
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
