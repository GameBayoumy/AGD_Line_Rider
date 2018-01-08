using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineSkinManager : MonoBehaviour {

	public static LineSkinManager Instance { 
		set;
		get;
	}

	public Material activeLine;
	public Material[] lineSkins = new Material[3]; 


	// Use this for initialization
	void Start () {

//		PlayerPrefs.SetInt("ShaderID", PlayerPrefs.GetInt("ShaderID"));

		PlayerPrefs.SetInt("UnlockedNeon", 1);
		PlayerPrefs.SetInt("UnlockedRope", PlayerPrefs.GetInt("UnlockedRope"));
		PlayerPrefs.SetInt("UnlockedBubbles", PlayerPrefs.GetInt("UnlockedBubbles"));

	}


	// Update is called once per frame
	void Update () {

	}
}
