using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShaders : MonoBehaviour {

    public Material activeMaterial;
    public Material[] possibleMaterials;
    public GameObject skinUI;
    HighScore _score;

	// Use this for initialization
	void Start () {

        _score = GameObject.Find("GameController").GetComponent<HighScore>();

        PlayerPrefs.SetInt("ShaderID", PlayerPrefs.GetInt("ShaderID"));

        PlayerPrefs.SetInt("UnlockedNeon", 1);
        PlayerPrefs.SetInt("UnlockedRope", PlayerPrefs.GetInt("UnlockedRope"));
        PlayerPrefs.SetInt("UnlockedBubbles", PlayerPrefs.GetInt("UnlockedBubbles"));
        PlayerPrefs.SetInt("UnlockedCave", PlayerPrefs.GetInt("UnlockedCave"));
        PlayerPrefs.SetInt("UnlockedRainbow", PlayerPrefs.GetInt("UnlockedRainbow"));

        SelectShader();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.G))
        {
            ResetAllProgress();
        }

    }

    public void SelectShader()
    {
        activeMaterial = possibleMaterials[PlayerPrefs.GetInt("ShaderID")];
    }

    public void RevealSkinUI()
    {
        skinUI.SetActive(true);
    }

    public void HideSkinUI()
    {
        skinUI.SetActive(false);
    }

    public void ResetAllProgress()
    {
        PlayerPrefs.SetInt("UnlockedRope", 0);
        PlayerPrefs.SetInt("UnlockedBubbles", 0);
        PlayerPrefs.SetInt("UnlockedCave", 0);
        PlayerPrefs.SetInt("UnlockedRainbow", 0);
    }
}
