using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {

    public float targetTime = 0.1f;
    private bool hit = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(hit){
            targetTime -= Time.deltaTime;
        }

        if (targetTime <= 0.0f && hit == true)
        {
            transform.localScale = new Vector3(transform.localScale.x / 1.20f, transform.localScale.y / 1.20f, transform.localScale.z * 1.0f);
            hit = false;
            targetTime = 0.1f;
        }
	}

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.20f, transform.localScale.y * 1.20f, transform.localScale.z * 1.0f);
            hit = true;
        }
    }
}
