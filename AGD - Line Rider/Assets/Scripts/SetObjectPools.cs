using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetObjectPools : MonoBehaviour {

    public List<GameObject> easySets;
    public List<GameObject> normalSets;
    public List<GameObject> hardSets;
    public List<GameObject> customSets;
    public List<GameObject> chosenSets;

    public int easyAmount;
    public int normalAmount;
    public int hardAmount;
    public int customAmount;

    // Use this for initialization
    void Start () {

        easySets = new List<GameObject>();
        normalSets = new List<GameObject>();
        hardSets = new List<GameObject>();
        customSets = new List<GameObject>();

        chosenSets = easySets;

        for (int i = 1; i < easyAmount+1; i++)
        {
            GameObject easyObj = Instantiate(Resources.Load("Sets/easy/easy_set" + i, typeof(GameObject))) as GameObject;
            easyObj.SetActive(false);
            easySets.Add(easyObj);
        }

        for (int i = 1; i < normalAmount + 1; i++)
        {
            GameObject normalObj = Instantiate(Resources.Load("Sets/normal/normal_set" + i, typeof(GameObject))) as GameObject;
            normalObj.SetActive(false);
            normalSets.Add(normalObj);
        }

        for (int i = 1; i < hardAmount + 1; i++)
        {
            GameObject hardObj = Instantiate(Resources.Load("Sets/hard/hard_set" + i, typeof(GameObject))) as GameObject;
            hardObj.SetActive(false);
            hardSets.Add(hardObj);
        }

        if (Application.isEditor)
        {
            for (int i = 1; i < customAmount + 1; i++)
            {
                GameObject customObj = Instantiate(Resources.Load("Sets/custom/custom_set" + i, typeof(GameObject))) as GameObject;
                customObj.SetActive(false);
                customSets.Add(customObj);
            }
        }

        else
        {
            for (int i = 1; i < customAmount + 1; i++)
            {
                GameObject customObj = Instantiate(Resources.Load("Sets/customMobile/mobile_set" + i, typeof(GameObject))) as GameObject;
                customObj.SetActive(false);
                customSets.Add(customObj);
            }
        }

    }

    public GameObject GetPooledSet()
    {
        int i = Random.Range(0, chosenSets.Count);

        if (!chosenSets[i].activeInHierarchy)
        {
            return chosenSets[i];
        }

        return null;
    }

}