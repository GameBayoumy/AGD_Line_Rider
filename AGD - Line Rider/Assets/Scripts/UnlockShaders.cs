using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShaders : MonoBehaviour {

    public Material activeMaterial;
    public Material[] possibleMaterials;
    public GameObject skinUI;
    HighScore score;

	// Use this for initialization
	void Start () {

        score = GameObject.Find("GameController").GetComponent<HighScore>();

        PlayerPrefs.SetInt("ShaderID", PlayerPrefs.GetInt("ShaderID"));

        PlayerPrefs.SetInt("UnlockedNeon", 1);
        PlayerPrefs.SetInt("UnlockedRope", PlayerPrefs.GetInt("UnlockedRope"));
        PlayerPrefs.SetInt("UnlockedBubbles", PlayerPrefs.GetInt("UnlockedBubbles"));
        PlayerPrefs.SetInt("UnlockedWave", PlayerPrefs.GetInt("UnlockedWave"));

        SelectShader();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayerPrefs.SetInt("UnlockedRope", 0);
        }

        if ((int)score.timeScore == 200)
        {
            PlayGamesScript.UnlockAchievement(GPGSIds.achievement_rope);
            PlayerPrefs.SetInt("UnlockedRope", 1);
        }

    }

    public void UnlockShader(string shaderKey)
    {
        PlayerPrefs.SetInt(shaderKey, 1);
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
}
