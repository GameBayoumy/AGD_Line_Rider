using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour {

	public float timeScore;
	public Text timerText;
	public Text resultText;

	public Transform playerPosition;

	public GameOverMenu gameOverCall;
	public GameObject gameOverCaller;

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

		if(gameOverCall.gameOverState == true){
			if (PlayerPrefs.GetFloat ("Highscore") < timeScore) {
				PlayerPrefs.SetFloat ("Highscore", timeScore);
			}
		}
	}
}