using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashcanTrigger : MonoBehaviour {

    public GameObject trashcanImage;

	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.ScreenToWorldPoint(trashcanImage.transform.position + new Vector3(0,0,10));
    }
}