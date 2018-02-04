using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {

    public Text uiText;
    public Slider uiSlider;

    private Player player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        uiSlider.maxValue = player.maxResource;        // Set max value in ui slider
        uiSlider.value = player.drawResource;          // Set current ui value to draw resource
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Update UI elements
        uiSlider.value = player.drawResource;
        uiText.text = "Draw " + player.drawResource;
    }
}
