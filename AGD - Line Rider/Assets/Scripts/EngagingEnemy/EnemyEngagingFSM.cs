using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagingFSM : FSMEngaging {


    private Vector3 PlayerPosition;
    private Vector3 EnemyEngagingPosition;
    private bool rotated = false;

    public GameObject Player;

    HighScore score;

    //public Transform from;
    //public Transform to;
    public float speedRotation = 100F;


    public enum FSMState
    {
        None,
        Green,
        Red,
        Blue,
    }

    public FSMState curState;



    //protected override void Awake()
    //{
    //    Player = GameObject.Find("Player");
    //    base.Awake();
    //}

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

        //if (curState == FSMState.None)
        //{
        //    Debug.Log("None");
        //}

        //if (curState == FSMState.Green){
        //    Debug.Log("Green");
        //}

        //if (curState == FSMState.Red)
        //{
        //    Debug.Log("Red");
        //}

        //if (curState == FSMState.Blue)
        //{
        //    Debug.Log("Blue");
        //}


        PlayerPosition = Player.transform.position;

        if ((this.transform.position.x - Player.transform.position.x) <= 18){
            this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
        }

       // this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);


        //EnemyEngagingPosition = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);

        //if (this.transform.position.x <= Player.transform.position.x + 20)
        //{
        //    this.transform.position = Vector3.Lerp(transform.position, EnemyEngagingPosition, speedRotation);
        //}


        if ((int)score.timeScore == 30)
        {
            curState = FSMState.Green;
        }

        if ((int)score.timeScore == 60)
        {
            curState = FSMState.Red;
        }

        if ((int)score.timeScore == 90)
        {
            curState = FSMState.Blue;
        }


    }

    protected void UpdateGreenState()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -120f), speedRotation);
    }

    protected void UpdateRedState()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -240f), speedRotation);
    }

    protected void UpdateBlueState()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.x, -360f), speedRotation);
    }



}
