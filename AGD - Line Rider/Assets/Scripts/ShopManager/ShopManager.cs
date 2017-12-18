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
	public Text lineSkinBuyEquip;

	public Transform lineSkinPanel;

	//Lists the prices of the alternate lines or line skins
	private int[] alternateLineCost = new int[] { 0, 10, 20};

	private int selectedLineSkinIndex;

	// Use this for initialization
	void Start () {

		//This value within PlayerPrefs determines whether the product in question
		//Has been bought (and therefore unlocked) or not
		//0 means that it is locked, 1 means that it is unlocked

//		PlayerPrefs.SetInt("Line_1_Neon", 0);
//		PlayerPrefs.SetInt("Line_2_Rope", 0);
//		PlayerPrefs.SetInt("Line_3_Bubbles", 0);
//
//		PlayerPrefs.SetInt("Line_1_Neon", 1);
//		PlayerPrefs.SetInt("Line_2_Rope", 1);
//		PlayerPrefs.SetInt("Line_3_Bubbles", 1);
//
//
//
//
//
//		PlayerPrefs.SetInt ("Product_1_Key", 0);
//		PlayerPrefs.GetInt ("Product_1_Key");
//
//		PlayerPrefs.SetInt ("Product_2_Key", 0);
//		PlayerPrefs.GetInt ("Product_2_Key");
	
		// Add button onclick events to shop buttons
		InitializeShop ();

	}

	// Update is called once per frame
	void Update () {
		//Displays the current currency within the shop scene
		currentCurrencyText.text = (GetCurrency().ToString()) + " Coins";
	}


	private void InitializeShop(){
	
		//Just to make sure the references have been assigned
		if (lineSkinPanel == null) { 
			Debug.Log ("You did not assign the lineSkin panel in the inspector");
		}

		//For every children transform under the lineSkin panel, find the button and add onclick
		int i = 0;
		foreach (Transform t in lineSkinPanel) {
			int currentIndex = i;
			Button b = t.GetComponent<Button> ();
			//This onclick event leads to OnLineSkinSelect
			//In which the currently clicked button will be highlightedm
			b.onClick.AddListener (() => OnLineSkinSelect (currentIndex));
			i++;
		}
		//Reset the currentIndex
		i = 0;
	}


	private void SetLineSkin(int Index){
	
		//Change the appearance of the line


		//Change buy/equip button text
		lineSkinBuyEquip.text = "Current";
	}





	private void OnLineSkinSelect(int currentIndex){ 
		Debug.Log("Selecting lineSkin button : " + currentIndex);

		//Declare the selected lineSkin
		//The selectedLineSkinIndex value is equal to the currentIndex value of the button in question
		selectedLineSkinIndex = currentIndex;

		//Change the content of the buy/equip button, depending on the state of the lineSkin
		if (SaveManager.Instance.IsColorOwned (currentIndex)) {
			//LineSkin is bought, therefore unlocked
			lineSkinBuyEquip.text = "Equip"; 
		} else {
			//LineSkin has not been bought yet, therefore yet to be unlocked
			lineSkinBuyEquip.text = "Buy: " + alternateLineCost[currentIndex].ToString();


		}


	}


	public void OnLineSkinBuyEquip() { 
		Debug.Log("Buy/Equip lineSkin");

		//Is the selected lineSkin bought, and therefore unlocked
		if (SaveManager.Instance.IsColorOwned (selectedLineSkinIndex)) {
			//Equip the selected lineSkin
			SetLineSkin (selectedLineSkinIndex);
		} else {
			//Attempt to buy the selected lineSkin
			Purchase();				
		}
	}




	public void Purchase(){
		//Checks if currency is equal to or more than the price
		//If it is, then the currentCurrency will have its value subtracted by the price
		if (GetCurrency() >= alternateLineCost[selectedLineSkinIndex]) {
			SetCurrency((GetCurrency() - alternateLineCost[selectedLineSkinIndex]));
			PlayerPrefs.SetInt ("Unlocked LineSkin", selectedLineSkinIndex, 1);
		}
	}


	//Gets the current currency from the game
	public int GetCurrency(){
		return PlayerPrefs.GetInt ("Player Currency");
	}

	//Updates the current currency from the game
	public void SetCurrency(int value){
		PlayerPrefs.SetInt ("Player Currency", value);
	}






}
