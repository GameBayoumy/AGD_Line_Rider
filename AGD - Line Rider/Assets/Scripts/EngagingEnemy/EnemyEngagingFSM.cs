using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagingFSM : FSMEngaging {



    private Vector3 EnemyEngagingPosition;
    private bool rotated = false;
    private bool spawned = false;

    Rigidbody2D m_Rigidbody2D;






    private Vector3 PlayerPosition; //Holds the position of the Player

    public GameObject Player;
    public GameObject enemyBlock;

    HighScore score;
    private float spawnTimer;

    private int SinusMovement = -8; //The distance of the up and down movement of Green State

    private float speedRotation = 100F; //The speed of the enemies rotation.
    private float speedSinus = .75F;

    private bool hasHit = false;
    private bool hasReturned = true;




    public float speed = .5f;
    private float fraction = 0; 
    private Vector3 start;
    private Vector3 des;




    public enum FSMState
    {
        None,
        Green,
        Red,
        Blue,
    }

    public FSMState curState;



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
        score = GameObject.Find("GameController").GetComponent<HighScore>();
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

        //stores the position of the player
        PlayerPosition = Player.transform.position;

        //Makes sure that the enemy floats in front of the player at a fixed distance at certain states
        if (curState == FSMState.None || curState == FSMState.Green)
        {
            if ((this.transform.position.x - Player.transform.position.x) <= 18)
            {
                this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
            }
        }


        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        des = new Vector3(Player.transform.position.x + 18, transform.position.y, transform.position.z);



        //currently how different states are switched
        if ((int)score.timeScore == 30)
        {
            curState = FSMState.Green;
        }

        if ((int)score.timeScore == 60000)
        {
            curState = FSMState.Red;
        }

        if ((int)score.timeScore == 150)
        {
            curState = FSMState.Blue;
        }


    }






    //Behaviour of greenstate
    protected void UpdateGreenState()
    {
        //Rotates the sprite to the Green side
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -120f), speedRotation);

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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -240f), speedRotation);
    }

    protected void UpdateBlueState()
    {
        //Rotates the sprite to the Blue side
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -360f), speedRotation);


        if(hasHit == true){
     

            if (fraction < 1)
            {
                fraction += Time.deltaTime * speed;

                if ((this.transform.position.x - Player.transform.position.x) <= 17.8)
                {
                    transform.position = Vector3.Lerp(start, des, fraction);
                } else  {
                    hasHit = false;
                    hasReturned = true;

                }
            }

        }

        if (hasReturned == true){
            this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
            
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
            gameOverCall.gameOverState = true;
        }

        if (collision.gameObject.name == "Trail")
        {
            hasHit = true;
        }

    }

}
