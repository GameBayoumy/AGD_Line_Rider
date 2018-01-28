using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeed : MonoBehaviour {

    public bool useCustomSeed;
    public int originalSeed;
    public int customSeed;
    public int activeSeed;

    // Use this for initialization
    void Awake () {

        if (useCustomSeed)
        {
            originalSeed = customSeed;
            Random.InitState(customSeed);
            activeSeed = customSeed;
        }
        else
        {
            originalSeed = Random.Range(0, 5000000);
            activeSeed = originalSeed;
            Random.InitState(activeSeed);
        }
    }
	
	// Update is called once per frame
	void Update () {

        Random.InitState(activeSeed);

    }
}
