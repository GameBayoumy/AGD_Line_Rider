using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLaser : MonoBehaviour {

    public bool laserHit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("LineMeshTest");
        }
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Trail")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                laserHit = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Trail")
        {
            laserHit = false;
        }
    }
}
