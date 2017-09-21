using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public List<GameObject> backgroundSprites;
    public Transform resetPosition;

    public float offset = 30;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgroundSprites.Count; i++)
        {
            if (backgroundSprites[i].transform.position.z < resetPosition.position.z)
            {
                backgroundSprites[i].transform.position = new Vector3(backgroundSprites[0].transform.position.x, backgroundSprites[0].transform.position.y, backgroundSprites[i].transform.localPosition.z + offset);
            }
        }
    }
}