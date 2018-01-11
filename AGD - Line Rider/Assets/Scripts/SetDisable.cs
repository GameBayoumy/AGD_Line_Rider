using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisable : MonoBehaviour {

    private GameObject deletePoint;

    private void Awake()
    {
        deletePoint = GameObject.Find("deletePoint");
    }

    private void Update()
    {
        DisableGameObject();

    }

    private void DisableGameObject()
    {
        if (transform.position.x < deletePoint.transform.position.x)
        {
            gameObject.SetActive(false);
        }
    }
}
