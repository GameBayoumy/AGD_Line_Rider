using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectEditor : MonoBehaviour {

    public GameObject relatedPrefab;
    public Sprite relatedSprite;
    public Color relatedColor;
    public Vector2 relatedSize;

    public bool isCircle;
    public Vector2 boxColliderSize;
    public float circleRadius;

    public GameObject placeholder;

    public string myName;
    public string description;
    public bool canBeScaled;
    public bool canBeRotated;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void SpawnObject()
    {
        GameObject ph = Instantiate(placeholder, transform.position, Quaternion.Euler(Vector3.zero));
        PlaceholderScript phScript = ph.GetComponent<PlaceholderScript>();
        ph.transform.position = Vector2.zero;
        ph.transform.localScale = relatedSize;

        if (!isCircle)
        {
            ph.GetComponent<CircleCollider2D>().enabled = false;
            ph.GetComponent<BoxCollider2D>().enabled = true;
            ph.GetComponent<BoxCollider2D>().size = boxColliderSize;
        }
        else
        {
            ph.GetComponent<BoxCollider2D>().enabled = false;
            ph.GetComponent<CircleCollider2D>().enabled = true;
            ph.GetComponent<CircleCollider2D>().radius = circleRadius;
        }
        ph.GetComponent<SpriteRenderer>().sprite = relatedSprite;
        ph.GetComponent<SpriteRenderer>().color = relatedColor;
        phScript.objectToSpawn = relatedPrefab;

        phScript.myName = myName;
        phScript.description = description;
        phScript.canBeScaled = canBeScaled;
        phScript.canBeRotated = canBeRotated;
    }
}
