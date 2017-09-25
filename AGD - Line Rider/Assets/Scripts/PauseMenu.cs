using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public Transform canvas;
	public Transform controls;
	public Transform background;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Pause ();
		}
	}

	public void Pause(){
		if (canvas.gameObject.activeInHierarchy == false) {
			canvas.gameObject.SetActive (true);
			//Causes the game to freeze in place (pausing)
			Time.timeScale = 0;
			//Touch gameplay is turned off
			controls.GetComponent<Touch>().enabled = false;
			//Scrolling background is frozen
			background.GetComponent<scroll>().enabled = false;
			} else {
			//Everything resumes once more
			Resume();
		}
	}

	public void Resume(){
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		controls.GetComponent<Touch> ().enabled = true;
		background.GetComponent<scroll> ().enabled = true;
	}
	
	public void MainMenu(){
		SceneManager.LoadScene(0);
	}

	public void Quit()
	{			
		Application.Quit();
	}

}