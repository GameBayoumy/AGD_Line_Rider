﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagingFSM : FSMEngaging {


    public GameObject Player;
    public GameObject enemyBlock;
    public GameObject enemyBumper;
    public FSMState curState;


    public int green;
    public int blue;
    public int red;
    public int stateTimer;
   
    public enum FSMState
    {
        None,
        Green,
        Red,
        Blue,
    }
    
    private bool charged = false;
    private float spawnTimer;
    private float spawnBumperTimer;
    private float launchTimer = 20f;
    private int SinusMovement = -8; //The distance of the up and down movement of Green State
    private float speedRotation = 10F; //The speed of the enemies rotation.
    private bool hasHit = false;

    
    protected override void Awake()
    {
        gameOverCaller = GameObject.Find("GameController");
        gameOverCall = gameOverCaller.GetComponent<GameOverMenu>();
        Player = GameObject.Find("Player");
    }

    protected override void Initialize()
    {
        curState = FSMState.None;
       
    }

    void Start (){
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
            if ((this.transform.position.x - Player.transform.position.x) <= 18)
            {
                this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -120f), speedRotation * Time.deltaTime);

        //Lets the enemy move up and down.
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, SinusMovement));


        //Spawns enemyblock at certain interval
        spawnTimer -= 1;
        if (spawnTimer <= 20)
        {
            Instantiate(enemyBlock, new Vector3(this.transform.position.x - 3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            spawnTimer = 250;
        }
    }

    protected void UpdateRedState()
    {
        //Rotates the sprite to the Red side
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -240f), speedRotation * Time.deltaTime);
        launchTimer -= 1;

        if (launchTimer >= 10)
        {
            if ((this.transform.position.x - Player.transform.position.x) <= 18)
            {
                this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
            }

        } else if (launchTimer < 10 && charged == false) {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, 0));
        }

        if (launchTimer == 6){
            charged = true;
        }

        if (launchTimer < 5 && charged == true)
        {
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15f, 0));
        }
    }




    protected void UpdateBlueState()
    {
        //Rotates the sprite to the Blue side
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -360f), speedRotation * Time.deltaTime);
        //Lets the enemy move up and down.
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, SinusMovement));


        //Spawns enemyblock at certain interval
        spawnBumperTimer -= 1;
        if (spawnBumperTimer <= 20)
        {
            Instantiate(enemyBumper, new Vector3(this.transform.position.x - 3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            spawnBumperTimer = 100;
        }
    }



    //Handles collision with the enemy and other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            //changes the velocity of GreenState from up/down down-up
            SinusMovement *= -1;
        }


        if (collision.gameObject.tag == "Player")
        {
            //Game Over when the player is hit
            gameOverCall.gameOverState = true;
        }
    }

}
