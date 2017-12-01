using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderScript : MonoBehaviour {

    public GameObject objectToSpawn;
    public bool overlapping;
    public string collidingObject;

    Color col;
    Color originalCol;

	// Use this for initialization
	void Start () {
        col = GetComponent<SpriteRenderer>().color;
        originalCol = col;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        
        if (overlapping)
        {
            col = Color.red;
        }
        else
        {
            col = originalCol;
        }
    }

    void OnMouseUp()
    {
        col.a = 255;
        GetComponent<SpriteRenderer>().color = col;

        if (overlapping && collidingObject == "TrashcanTrigger")
        {
            Destroy(gameObject);
        }
    }

    //Use this is the object has no additional scripts.
    void SpawnObstacle()
    {
        GameObject newObstacle = Instantiate(objectToSpawn, transform.position, transform.rotation);
    }

    //Use this if the object has the EnemyBasic script.
    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(objectToSpawn, transform.position, transform.rotation);
        newEnemy.GetComponent<EnemyBasic>().enabled = false;
    }

    //Use this if the object has the EnemyLaser script.
    void SpawnLaser()
    {
        GameObject newLaser = Instantiate(objectToSpawn, transform.position + new Vector3(0, 9.25f, 0), transform.rotation);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        overlapping = true;
        collidingObject = collision.gameObject.name;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        overlapping = false;
        collidingObject = "";
    }
}
