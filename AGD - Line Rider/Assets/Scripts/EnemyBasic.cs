using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBasic : SpawnableGameObject {

    //These are essential for calling the GameOverMenu script
    public GameOverMenu gameOverCall;
    public GameObject gameOverCaller;



    protected override void Awake()
    {
        //Calls the GameController which hosts the GameOverMenu script
        GameObject gameOverCaller = GameObject.Find("GameController");
        gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();
        base.Awake();

    }
    protected override void Update() { base.Update(); }
    protected override void DisableGameObject() { base.DisableGameObject(); }
    protected override void Reset()
    {

        base.Reset(); 
    }


    private void OnCollisionEnter2D(Collision2D collision)
	{   
		if (collision.gameObject.tag == "Player")
		{
//			SceneManager.LoadScene("LineMeshTest");
			GameOverMenu.SetGameOverState(true);
		}
	}
}
