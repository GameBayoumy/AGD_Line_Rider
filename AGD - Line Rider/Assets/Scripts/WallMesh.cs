using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WallMesh : MonoBehaviour {

    public GameObject Player;
    public GameObject Wall;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        // movement = Player.transform.position.x;

        Wall.transform.position = new Vector3(Player.transform.position.x, Wall.transform.position.y, Wall.transform.position.z);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            SceneManager.LoadScene("LineMeshTest");
        }
    }
}
