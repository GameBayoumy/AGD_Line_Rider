using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){

            transform.localScale += new Vector3(0.02f, 0.02f, 0);
           // transform.localScale -= new Vector3(0.05f, 0.1f, 0);
        }
    }
}
