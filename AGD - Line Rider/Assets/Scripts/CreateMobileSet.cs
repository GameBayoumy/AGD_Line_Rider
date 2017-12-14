using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMobileSet : MonoBehaviour {

    public int setID;

	// Use this for initialization
	void Awake () {
        int amountOfObjects = PlayerPrefs.GetInt("Set" + setID + "Amount");
        transform.GetChild(0).transform.localPosition = new Vector3(PlayerPrefs.GetFloat("Set" + setID + "Object1X"), PlayerPrefs.GetFloat("Set" + setID + "Object1Y"), 0);

        if (amountOfObjects != 0)
        {
            for (int i = 2; i < amountOfObjects + 1; i++)
            {
                GameObject newChild;
                newChild = Instantiate(Resources.Load("Sets/editorObjects/" + PlayerPrefs.GetString("Set" + setID + "Object" + i)), transform.position, Quaternion.identity) as GameObject;
                newChild.transform.parent = gameObject.transform;
                newChild.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("Set" + setID + "Object" + i + "X"), PlayerPrefs.GetFloat("Set" + setID + "Object" + i + "Y"), 0);
                newChild.transform.localScale = new Vector3(newChild.transform.localScale.x, PlayerPrefs.GetFloat("Set" + setID + "Object" + i + "Size"), 1);
                newChild.transform.localEulerAngles = new Vector3(newChild.transform.localEulerAngles.x, newChild.transform.localEulerAngles.y, PlayerPrefs.GetFloat("Set" + setID + "Object" + i + "Rotation"));
            }
        }
		
	}
}
