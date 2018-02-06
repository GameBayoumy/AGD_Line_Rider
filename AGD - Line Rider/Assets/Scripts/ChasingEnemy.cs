using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemy : EnemyBasic {

    public float speed = 0.25f;
    private bool hasPassed;
    private bool shouldStop;
	
    // Use this for initialization
	protected override void Awake() {
        base.Awake();
	}

    protected override void Movement()
    {
        base.Movement();

        if (((this.transform.position.x - player.transform.position.x) < -15f) || hasPassed == true && shouldStop == false)
        {
            hasPassed = true;
            float distance = Vector2.Distance(player.position, transform.position);

            if (distance > 1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed);
            }
        }

        if (((this.transform.position.x - player.transform.position.x) < -15f) && hasPassed == false)
        {
            shouldStop = true;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
