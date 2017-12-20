using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour {

	public float timeScore;
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

//			var move = new Vector3(Input.GetAxis("Horizontal"), 0 , 0);
//			transform.position += move * movementSpeed * Time.deltaTime;
//			if (Input.GetButtonDown("Jump"))
//			{
//				rb.AddForce(0, jumpPower, 0);
//			}

		timeScore = playerPosition.transform.position.x;
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

        if (timeScore < 150)
        {
            currentDifficulty = 1; ;
        }
        else if (timeScore >= 150 && timeScore < 299)
        {
            currentDifficulty = 2;
        }
        else
        {
            currentDifficulty = 3;
        }
	}
}