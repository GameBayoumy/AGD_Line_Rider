using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenState : State {

    public GameObject enemyBlock;

    private int _sinusMovement = -8;
    private float _spawnTimer;
    private EnemyEngagingFSM enemyEngaging;

    public GreenState(GameObject gameObject) : base(gameObject)
    {
    }
    
    public override void Tick()
    {
        //Rotates the sprite to the Green side
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.rotation.x, gameObject.transform.rotation.x, -120f), enemyEngaging.speedRotation * Time.deltaTime);

        //Lets the enemy move up and down.
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, _sinusMovement));

        //Spawns enemyblock at certain interval
        _spawnTimer -= 1;
        if (_spawnTimer <= 20)
        {
            GameObject newBlock = Instantiate(enemyBlock, new Vector3(this.transform.position.x - 3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            newBlock.tag = "Spawnables";
            _spawnTimer = 250;
        }
    }

    public override void OnStateEnter()
    {
        enemyEngaging = gameObject.GetComponent<EnemyEngagingFSM>();
    }
}
