using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShader : MonoBehaviour {

    public int shaderID;
    Button myButton;
    UnlockShaders shaderManager;

	// Use this for initialization
	void Start () {

        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(ChangeShader);
        shaderManager = GameObject.Find("ShaderManager").GetComponent<UnlockShaders>();
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeShader()
    {
        PlayerPrefs.SetInt("ShaderID", shaderID);
        shaderManager.SelectShader();
    }
}
