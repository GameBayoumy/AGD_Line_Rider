using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySplitting : MonoBehaviour {

    public GameObject Player;
    public GameObject EnemySplittingSmallUp;
    public GameObject EnemySplittingSmallDown;
    public GameObject Up;
    public GameObject Down;
    public AudioClip splittingSFX;

    private Vector3 PlayerPosition;
    bool SpawnAllowed = true;
    bool spawned = false;

    public GameOverMenu gameOverCall;
    public GameObject gameOverCaller;

    void Awake()
    {
        //Calls the GameController which hosts the GameOverMenu script
        gameOverCaller = GameObject.Find("GameController");
        gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();
        Player = GameObject.Find("Player");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z); //puts the playerposition into a vector3

        if((transform.position.x - PlayerPosition.x) <= 15 && SpawnAllowed == true){
            spawned = true;
            Up = Instantiate(EnemySplittingSmallUp, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity); //spawns the small upper enemy
            Down = Instantiate(EnemySplittingSmallDown, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z), Quaternion.identity); //spawns the small downgoing enemy
            SpawnAllowed = false;
            SoundManager.PlaySFX(splittingSFX, "SFX");
            Destroy(gameObject);
        }

        if (spawned == true)
        {
           //adds force to make the transition more fluent 
           Up.GetComponent<Rigidbody2D>().AddForce(new Vector2(-60, 40));
           Down.GetComponent<Rigidbody2D>().AddForce(new Vector2(-60, -40));
        }
	}




    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            GameOverMenu.SetGameOverState(true);
        }
    }
}
