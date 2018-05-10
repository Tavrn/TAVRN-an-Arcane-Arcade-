using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelect : MonoBehaviour {

  //public GameObject userDeckPrefab = userDeckPrefab;

  private int DECKSIZE = 15;

  public static int currProposal = 0;
  private int prevProposal = 0;
  private int currPage = 0;
  private int spellNum = -1;

  public TextMesh pgTxt;
  public TextMesh spTxt;
  public TextMesh prTxt;

	// Update is called once per frame
  void Start() {
    //resets deck each time you enter the tavern for testing
    if(PlayerPrefs.HasKey("DeckInitialized") == false)
    {
      for(int i = 0; i < DECKSIZE; ++i)
      {
        PlayerPrefs.SetInt("Spell_" + i, i + 1);
      }
      PlayerPrefs.SetInt("DeckInitialized", 1);
    }
    pgTxt.text = " Page \n number: \n" + currPage;
    spTxt.text = " Spell \n number: \n" +  PlayerPrefs.GetInt("Spell_" + currPage, -1);
    prTxt.text = " Proposed spell \nnumber: " + 0;
	}

	void Update () {
    if(prevProposal != currProposal)
    {
      ProposalChanged();
    }
	}

  void ProposalChanged() {
    prTxt.text = " Proposed spell \nnumber: " + currProposal;
    prevProposal = currProposal;
  }

  //turns the currentPage counter to the one corresponding to the page to the right
	public void FlipRight() {
    currPage = currPage + 1;
    if (currPage > DECKSIZE - 1)
    {
      currPage = 0;
    }
    pgTxt.text = " Page \n number: \n" + currPage;
    spTxt.text = " Spell \n number: \n" +  PlayerPrefs.GetInt("Spell_" + currPage, -1);
	}

  //turns the currentPage counter to the corresponding to the page to the left
	public void FlipLeft() {
    currPage = currPage - 1;
    if (currPage < 0)
    {
      currPage = DECKSIZE - 1;
    }
    pgTxt.text = " Page \n number: \n" + currPage;
    spTxt.text = " Spell \n number: \n" +  PlayerPrefs.GetInt("Spell_" + currPage, -1);
	}

  //adds currProposal to currDeck if not already present
	public void AddPage() {
//Debug.Log("function");
    if(!DeckContains(currProposal))
    {
//Debug.Log(currProposal);
      PlayerPrefs.SetInt("Spell_" + currPage, currProposal);
      spTxt.text = " Spell \n number: \n" + currProposal;
    }
	}

  //checks if there is a spell corresponding to int target
  bool DeckContains(int target) {
    for(int i = 0; i < DECKSIZE; ++i)
    {
      int temp;
      temp = PlayerPrefs.GetInt("Spell_" + i, -1);
      if (temp == target)
      {
        return true;
      }
    }
    return false;
  }
}
