using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAchievement : MonoBehaviour
{

    void OnEnable()
    {
        HighScore.OnGameOver += CheckScoreAchievement;
        Player.OnCollision += CheckCollisionAchievement;
        Player.OnResource += CheckResourceAchievement;
    }

    //Unsubscribing to events upon disabling the objects prevents errors and memory leaks.
    void OnDisable()
    {
        HighScore.OnGameOver -= CheckScoreAchievement;
        Player.OnCollision -= CheckCollisionAchievement;
        Player.OnResource -= CheckResourceAchievement;
    }

    void CheckScoreAchievement(float theScore, int ballType)
    {
        if (theScore >= 200)
        {
            PlayGamesScript.UnlockAchievement(GPGSIds.achievement_bounce);
            PlayerPrefs.SetInt("UnlockedRope", 1);
            PlayerPrefs.SetInt("UnlockedBounce", 1);

        }

        if (ballType != 0)
        {
            PlayGamesScript.UnlockAchievement(GPGSIds.achievement_snail);
            PlayerPrefs.SetInt("UnlockedRainbow", 1);
            EncryptedPlayerPrefs.SetInt("UnlockedSnailEncrypted", 1);

        }
    }

    void CheckCollisionAchievement(Collision2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            PlayGamesScript.UnlockAchievement(GPGSIds.achievement_weightless);
            PlayerPrefs.SetInt("UnlockedCave", 1);
            PlayerPrefs.SetInt("UnlockedWeightless", 1);
        }
    }

    void CheckResourceAchievement(float resource)
    {
        PlayGamesScript.UnlockAchievement(GPGSIds.achievement_ascending);
        PlayerPrefs.SetInt("UnlockedBubbles", 1);
        PlayerPrefs.SetInt("UnlockedGravity", 1);
    }
}
