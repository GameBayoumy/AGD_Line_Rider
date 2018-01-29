using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestList : MonoBehaviour {

    public string myString;

    public string[] allFields;
    public string[] allNames;
    public string[] allScores;
    public string[] allSeeds;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.B))
        {
            allFields = myString.Split(new string[] { "{\"Tijdstempel\":\"", "\",\"Name\":\"", "\",\"Score\":\"", "\",\"Seed\":\"", "[", "\"}]", "\"}," }, StringSplitOptions.None);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            int j = 0;
            int k = 0;
            int l = 0;

            int theCount = myString.Split(new string[] { "Name" }, StringSplitOptions.None).Length - 1;
            allNames = new string[theCount];
            allScores = new string[theCount];
            allSeeds = new string[theCount];

            for (int i = 3; i < allFields.Length; i += 5)
            {
                allNames[j] = allFields[i];
                j++;
            }

            for (int i = 4; i < allFields.Length; i += 5)
            {
                allScores[k] = allFields[i];
                k++;
            }

            for (int i = 5; i < allFields.Length; i += 5)
            {
                allSeeds[l] = allFields[i];
                l++;
            }
        }
		
	}
}
