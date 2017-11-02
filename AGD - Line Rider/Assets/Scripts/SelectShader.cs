using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShader : MonoBehaviour {

    public int shaderID;
    public string shaderName;
    Button _myButton;
    UnlockShaders _shaderManager;

	// Use this for initialization
	void Start () {

        _myButton = GetComponent<Button>();
        _myButton.onClick.AddListener(ChangeShader);
        _shaderManager = GameObject.Find("ShaderManager").GetComponent<UnlockShaders>();
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeShader()
    {
        PlayerPrefs.SetInt("ShaderID", shaderID);
        _shaderManager.SelectShader();
    }

    void OnEnable()
    {
        if (PlayerPrefs.GetInt(shaderName) == 1)
        {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }

}
