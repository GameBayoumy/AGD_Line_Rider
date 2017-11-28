using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectEditor : MonoBehaviour {

    public GameObject relatedPrefab;
    public Sprite relatedSprite;
    public Color relatedColor;
    public Vector2 relatedSize;
    public GameObject placeholder;

	// Use this for initialization
	void Start () {

        placeholder = GameObject.Find("Placeholder");
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.transform.name);
        }

    }

    public void SpawnObject()
    {
        placeholder.transform.position = Vector2.zero;
        placeholder.transform.localScale = relatedSize;
        placeholder.GetComponent<SpriteRenderer>().sprite = relatedSprite;
        placeholder.GetComponent<SpriteRenderer>().color = relatedColor;
        placeholder.GetComponent<PlaceholderScript>().objectToSpawn = relatedPrefab;
    }
}
