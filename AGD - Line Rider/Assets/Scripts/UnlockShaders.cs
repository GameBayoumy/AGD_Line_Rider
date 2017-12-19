using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShaders : MonoBehaviour
{

    public Material activeMaterial;
    public Material[] possibleMaterials;
    public GameObject skinUI;
    public GameObject ballUI;
    HighScore _score;

    // Use this for initialization
    void Start()
    {

        PlayerPrefs.SetInt("ShaderID", PlayerPrefs.GetInt("ShaderID"));

        PlayerPrefs.SetInt("UnlockedNeon", 1);
        PlayerPrefs.SetInt("UnlockedRope", PlayerPrefs.GetInt("UnlockedRope"));
        PlayerPrefs.SetInt("UnlockedBubbles", PlayerPrefs.GetInt("UnlockedBubbles"));
        PlayerPrefs.SetInt("UnlockedCave", PlayerPrefs.GetInt("UnlockedCave"));
        PlayerPrefs.SetInt("UnlockedRainbow", PlayerPrefs.GetInt("UnlockedRainbow"));

        PlayerPrefs.SetInt("UnlockedNormal", 1);
        PlayerPrefs.SetInt("UnlockedBounce", PlayerPrefs.GetInt("UnlockedBounce"));
        PlayerPrefs.SetInt("UnlockedGravity", PlayerPrefs.GetInt("UnlockedGravity"));
        PlayerPrefs.SetInt("UnlockedWeightless", PlayerPrefs.GetInt("UnlockedWeightless"));
        PlayerPrefs.SetInt("UnlockedSnail", PlayerPrefs.GetInt("UnlockedSnail"));

        SelectShader();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            ResetAllProgress();
        }

    }

    public void UnlockShader(string shaderKey)
    {
        PlayerPrefs.SetInt(shaderKey, 1);
    }

    public void SelectShader()
    {
        activeMaterial = possibleMaterials[PlayerPrefs.GetInt("Activated LineSkin")];
    }

    public void RevealSkinUI()
    {
        skinUI.SetActive(true);
    }

    public void HideSkinUI()
    {
        skinUI.SetActive(false);
    }

    public void RevealBallUI()
    {
        ballUI.SetActive(true);
    }

    public void HideBallUI()
    {
        ballUI.SetActive(false);
    }

    public void ResetAllProgress()
    {
        PlayerPrefs.SetInt("UnlockedRope", 0);
        PlayerPrefs.SetInt("UnlockedBubbles", 0);
        PlayerPrefs.SetInt("UnlockedCave", 0);
        PlayerPrefs.SetInt("UnlockedRainbow", 0);

        PlayerPrefs.SetInt("UnlockedBounce", 0);
        PlayerPrefs.SetInt("UnlockedGravity", 0);
        PlayerPrefs.SetInt("UnlockedWeightless", 0);
        PlayerPrefs.SetInt("UnlockedSnail", 0);
    }
}
