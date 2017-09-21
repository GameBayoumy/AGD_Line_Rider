using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {


	public GameObject Player;       //Public variable to store a reference to the player game object
	public Vector3 offset;         //Private variable to store the offset distance between the player and camera



	private void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}


	// Use this for initialization
	void Start () {
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.GetComponent<Rigidbody2D>().velocity.x <= 8){
            offset += new Vector3(0.02f,0,0);

            Debug.Log(offset);
        }

        if (offset.x >= 23){
            SceneManager.LoadScene("LineMeshTest");
        }

        if(Player.GetComponent<Rigidbody2D>().velocity.x >= 8){
            offset -= new Vector3(0.02f, 0, 0);
        }

        if (offset.x <= 0){
            offset = new Vector3(0, 0, -10);
        }
	}

	// LateUpdate is called after Update each frame
	void LateUpdate()
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		transform.position = Player.transform.position + offset;
	}
}
