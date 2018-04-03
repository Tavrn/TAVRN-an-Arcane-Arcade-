using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelect : MonoBehaviour {

  //public GameObject userDeckPrefab = userDeckPrefab;
	public GameObject userDeck;

	// Use this for initialization
	void Awake () {
		//try to find userDeck
		  //if it exists set userDeck =  to what you found
			//else create a userDeck using Instantiate and DontDestroyOnLoad

	  /*
		userDeck = Find("UserDeck");
		if(userDeck == null)
		{
			userDeck = Instantiate(userDeckPrefab, gameObject);
			DontDestroyOnLoad(userDeck);
		}
		*/
	}

	// Update is called once per frame
	void Update () {

	}

	//functions to make
	/*
	void FlipRight();
	void FlipLeft();
	void AddPage();
	void RemovePage();
	bool OpenPages();
  */
}
