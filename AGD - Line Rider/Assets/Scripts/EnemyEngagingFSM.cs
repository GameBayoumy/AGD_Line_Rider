using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEngagingFSM : EnemyBasic {


    private Vector3 PlayerPosition;
    private Vector3 EnemyEngagingPosition;

    public GameObject Player;


    protected override void Awake () {
        Player = GameObject.Find("Player");
        base.Awake();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerPosition = Player.transform.position;

        this.transform.position = new Vector3(Player.transform.position.x + 18, this.transform.position.y, this.transform.position.z);
	}
}
