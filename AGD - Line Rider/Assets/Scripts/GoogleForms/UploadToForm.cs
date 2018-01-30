using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadToForm : MonoBehaviour {

    public GameObject username;

    private string myName;
    float number = 6;
    float secondNumber = 11;

    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/d/e/1FAIpQLSdF9tmmJAgZqfH_mcwbFaxQaaVdGoCThT36seg1iMl5HrdUsQ/formResponse";

    IEnumerator Post(string finalName, string score, string seed)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.962731268", finalName);
        form.AddField("entry.884131247", score);
        form.AddField("entry.1310073433", seed);
        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL, rawData);
        yield return www;
    }

    public void Send()
    {
        myName = username.GetComponent<InputField>().text;
        StartCoroutine(Post(myName, number.ToString(), secondNumber.ToString()));
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
