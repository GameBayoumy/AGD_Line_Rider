using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBasic : MonoBehaviour {
   
   
   

	// Use this for initialization
	void Start () {
	//	GetComponent<ObjectPoolScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//SpawnObject(EnemyBasicPool);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{   


		if (collision.gameObject.tag == "Player")
		{
			SceneManager.LoadScene("LineMeshTest");
		}
	}
}
