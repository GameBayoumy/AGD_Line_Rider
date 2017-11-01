using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagingFSM : FSMEngaging {


    private Vector3 PlayerPosition;
    private Vector3 EnemyEngagingPosition;
    private bool rotated = false;
    private bool spawned = false;

    Rigidbody2D m_Rigidbody2D;


    public GameObject Player;
    public GameObject enemyBlock;

    HighScore score;
    float spawnTimer;
    private int SinusMovement = -8;

    public float speedRotation = 100F;
    public float speedSinus = .75F;


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
        Player = GameObject.Find("Player");
        base.Awake();
    }

    protected override void Initialize()
    {
        curState = FSMState.None;
       
    }

    void Start (){
        score = GameObject.Find("GameController").GetComponent<HighScore>();
    }

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

        PlayerPosition = Player.transform.position;

        if ((this.transform.position.x - Player.transform.position.x) <= 18){
            this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
        }


        if ((int)score.timeScore == 30)
        {
            curState = FSMState.Green;
        }

        if ((int)score.timeScore == 60000)
        {
            curState = FSMState.Red;
        }

        if ((int)score.timeScore == 90000)
        {
            curState = FSMState.Blue;
        }


    }







    protected void UpdateGreenState()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -120f), speedRotation);

        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, SinusMovement));


        spawnTimer -= 1;
        if (spawnTimer <= 20)
        {
            Instantiate(enemyBlock, new Vector3(this.transform.position.x - 3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            spawnTimer = 250;
        }
    }





    protected void UpdateRedState()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -240f), speedRotation);
    }

    protected void UpdateBlueState()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -360f), speedRotation);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Wall")
        {
            SinusMovement *= -1;
        }

    }

}
