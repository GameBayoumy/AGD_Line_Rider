using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBasic : SpawnableGameObject {
   
   
	//// Use this for initialization
	//void Start () {
	////	GetComponent<ObjectPoolScript>();
	//}
	
	//// Update is called once per frame
	//void Update () {
	//	//SpawnObject(EnemyBasicPool);
	//}

	//These are essential for calling the GameOverMenu script
	public GameOverMenu gameOverCall;
	public GameObject gameOverCaller;

	void Awake(){
		//Calls the GameController which hosts the GameOverMenu script
		GameObject gameOverCaller = GameObject.Find("GameController");     
		gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();  
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{   


		if (collision.gameObject.tag == "Player")
		{
//			SceneManager.LoadScene("LineMeshTest");
			gameOverCall.gameOverState = true;
		}
	}
}
