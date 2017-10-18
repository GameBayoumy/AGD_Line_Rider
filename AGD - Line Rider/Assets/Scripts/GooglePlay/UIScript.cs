using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public static UIScript Instance { get; private set; }

	// Use this for initialization
	void Start () {
        Instance = this;
        PlayGamesScript.LogIn();
	}

    [SerializeField]

    public void SignInUser()
    {
        PlayGamesScript.LogIn();
    }

    public void RevealAchievementsUI()
    {
        GameObject.Find("Username").GetComponent<Text>().text = Social.localUser.userName;
        Social.ShowAchievementsUI();
    }

    public void Increment()
    {
        //PlayGamesScript.IncrementAchievement(GPGSIds.achievement_math_wizard, 1);
        GameObject.Find("BananaText").GetComponent<Text>().text = "User: " + Social.localUser.userName + "...";
    }

    public void Unlock()
    {
        //PlayGamesScript.UnlockAchievement(GPGSIds.achievement_test3);
    }

    public void ShowAchievements()
    {
        PlayGamesScript.ShowAchievementsUI();
    }

    public void TestButton()
    {
        Debug.Log("Works fine.");
    }
}
