using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBeginPoint : MonoBehaviour
{

    public GameObject beginPoint;
    float beginPos;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.transform.localPosition.x < beginPos)
            {
                beginPos = child.transform.localPosition.x;
            }
        }

        GameObject thePoint = Instantiate(beginPoint, transform.position, transform.rotation) as GameObject;
        thePoint.transform.parent = transform;
        thePoint.transform.localPosition = new Vector3(beginPos, 0, 0);
    }
}
