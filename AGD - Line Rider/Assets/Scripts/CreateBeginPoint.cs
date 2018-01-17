using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBeginPoint : MonoBehaviour {

    public float beginPos = 100;
    public float endPos;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.transform.localPosition.x < beginPos)
            {
                beginPos = child.transform.localPosition.x;
            }
        }

        endPos = transform.GetChild(0).transform.localPosition.x;
    }

	// Use this for initialization
	void Start () {
        AdjustAllPositions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AdjustAllPositions()
    {
        float finalPos = (endPos + beginPos) / 2;
        foreach (Transform child in transform)
        {
            child.transform.localPosition += new Vector3(finalPos, 0, 0);
        }
    }
}
