using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySinus : EnemyBasic {

    private int SinusMovement = -20;

   
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, SinusMovement));
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SinusMovement *= -1;

        if (collision.gameObject.tag == "Player")
        {
            GameOverMenu.SetGameOverState(true);
        }

    }

}
