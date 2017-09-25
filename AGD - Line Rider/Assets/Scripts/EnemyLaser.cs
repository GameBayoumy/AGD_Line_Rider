using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLaser : MonoBehaviour {

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

		foreach (ContactPoint2D contact in collision.contacts)
		{
			Debug.DrawRay(contact.point, contact.normal, Color.white);
            Debug.Log(contact.point);
		}
	}
}
