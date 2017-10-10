using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableGameObject : MonoBehaviour {

    protected GameObject deletePoint;
	// Use this for initialization
	void Awake () {
        deletePoint = GameObject.Find("deletePoint");
	}
	
	// Update is called once per frame
	protected void Update () {
        DisableGameObject();
		
	}
    protected void DisableGameObject()
    {
        if (transform.position.x < deletePoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
