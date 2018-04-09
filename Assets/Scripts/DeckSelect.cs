using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelect : MonoBehaviour {

  //public GameObject userDeckPrefab = userDeckPrefab;

  private int currentPage = 0;
	private string[] pageToKey = new string[10];

	// Update is called once per frame
  void Start() {
	  for(int i = 0; i < 10; ++i)
		{
			pageToKey[i] = "string" + i;
		}
	}

	void Update () {

	}

	void FlipRight() {
		//change to the appropriate
	}

	void FlipLeft() {

	}

	void AddPage() {

	}

	bool ContainsPage() {

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
