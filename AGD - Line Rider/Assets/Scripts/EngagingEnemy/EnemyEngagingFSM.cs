using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagingFSM : FSMEngaging {


    private Vector3 PlayerPosition;
    private Vector3 EnemyEngagingPosition;

    public GameObject Player;

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
    }

    protected void UpdateGreenState()
    {
        //if (groen == true)
        //{
        //    curState = FSMState.Green;
        //}
    }

    protected void UpdateRedState()
    {

    }

    protected void UpdateBlueState()
    {

    }

    void Update()
    {
        if (curState == FSMState.None)
        {
            Debug.Log("None");
        }

        if (curState == FSMState.Green){
            Debug.Log("Green");
        }

        if (curState == FSMState.Red)
        {
            Debug.Log("Red");
        }

        if (curState == FSMState.Blue)
        {
            Debug.Log("Blue");
        }


        PlayerPosition = Player.transform.position;

        this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
    }
}
