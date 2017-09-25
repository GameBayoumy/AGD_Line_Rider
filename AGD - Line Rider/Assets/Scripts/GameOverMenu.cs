using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

	public Transform gameOverScreen;
	public Transform controls;
	public Transform background;

	public bool gameOverState;

	public void Start(){
		gameOverState = false;
	}

	// Update is called once per frame
	public void Update () {
		if (gameOverState == true) {
			Freeze ();
		} else {
//			Resume ();
//			gameOverScreen.gameObject.SetActive(false)
		}
	}

	public void Freeze(){
		if (gameOverScreen.gameObject.activeInHierarchy == false) {
			gameOverScreen.gameObject.SetActive (true);
			//Causes the game to freeze in place (pausing)
			Time.timeScale = 0;
			//Touch gameplay is turned off
			controls.GetComponent<Touch> ().enabled = false;
			//Scrolling background is frozen
			background.GetComponent<scroll> ().enabled = false;
		}
	}

	//Resume is needed to be called before loading a scene,
	//so that all variables and Time are brought back to normal
	public void Resume(){
		gameOverScreen.gameObject.SetActive (false);
		Time.timeScale = 1;
		controls.GetComponent<Touch> ().enabled = true;
		background.GetComponent<scroll> ().enabled = true;
	}

	public void Retry(){
		gameOverState = false;
		Resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu(){
		gameOverState = false;
		SceneManager.LoadScene("Scene_StartMenu");
	}

	public void Quit()
	{			
		gameOverState = false;
		Application.Quit();
	}
}