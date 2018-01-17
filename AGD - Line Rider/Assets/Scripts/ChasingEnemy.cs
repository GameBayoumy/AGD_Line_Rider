using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : EnemyBasic {

    float speed = 0.20f;
    protected Transform playerPos;
    private Vector3 PlayerPosition;
    public GameObject Player;
    private bool hasPassed;
    private bool shouldStop;
	// Use this for initialization


	protected override void Awake() {
        playerPos = GameObject.FindWithTag("Player").transform;
        base.Awake();
           Player = GameObject.Find("Player");
	}

    protected override void Reset()
    {
        hasPassed = false;
        shouldStop = false;

        base.Reset();
    }
    // Update is called once per frame
    protected override void Update () {

        PlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z); //puts the playerposition into a vector3

        if (((this.transform.position.x - Player.transform.position.x) < -15f) || hasPassed == true && shouldStop == false)
        {
            hasPassed = true;
            float distance = Vector2.Distance(playerPos.position, transform.position);

            if (distance > 1f)
            {
				if (Time.timeScale == 1) {
					transform.position = Vector2.MoveTowards (transform.position, playerPos.position, speed);
				}
            }
        }

        if(((this.transform.position.x - Player.transform.position.x) < -15f) && hasPassed == false){
            shouldStop = true;
        }

        base.Update();
	}
}