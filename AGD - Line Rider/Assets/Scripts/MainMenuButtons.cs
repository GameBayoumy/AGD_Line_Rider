using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public Text highScoreText;
    public Text victoryText;

	public void Start(){
		//Showcases biggest highscore
		highScoreText.text = "Highscore: " + (PlayerPrefs.GetFloat ("Highscore")).ToString();
        victoryText.text = "Players defeated: " + PlayerPrefs.GetInt("PlayersDefeated");
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

    public void StartOnlineMode()
    {
        SceneManager.LoadScene(5);
    }
}