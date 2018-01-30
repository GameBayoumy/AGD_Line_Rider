using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GSFU_Demo_Runtime : MonoBehaviour
{
    public string myString;

    public string[] allFields;
    public string[] allNames;
    public string[] allScores;
    public string[] allSeeds;

    public GameObject button;
    public Vector2[] allPositions;

    bool madeArray;
    bool filledLists;
    bool placedButtons;

    void OnEnable()
	{
		// Suscribe for catching cloud responses.
		CloudConnectorCore.processedResponseCallback.AddListener(GSFU_Demo_Utils.ParseData);
	}
	
	void OnDisable()
	{
		// Remove listeners.
		CloudConnectorCore.processedResponseCallback.RemoveListener(GSFU_Demo_Utils.ParseData);
	}

    void Start()
    {
        GSFU_Demo_Utils.GetAllPlayers(true);
    }

    void Update()
    {
        if (myString != "" && !madeArray)
        {
            allFields = myString.Split(new string[] { "{\"Tijdstempel\":\"", "\",\"Name\":\"", "\",\"Score\":", ",\"Seed\":", "[", "}]", "}," }, StringSplitOptions.None);
            madeArray = true;
        }

        if (madeArray && !filledLists)
        {
            int j = 0;
            int k = 0;
            int l = 0;

            int theCount = myString.Split(new string[] { "Name" }, StringSplitOptions.None).Length - 1;
            allNames = new string[theCount];
            allScores = new string[theCount];
            allSeeds = new string[theCount];

            for (int i = 3; i < allFields.Length; i += 5)
            {
                allNames[j] = allFields[i];
                j++;
            }

            for (int i = 4; i < allFields.Length; i += 5)
            {
                allScores[k] = allFields[i];
                k++;
            }

            for (int i = 5; i < allFields.Length; i += 5)
            {
                allSeeds[l] = allFields[i];
                l++;
            }

            filledLists = true;
        }

        if (filledLists && !placedButtons)
        {
            int index = 0;

            for (int i = 0; i < allNames.Length; i++)
            {
                GameObject newButton = Instantiate(button, transform.position, transform.rotation);
                StartOnlineChallenge online = newButton.GetComponent<StartOnlineChallenge>();
                newButton.name = allNames[i];
                online.id = i;
                online.playername = allNames[i];
                online.score = float.Parse(allScores[i]);
                online.seed = int.Parse(allSeeds[i]);
                newButton.transform.parent = GameObject.Find("Canvas").transform;
                newButton.transform.localPosition = allPositions[index];
                index++;
            }

            placedButtons = true;
        }
    }
	
	//void OnGUI()
	//{
	//	if (GUI.Button(new Rect(20, 20, 140, 25), "Create Table"))
	//		GSFU_Demo_Utils.CreatePlayerTable(true);

	//	if (GUI.Button(new Rect(20, 60, 140, 25), "Create Player"))
	//		GSFU_Demo_Utils.SaveGandalf(true);
		
	//	if (GUI.Button(new Rect(20, 100, 140, 25), "Update Player"))
	//		GSFU_Demo_Utils.UpdateGandalf(true);

	//	if (GUI.Button(new Rect(20, 140, 140, 25), "Retrieve Player"))
	//		GSFU_Demo_Utils.RetrieveGandalf(true);
		
	//	if (GUI.Button(new Rect(20, 180, 140, 25), "Retrieve All Players"))
	//		GSFU_Demo_Utils.GetAllPlayers(true);
		
	//	if (GUI.Button(new Rect(20, 220, 140, 25), "Retrieve All Tables"))
	//		GSFU_Demo_Utils.GetAllTables(true);
	//}
	
	
}



