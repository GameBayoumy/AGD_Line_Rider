using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour {

	public int timeScore;
    public int currentDifficulty;
	public Text timerText;
	public Text resultText;

	public Transform playerPosition;

	public GameOverMenu gameOverCall;
	public GameObject gameOverCaller;

    public delegate void GameOverAction(float score, int ballID);
    public static event GameOverAction OnGameOver;

    bool checkedOnce = false;

	void Awake (){
		//Calls the GameController which hosts the GameOverMenu script
		GameObject gameOverCaller = GameObject.Find("GameController");     
		gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();
	}

	// Update is called once per frame
	void Update () {

		//Displays the Highscore as an integer value that is ALWAYS a positive value
		timeScore = (int)(Mathf.Abs(playerPosition.transform.position.x));
		timerText.text = timeScore.ToString("Score: 0");
		resultText.text = timeScore.ToString ("Your Score: 0");

		if(GameOverMenu.gameOverState == true && !checkedOnce){
            if (EncryptedPlayerPrefs.GetFloat ("Highscore") < timeScore) {
                EncryptedPlayerPrefs.SetFloat ("Highscore", timeScore);
			}
            if (OnGameOver != null)
                OnGameOver(timeScore, PlayerPrefs.GetInt("BallID"));

            checkedOnce = true;
        }
        if (GameOverMenu.gameOverState == false)
        {
            checkedOnce = false;
        }

        if (timeScore < 300)
        {
            currentDifficulty = 1; ;
        }
        else if (timeScore >= 300 && timeScore < 600)
        {
            currentDifficulty = 2;
        }
        else
        {
            currentDifficulty = 3;
        }
	}
}