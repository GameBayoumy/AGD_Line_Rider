using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

	public Button pauseButton;
    public AudioClip explosionSFX;
    public AudioClip loseSFX;
    public Transform gameOverScreen;
	public Transform playerActivity;
	public Transform controls;
	public Transform background;

	public static bool gameOverState;

	public void Start(){
		gameOverState = false;
	}

	// Update is called once per frame
	public void Update () {
		if (gameOverState == true) {
			Freeze ();
		}
	}

	public void Freeze(){
		if (gameOverScreen.gameObject.activeInHierarchy == false) {
            gameOverScreen.gameObject.SetActive (true);                 
			Time.timeScale = 0;                                         //Causes the game to freeze in place (pausing)
            playerActivity.GetComponent<Player>().enabled = false;      //Player is turned off
            controls.GetComponent<Touch> ().enabled = false;            //Touch gameplay is turned off
            background.GetComponent<scroll> ().enabled = false;         //Scrolling background is frozen
            DisableButtonOnClick ();                                    //Disable the Pause Button
            SoundManager.PlaySFX(explosionSFX, "SFX");
            SoundManager.PlaySFX(loseSFX, "SFX");
            SoundManager.StopBGM(false, 0f);
        }
	}

	public void DisableButtonOnClick() { 
		pauseButton.interactable = false; 
	}
		
	//Resume is needed to be called before loading a scene,
	//so that all variables and Time are brought back to normal
	public void Resume(){
		gameOverState = false;
		gameOverScreen.gameObject.SetActive (false);
		Time.timeScale = 1;
		playerActivity.GetComponent<Player> ().enabled = true;
		controls.GetComponent<Touch> ().enabled = true;
		background.GetComponent<scroll> ().enabled = true;
        SoundManager.PlayBGM(GameManager.mainBGM, false, 0f);
        Debug.Log(GameManager.mainBGM);
	}

	public void Retry(){
		Resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu(){
		Resume ();
		SceneManager.LoadScene("Scene_StartMenu");
	}

	public void Quit()
	{			
		Resume ();
		Application.Quit();
	}

    public static void SetGameOverState(bool value)
    {
        gameOverState = value;
    }
}