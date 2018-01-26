using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public Text highScoreText;

	public void Start(){
		//Showcases biggest highscore
		highScoreText.text = "Highscore: " + (PlayerPrefs.GetFloat ("Highscore")).ToString();
	}

	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void StartShopMenu()
	{
		SceneManager.LoadScene (4);
	}

	public void Quit()
	{
		Application.Quit();
	}

    public void StartEditor()
    {
        SceneManager.LoadScene(2);
    }

    public void StartCustomGame()
    {
        SceneManager.LoadScene(3);
    }

	public void StartTutorialMenu(){
		SceneManager.LoadScene (5);
	}

	//THIS IS RESERVED FOR THE TUTORIAL LEVELS
	public void StartTutorialLevel_1(){
		SceneManager.LoadScene (6);
	}

	public void StartTutorialLevel_2(){
		SceneManager.LoadScene (7);
	}

	public void StartTutorialLevel_3(){
		SceneManager.LoadScene (8);
	}

	public void StartMenuReturn(){
		SceneManager.LoadScene (0);
	}
}