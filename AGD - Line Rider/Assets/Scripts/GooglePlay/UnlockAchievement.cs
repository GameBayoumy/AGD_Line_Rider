using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAchievement : MonoBehaviour {

    void OnEnable()
    {
        HighScore.OnGameOver += CheckScoreAchievement;
        Player.OnCollision += CheckCollisionAchievement;
        Player.OnResource += CheckResourceAchievement;
    }

    void OnDisable()
    {
        HighScore.OnGameOver -= CheckScoreAchievement;
        Player.OnCollision -= CheckCollisionAchievement;
        Player.OnResource -= CheckResourceAchievement;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckScoreAchievement(float theScore, int shader)
    {
        if (theScore >= 200)
        {
            PlayGamesScript.UnlockAchievement(GPGSIds.achievement_rope);
            PlayerPrefs.SetInt("UnlockedRope", 1);
        }

        if (shader != 0)
        {
            PlayerPrefs.SetInt("UnlockedRainbow", 1);
        }
    }

    void CheckCollisionAchievement(Collision2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            PlayGamesScript.UnlockAchievement(GPGSIds.achievement_cave);
            PlayerPrefs.SetInt("UnlockedCave", 1);
        }
    }

    void CheckResourceAchievement(float resource)
    {
        PlayGamesScript.UnlockAchievement(GPGSIds.achievement_bubbles);
        PlayerPrefs.SetInt("UnlockedBubbles", 1);
    }
}
