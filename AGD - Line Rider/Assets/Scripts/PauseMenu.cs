using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public Transform canvas;
	public Transform playerActivity;
	public Transform touchControls;
	public Transform scrollingBackground;

	public void Pause(){
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			//Causes the game to freeze in place (pausing)
			Time.timeScale = 0;
			//Player is turned off
			playerActivity.GetComponent<Player>().enabled = false;
			//Touch gameplay is turned off
			touchControls.GetComponent<Touch>().enabled = false;
			//Scrolling background is frozen
			scrollingBackground.GetComponent<scroll>().enabled = false;
			} else {
			//Everything resumes once more
			Resume();
		}
	}

	public void Resume(){
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		playerActivity.GetComponent<Player> ().enabled = true;
		touchControls.GetComponent<Touch> ().enabled = true;
		scrollingBackground.GetComponent<scroll> ().enabled = true;
	}

	public void Retry(){
		//Resume is needed to be called before loading a scene,
		//so that all variables and Time are brought back to normal
		Resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	
	public void MainMenu(){
		Resume();
		SceneManager.LoadScene("Scene_StartMenu");
	}

	public void Quit()
	{			
		Application.Quit();
	}
}