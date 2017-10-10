using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallMesh : MonoBehaviour
{

    public GameObject Player;
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

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
//           SceneManager.LoadScene("LineMeshTest");
//			Player.GetComponent<Collider2D>().enabled = false;
			gameOverCall.gameOverState = true;
        }
    }
}