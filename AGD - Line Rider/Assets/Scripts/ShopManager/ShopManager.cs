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
	//This integer keeps track which lineSkin button is currently selected
	private int selectedLineSkinIndex;
	//This integer gets assigned a value of the lineSkin 
	//that corresponds to the selectedLineSkinIndex in question
	private int selectedLineSkinCheck;

	// Use this for initialization
	void Start () {

		SetCurrency (GetCurrency () + 100);

		PlayerPrefs.SetInt("UnlockedRope", 0);
		PlayerPrefs.SetInt("UnlockedBubbles", 0);


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

		//Checks which lineSkin refers to the selectedLineSkinIndex
		//Assigns selectedLineSkinCheck which the value derived from the checked LineSkin
		LineSkinChecker ();

		//Change the content of the buy/equip button, depending on the state of the lineSkin
		//Because selectedLineSkinCheck gets a value assigned from the unlockedLineSkin, it's either 0 or 1
		//If it's 0, the lineSkin has not been unlocked yet
		//If it's 1, the lineSkin has already been unlocked
		if (selectedLineSkinCheck == 1) {
			//LineSkin is bought, therefore unlocked
			lineSkinBuyEquip.text = "Equip"; 
		} else {
			//LineSkin has not been bought yet, therefore yet to be unlocked
			lineSkinBuyEquip.text = "Buy: " + alternateLineCost[currentIndex].ToString();
		}
	}


	public void OnLineSkinBuyEquip() { 
		Debug.Log("Buy/Equip lineSkin");

		LineSkinChecker ();

		//Is the selected lineSkin bought, and therefore unlocked
		if (selectedLineSkinCheck == 1) {
			//Equip the selected lineSkin
			SetLineSkin (selectedLineSkinIndex);
		} else {
			//Attempt to buy the selected lineSkin
			Purchase();				
		}
	}
		


	public void Purchase(){
		//Checks if currency is equal to or more than the price
		if (GetCurrency() >= alternateLineCost[selectedLineSkinIndex]) {
			//If it is, then the currentCurrency will have its value subtracted by the price
			SetCurrency((GetCurrency() - alternateLineCost[selectedLineSkinIndex]));
			LineSkinUnlocker ();
		}
	}
		
	public void LineSkinUnlocker(){
		//Checks to which lineSkin in question the selectedLineSkinIndex value relates to
		//If the value is 0 then it refers to the Neon lineSkin, therefore that one will be unlocked
		if (selectedLineSkinIndex == 0) {
			PlayerPrefs.SetInt ("UnlockedNeon", 1);
			Debug.Log ("You unlocked the Neon lineSkin!");
		} else if (selectedLineSkinIndex == 1) {
			PlayerPrefs.SetInt("UnlockedRope", 1);
			Debug.Log ("You unlocked the Rope lineSkin!");
		} else if (selectedLineSkinIndex == 2) {
			PlayerPrefs.SetInt ("UnlockedBubbles", 1);
			Debug.Log ("You unlocked the Bubbles lineSkin!");
		}
	}

	public void LineSkinChecker(){
		//Checks to which lineSkin in question the selectedLineSkinIndex value relates to
		//If the value is 0 then it refers to the Neon lineSkin, 
		//therefore the value from UnlockedNeon will be assigned to selectedLineSkinCheck
		if (selectedLineSkinIndex == 0) {
			selectedLineSkinCheck = PlayerPrefs.GetInt ("UnlockedNeon");
			Debug.Log ("Neon lineSkin checked!");
		} else if (selectedLineSkinIndex == 1) {
			selectedLineSkinCheck = PlayerPrefs.GetInt("UnlockedRope");
			Debug.Log ("Rope lineSkin checked!");
		} else if (selectedLineSkinIndex == 2) {
			selectedLineSkinCheck = PlayerPrefs.GetInt ("UnlockedBubbles");
			Debug.Log ("Bubbles lineSkin checked!");
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
