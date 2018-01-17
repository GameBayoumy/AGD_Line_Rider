using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour {

    public int CurrencyPoints;
    //USAGE: use this code in any class to change the currency.
    //GameObject CurrencyHandler = GameObject.Find("CurrencyHandler");
    //Currency currency = CurrencyHandler.GetComponent<Currency>();
    //PlayerPrefs.SetInt("Player Currency", currency.CurrencyPoints + 1);

	public Text currentCurrencyText;


    void Start()
    {
        // this array should be filled before you can use EncryptedPlayerPrefs :
        EncryptedPlayerPrefs.keys = new string[5];
        EncryptedPlayerPrefs.keys[0] = "23Wrudre";
        EncryptedPlayerPrefs.keys[1] = "SP9DupHa";
        EncryptedPlayerPrefs.keys[2] = "frA5rAS3";
        EncryptedPlayerPrefs.keys[3] = "tHat2epr";
        EncryptedPlayerPrefs.keys[4] = "jaw3eDAs";

        CurrencyPoints = EncryptedPlayerPrefs.GetInt("Player Currency");
        EncryptedPlayerPrefs.SetInt("Player Currency", CurrencyPoints);
       // Debug.Log(EncryptedPlayerPrefs.GetInt("Player Currency", -1));
    }

	void Update()
	{
		//Displays the current currency within the shop scene
		currentCurrencyText.text = (EncryptedPlayerPrefs.GetInt ("Player Currency").ToString());
	}
}