using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBasic : SpawnableGameObject {

    protected override void Awake() { base.Awake(); }
    protected override void Update() { base.Update(); }
    protected override void DisableGameObject() { base.DisableGameObject(); }

    private void OnCollisionEnter2D(Collision2D collision)
	{   
		if (collision.gameObject.tag == "Player")
		{
			SceneManager.LoadScene("LineMeshTest");
		}
	}
}
