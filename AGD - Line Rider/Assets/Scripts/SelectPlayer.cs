using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayer : MonoBehaviour {

    public int ballID;
    public string ballName;
    public float ballGravity;
    public float ballSpeed;
    public float ballBounce;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeBall()
    {
        PlayerPrefs.SetInt("BallID", ballID);
        PlayerPrefs.SetFloat("BallGravity", ballGravity);
        PlayerPrefs.SetFloat("BallBounce", ballBounce);
        PlayerPrefs.SetFloat("BallSpeed", ballSpeed);
    }

    void OnEnable()
    {
        if (PlayerPrefs.GetInt(ballName) == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
