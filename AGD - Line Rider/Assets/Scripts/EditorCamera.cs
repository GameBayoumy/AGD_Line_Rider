using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCamera : MonoBehaviour {

    bool movingLeft;
    bool movingRight;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (movingLeft && Camera.main.transform.position.x > 0)
            Camera.main.transform.position -= new Vector3(1, 0, 0);

        if (movingRight)
            Camera.main.transform.position += new Vector3(1, 0, 0);
    }

    public void ActivateLeft()
    {
        movingLeft = true;
    }
    public void DisableLeft()
    {
        movingLeft = false;
    }

    public void ActivateRight()
    {
        movingRight = true;
    }
    public void DisableRight()
    {
        movingRight = false;
    }
}
