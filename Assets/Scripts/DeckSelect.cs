using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSelect : MonoBehaviour {

  //public GameObject userDeckPrefab = userDeckPrefab;

  private int currentPage = 0;
  private int spellNum = -1;

  public TextMesh pgTxt;
  public TextMesh spTxt;

	// Update is called once per frame
  void Start() {
    //resets deck each time you enter the tavern for testing
    for(int i = 0; i < 10; ++i)
    {
      PlayerPrefs.SetInt("Spell_" + i, i);
    }
	}

	void Update () {

	}

	void FlipRight() {
    currentPage = currentPage + 1;
    currentPage = currentPage % 10;
    pgTxt.text = " Page \n number: \n" + currentPage;
    spTxt.text = " Spell \n number: \n" +  PlayerPrefs.GetInt("Spell_" + currentPage, -1);
	}

	void FlipLeft() {
    currentPage = currentPage - 1;
    currentPage = currentPage % 10;
    pgTxt.text = " Page \n number: \n" + currentPage;
    spTxt.text = " Spell \n number: \n" +  PlayerPrefs.GetInt("Spell_" + currentPage, -1);
	}

	void AddPage() {
    if(DeckContains(spellNum) == true)
    {
      PlayerPrefs.SetInt("Spell_" + currentPage, spellNum);
      spTxt.text = " Spell \n number: \n" + spellNum;
    }
	}

   bool DeckContains(int target) {
    for(int i = 0; i < 10; ++i)
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
