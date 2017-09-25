using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    public GameObject GameObjectLaser;
    public Vector3 extendLaser;
    public EnemyLaser EnemyLaser;

    private void Awake()
    {
        GameObjectLaser = GameObject.FindGameObjectWithTag("Laser");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObjectLaser.transform.localScale.y <= 0.2f){
            GameObjectLaser.transform.localScale = new Vector3(1, 1f, 1);
        }


        if (EnemyLaser.laserHit == true){
            Debug.Log(EnemyLaser.laserHit);
            GameObjectLaser.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.02f,transform.localScale.z);
        }

        if (EnemyLaser.laserHit == false){
			GameObjectLaser.transform.localScale = new Vector3(1, 1, 1);
        }


		
	}
}
