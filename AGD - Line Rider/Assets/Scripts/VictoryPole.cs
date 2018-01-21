using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPole : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "pCylinder1")
		{
			VictoryScreenMenu.SetVictoryState(true);
		}
	}
}
