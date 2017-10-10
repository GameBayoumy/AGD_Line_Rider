using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour {

    public int CurrencyPoints;




    //USAGE: use this code in any class to change the currency.
    //GameObject CurrencyHandler = GameObject.Find("CurrencyHandler");
    //Currency currency = CurrencyHandler.GetComponent<Currency>();
    //PlayerPrefs.SetInt("Player Currency", currency.CurrencyPoints + 1);


	// Use this for initialization
	void Start () {
        CurrencyPoints = PlayerPrefs.GetInt("Player Currency");
        PlayerPrefs.SetInt("Player Currency", CurrencyPoints);
	}
	
	// Update is called once per frame
	void Update () {
        print(CurrencyPoints);
	}
}
