using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagingFSM : FSMEngaging {
    public enum FSMState
    {
        None,
        Green,
        Red,
        Blue,
    }
    public FSMState curState;

    public GameObject player;
    public GameObject enemyBlock;
    public GameObject enemyBumper;
   
    public int green;
    public int blue;
    public int red;
    public int stateTimer;
    private int _sinusMovement = -8; 
   
    private bool _charged = false;

    private float _spawnTimer;
    private float _spawnBumperTimer;
    private float _launchTimer = 20f;
    private float _speedRotation = 10F;


    protected override void Awake()
    {
        gameOverCaller = GameObject.Find("GameController");
        gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();
        player = GameObject.Find("Player");
    }

    protected override void Initialize()
    {
        //Sets the starting state to None
        curState = FSMState.None; 
    }

    //switch between the different states
    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Green:
                UpdateGreenState();
                break;
            case FSMState.Red:
                UpdateRedState();
                break;
            case FSMState.Blue:
                UpdateBlueState();
                break;
        }

        stateTimer++;

        //Makes sure that the enemy floats in front of the player at a fixed distance at certain states
        if (curState == FSMState.None || curState == FSMState.Green || curState == FSMState.Blue)
        {
            if ((this.transform.position.x - player.transform.position.x) <= 18)
            {
                this.transform.position = new Vector3(player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
            }
        }
       
        //currently how different states are switched
        if (stateTimer > green)
        {
            curState = FSMState.Green;
        }

        if (stateTimer > blue)
        {
            curState = FSMState.Blue;
        }

        if (stateTimer > red)
        {
            curState = FSMState.Red;
        }
    }

    //Behaviour of greenstate
    protected void UpdateGreenState()
    {
        //Rotates the sprite to the Green side
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -120f), _speedRotation * Time.deltaTime);

        //Lets the enemy move up and down.
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, _sinusMovement));

        //Spawns enemyblock at certain interval
        _spawnTimer -= 1;
        if (_spawnTimer <= 20)
        {
            GameObject newBlock = Instantiate(enemyBlock, new Vector3(this.transform.position.x - 3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            newBlock.tag = "Spawnables";
            _spawnTimer = 250;
        }
    }

    protected void UpdateRedState()
    {
        //Rotates the sprite to the Red side
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -240f), _speedRotation * Time.deltaTime);
        _launchTimer -= 1;

        if (_launchTimer >= 10)
        {
            if ((this.transform.position.x - player.transform.position.x) <= 18)
            {
                this.transform.position = new Vector3(player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
            }

        } else if (_launchTimer < 10 && _charged == false) 
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, 0));
        }

        if (_launchTimer == 6)
        {
            _charged = true;
        }

        if (_launchTimer < 5 && _charged == true)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15f, 0));
        }
    }

    protected void UpdateBlueState()
    {
        //Rotates the sprite to the Blue side
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -360f), _speedRotation * Time.deltaTime);
        //Lets the enemy move up and down.
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, _sinusMovement));

        //Spawns enemyblock at certain interval
        _spawnBumperTimer -= 1;
        if (_spawnBumperTimer <= 20)
        {
            GameObject newBumper = Instantiate(enemyBumper, new Vector3(this.transform.position.x - 3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            newBumper.tag = "Spawnables";
            _spawnBumperTimer = 100;
        }
    }

    //Handles collision with the enemy and other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            //changes the velocity of GreenState from up/down down-up
            _sinusMovement *= -1;
        }

        if (collision.gameObject.tag == "Player")
        {
            //Game Over when the player is hit
            GameOverMenu.SetGameOverState(true);
        }
    }
}
