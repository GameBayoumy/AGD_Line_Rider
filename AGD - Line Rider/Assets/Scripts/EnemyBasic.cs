using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBasic : MonoBehaviour{

    //These are essential for calling the GameOverMenu script
    public GameOverMenu gameOverCall;
    public Transform player;

    protected virtual void Awake()
    {
        //Calls the GameController which hosts the GameOverMenu script
        GameObject gameOverCaller = GameObject.Find("GameController");
        gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();
    }

    protected virtual void Update()
    {
        Movement();
    }

    protected virtual void Movement()
    {

    }

    protected virtual void DisableGameObject()
    {

    }

	protected virtual void OnCollisionEnter2D(Collision2D collision)
	{   
		if (collision.gameObject.tag == "Player")
		{
			GameOverMenu.SetGameOverState(true);
		}
	}
}
