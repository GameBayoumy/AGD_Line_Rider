using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineSkinManager : MonoBehaviour {

	public int lineID;
	public string lineName;
	Button _thisButton;

	// Use this for initialization
	void Start () {

		_thisButton = GetComponent<Button>();
		_thisButton.onClick.AddListener(ChangeLine);

	}


	// Update is called once per frame
	void Update () {

	}

	void ChangeLine()
	{
		PlayerPrefs.SetInt("LineID", lineID);
//		_shaderManager.SelectShader();
	}

	void OnEnable()
	{
		if (PlayerPrefs.GetInt(lineName) == 1)
		{
			gameObject.SetActive(true);
		}
		else {
			gameObject.SetActive(false);
		}
	}

}
