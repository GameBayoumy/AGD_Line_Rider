using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public List<GameObject> backgroundSprites;
    public Transform resetPosition;

    public float offset = 40;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgroundSprites.Count; i++)
        {
            if (backgroundSprites[i].transform.position.x < resetPosition.position.x)
            {
                backgroundSprites[i].transform.position = new Vector3(backgroundSprites[i].transform.localPosition.x + offset, backgroundSprites[0].transform.position.y, backgroundSprites[0].transform.position.z);
            }
        }
    }
}