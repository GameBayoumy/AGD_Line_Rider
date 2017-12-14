using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public Text highScoreText;

	public void Start(){
		//Showcases biggest highscore
		highScoreText.text = "Highscore: " + ((int)PlayerPrefs.GetFloat ("Highscore")).ToString();
	}

	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void StartShopMenu()
	{
		SceneManager.LoadScene (2);
	}

	public void Quit()
	{
		Application.Quit();
	}
}