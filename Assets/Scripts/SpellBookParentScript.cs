using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookParentScript : MonoBehaviour {
	public GameObject hand;
	public GameObject openBook;
	public GameObject closedBook;
	private bool open = false;
	private int deckSize = 10;
	private int spellPage = 0;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = hand.transform.position;
		transform.rotation = hand.transform.rotation;
	}
	public void ToggleOpen(){
		open = !open;
		if(open){
			openBook.SetActive(true);
			closedBook.SetActive(false);
		}else{
			openBook.SetActive(false);
			closedBook.SetActive(true);
		}
	}
	public void FlipRight(){
		spellPage++;
		if(spellPage==deckSize)
			spellPage = 0;
		Debug.Log(spellPage);
	}
	public void FlipLeft(){
		spellPage--;
		if(spellPage==-1)
			spellPage = deckSize-1;
		Debug.Log(spellPage);
	}
}
