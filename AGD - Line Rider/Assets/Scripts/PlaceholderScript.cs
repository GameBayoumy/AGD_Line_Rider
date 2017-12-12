using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderScript : MonoBehaviour {

    public GameObject objectToSpawn;
    public bool overlapping;
    public string collidingObject;

    Color col;
    Color originalCol;

    TestLevel _testManager;
    bool spawnedMyObject;
    GameObject _objectCopy;
    Vector2 _positionInLevel;
    GameObject _finalSet;

	// Use this for initialization
	void Start () {
        col = GetComponent<SpriteRenderer>().color;
        originalCol = col;
        _testManager = GameObject.Find("Play").GetComponent<TestLevel>();
        _finalSet = GameObject.Find("CustomSet");
	}
	
	// Update is called once per frame
	void Update () {

        if (!spawnedMyObject && _testManager.testInProgress)
        {
            spawnedMyObject = true;
            _positionInLevel = transform.position;
            _objectCopy = Instantiate(objectToSpawn, transform.position, transform.rotation);
            _objectCopy.transform.parent = _finalSet.transform;
            GetComponent<SpriteRenderer>().enabled = false;
            transform.position = new Vector2(-200, -200);
        }

        if (spawnedMyObject && !_testManager.testInProgress)
        {
            spawnedMyObject = false;
            Destroy(_objectCopy);
            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = _positionInLevel;
        }
		
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
