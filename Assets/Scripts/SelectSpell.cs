using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSpell : MonoBehaviour {
  public Material dimMat;
  public Material brightMat;
  public bool isParent;
  public int spellNumber;
  private MeshRenderer mr;
  private SelectSpell parentScript;
	// Use this for initialization
	void Start () {
    if(!isParent){
      if(transform.childCount>0){
        foreach(Transform t in transform){
          t.gameObject.SetActive(false);
        }
      }
      mr = GetComponent<MeshRenderer>();
      parentScript = transform.parent.GetComponent<SelectSpell>();
    }else{
      foreach(Transform t in transform){
        t.gameObject.SetActive(true);
      }
    }
	}
  public void ParentDim(bool b){
    foreach(Transform t in transform){
      if(b){
        t.GetComponent<MeshRenderer>().material = dimMat;
      }else{
        t.GetComponent<MeshRenderer>().material = brightMat;
      }
    }
  }
	void OnTriggerEnter(Collider col)
	{
		DeckSelect.currProposal = spellNumber;
    if(!isParent){
      if(transform.childCount>0){
        foreach(Transform t in transform){
          t.gameObject.SetActive(true);
        }
      }
      // mr.enabled = false;
      parentScript.ParentDim(true);
    }
	}
  void OnTriggerExit(Collider c){
    if(!isParent){
      if(transform.childCount>0){
        foreach(Transform t in transform){
          t.gameObject.SetActive(false);
        }
      }
      // mr.enabled = true;
      parentScript.ParentDim(false);
    }
  }
}
