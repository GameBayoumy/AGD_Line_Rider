using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour {

	public float timeScore;
	public Text timerText;

	// Update is called once per frame
	void Update () {

//			var move = new Vector3(Input.GetAxis("Horizontal"), 0 , 0);
//			transform.position += move * movementSpeed * Time.deltaTime;
//			if (Input.GetButtonDown("Jump"))
//			{
//				rb.AddForce(0, jumpPower, 0);
//			}
		timeScore += Time.deltaTime;
		timerText.text = timeScore.ToString("Score: 0");
	}
}