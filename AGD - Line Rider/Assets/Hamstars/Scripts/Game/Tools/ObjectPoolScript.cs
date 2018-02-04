using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolScript : MonoBehaviour
{

	public GameObject pooledObject;
	public int pooledAmount;

	List<GameObject> pooledObjects;

	//Creates list of pooled objects and fills the list until it meets the pooledAmount
	void Start()
	{
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
			obj.transform.SetParent(gameObject.transform);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	//If object is inactive, retrieve object. If there isn't an inactive game object, a new one will be instantiated.
	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		GameObject obj = (GameObject)Instantiate(pooledObject);
		obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj;
	}
}