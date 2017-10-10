using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : EnemyBasic {

    public float speed = 0.1f;
    protected Transform playerPos;
	// Use this for initialization
	protected override void Awake() {
        playerPos = GameObject.FindWithTag("Player").transform;
        base.Awake();
	}
	
	// Update is called once per frame
	protected override void Update () {

        float distance = Vector2.Distance(playerPos.position, transform.position);

        if (distance > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed);
        }
        base.Update();
	}
}
