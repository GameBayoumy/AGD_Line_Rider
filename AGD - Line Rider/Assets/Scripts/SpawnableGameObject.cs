using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableGameObject : MonoBehaviour {

    //Removes objects when they are outside the screen

    protected GameObject deletePoint;
    protected virtual void Awake () {
        deletePoint = GameObject.Find("deletePoint");
	}

    protected virtual void Update () {
        Debug.Log("BLEEEEEEEEEEEEEEEEEEEEEEEEE");
        DisableGameObject();
		
	}
    protected virtual void DisableGameObject()
    {
        if (transform.position.x > deletePoint.transform.position.x)
        {
            Debug.Log("BLUUUH");
        }
            if (transform.position.x < deletePoint.transform.position.x)
        {
            gameObject.SetActive(false);
            Debug.Log("Activated!");
        }
    }
}
