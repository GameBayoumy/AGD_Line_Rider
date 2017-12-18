using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour {

	//This script manages the shop of the game
	//It keeps track of the currentCurrency both as a display text and as a value
	//It keeps track which products have been bought or are yet to be bought
	//It keeps track which product is currently activated and therefore ensures others being deactivated

	public Text currentCurrencyText;

	//Lists the prices of the alternate players or player skins
	private int[] alternatePlayerCost = new int[] { 10, 20, 30};

	// Use this for initialization
	void Start () {


		//This value within PlayerPrefs determines whether the product in question
		//Has been bought (and therefore unlocked) or not
		//0 means that it is locked, 1 means that it is unlocked

		PlayerPrefs.SetInt("Line_1_Neon", 0);
		PlayerPrefs.SetInt("Line_2_Rope", 0);
		PlayerPrefs.SetInt("Line_3_Bubbles", 0);

		PlayerPrefs.SetInt("Line_1_Neon", 1);
		PlayerPrefs.SetInt("Line_2_Rope", 1);
		PlayerPrefs.SetInt("Line_3_Bubbles", 1);





		PlayerPrefs.SetInt ("Product_1_Key", 0);
		PlayerPrefs.GetInt ("Product_1_Key");

		PlayerPrefs.SetInt ("Product_2_Key", 0);
		PlayerPrefs.GetInt ("Product_2_Key");
	
	}

	// Update is called once per frame
	void Update () {
		//Displays the current currency within the shop scene
		currentCurrencyText.text = (getCurrency().ToString()) + " Coins";
	}


	void Purchase(){




		//Checks if currency is equal to or more than the price
		//If it is, then the currentCurrency will have its value subtracted by the price
		if (getCurrency() >= 200) {
			setCurrency((getCurrency() - 200));
		}
	}


	//Gets the current currency from the game
	public int getCurrency(){
		return PlayerPrefs.GetInt ("Player Currency");
	}

	//Updates the current currency from the game
	public void setCurrency(int value){
		PlayerPrefs.SetInt ("Player Currency", value);
	}






}
