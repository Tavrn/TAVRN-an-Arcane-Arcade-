using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSpell : MonoBehaviour {

  public int spellNumber;

	// Use this for initialization
	void Start () {
    if(transform.childCount>0){
      transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col)
	{
		DeckSelect.currProposal = spellNumber;
    if(transform.childCount>0){
      transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }
	}
  void OnTriggerExit(Collider c){
    if(transform.childCount>0){
      transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
  }
}
