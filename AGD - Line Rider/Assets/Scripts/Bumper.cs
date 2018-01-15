using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

   // public float targetTime = 0.1f;
    public AudioClip bounceSFX;
    public float scaleSize;

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "HamsterBall")
        {
            SoundManager.PlaySFXRandomized(bounceSFX, "SFX");
            transform.localScale = new Vector3(transform.localScale.x * scaleSize, transform.localScale.y * scaleSize, transform.localScale.z * 1.0f);
            hit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "HamsterBall")
        {
            SoundManager.PlaySFXRandomized(bounceSFX, "SFX");
            transform.localScale = new Vector3(transform.localScale.x / scaleSize, transform.localScale.y / scaleSize, transform.localScale.z / 1.0f);
            hit = true;
        }
    }
}
