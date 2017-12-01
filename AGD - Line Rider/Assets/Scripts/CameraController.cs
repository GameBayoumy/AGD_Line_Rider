using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {


	public GameObject Player;       //Public variable to store a reference to the player game object
	public Vector3 offset;         //Private variable to store the offset distance between the player and camera

	//These are essential for calling the GameOverMenu script
	public GameOverMenu gameOverCall;
	public GameObject gameOverCaller;

	private void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		//Calls the GameController which hosts the GameOverMenu script
		GameObject gameOverCaller = GameObject.Find("GameController");     
		gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();  
	}


	// Use this for initialization
	void Start () {
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
  //      if (Player.GetComponent<Rigidbody2D>().velocity.x <= 8){
		//	offset += new Vector3(0.02f * Time.timeScale,0,0);
		//} else if(Player.GetComponent<Rigidbody2D>().velocity.x > 8){
			//offset -= new Vector3(0.02f * Time.timeScale, 0, 0);
        //}

        if (offset.x <= 0){
			offset = new Vector3(0, 0, -2);
        }
		//If the scrolling screen catches on with the player, a Game Over results
		if (offset.x >= 23){
			GameOverMenu.SetGameOverState(true);
		}
	}

	// LateUpdate is called after Update each frame
	void LateUpdate()
	{
        // Set the position of the camera's transform to be the same as the player's x position, but offset by the calculated offset distance.
        this.transform.position = new Vector3(Player.GetComponent<Rigidbody2D>().position.x + offset.x, this.transform.position.y, this.transform.position.z);
	}
}