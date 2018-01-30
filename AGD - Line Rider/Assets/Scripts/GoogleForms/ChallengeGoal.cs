using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeGoal : MonoBehaviour {

    public HighScore controller;
    public GameOverMenu gameOver;
    public GameObject winScreen;
    public Text winText;

    bool won;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector2(PlayerPrefs.GetFloat("TargetScore") + 1, 3.31f);

        if (controller.timeScore == transform.position.x && !won)
        {
            PlayerPrefs.SetInt("PlayersDefeated", PlayerPrefs.GetInt("PlayersDefeated") + 1);
            gameOver.Freeze();
            GameObject.Find("Canvas_GameOverScreen").SetActive(false);
            winScreen.SetActive(true);
            winText.text = "You defeated " + PlayerPrefs.GetString("TargetName");
            won = true;
        }
		
	}
}
