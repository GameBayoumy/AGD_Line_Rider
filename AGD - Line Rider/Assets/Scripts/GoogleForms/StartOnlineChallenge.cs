using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartOnlineChallenge : MonoBehaviour {

    public int id;
    public string playername;
    public float score;
    public int seed;

	// Use this for initialization
	void Start () {

        transform.Find("p_name").GetComponent<Text>().text = playername;
        transform.Find("p_score").GetComponent<Text>().text = "Score: " + score.ToString();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPlayerChallenge()
    {
        PlayerPrefs.SetFloat("TargetScore", score);
        PlayerPrefs.SetInt("TargetSeed", seed);
        PlayerPrefs.SetString("TargetName", playername);
        SceneManager.LoadScene(6);
    }
}
