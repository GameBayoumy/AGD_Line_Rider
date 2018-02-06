using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySinus : EnemyBasic {

    private int SinusMovement = -20;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Movement()
    {
        base.Movement();

        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, SinusMovement));
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        SinusMovement *= -1;
    }
}