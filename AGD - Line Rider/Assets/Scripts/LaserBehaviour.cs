using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {
    
    public Vector3 extendLaser;
    public EnemyLaser EnemyLaser;
	
	// Update is called once per frame
	void Update ()
    {
        if (EnemyLaser.laserHit == true)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 0.04f,transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 0.04f, transform.localScale.z);
        }

        if(transform.localScale.y <= 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, .1f, transform.localScale.z);
        }
	}
}
