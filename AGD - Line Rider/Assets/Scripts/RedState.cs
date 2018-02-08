using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedState : State {

    private float _launchTimer = 20f;
    private bool _charged;
    private EnemyEngagingFSM engagingEnemy;

    public RedState(GameObject gameObject) : base(gameObject)
    {
    }

    public override void Tick()
    {
        //Rotates the sprite to the Red side
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.x, -240f), engagingEnemy.speedRotation * Time.deltaTime);
        _launchTimer -= 1;

        if (_launchTimer >= 10)
        {
            if ((gameObject.transform.position.x - engagingEnemy.player.transform.position.x) <= 18)
            {
                gameObject.transform.position = new Vector3(engagingEnemy.player.transform.position.x + 18, gameObject.transform.position.y, gameObject.transform.position.z);
            }
        }
        else if (_launchTimer < 10 && _charged == false)
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(150f, 0));

        if (_launchTimer == 6)
            _charged = true;

        if (_launchTimer < 5 && _charged == true)
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15f, 0));
    }

    public override void OnStateEnter()
    {
        Debug.Log("RedState entered!");
        engagingEnemy = gameObject.GetComponent<EnemyEngagingFSM>();
    }
}
