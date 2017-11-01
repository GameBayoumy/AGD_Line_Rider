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
        PlayerPrefs.SetInt("UnlockedRope", 0);
        PlayerPrefs.SetInt("UnlockedBubbles", 0);
        PlayerPrefs.SetInt("UnlockedWave", 0);

        SelectShader();
	}
	
	// Update is called once per frame
	void Update () {    
        
		if ((int)score.timeScore == 100)
        {
            UnlockShader("UnlockedRope");
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
