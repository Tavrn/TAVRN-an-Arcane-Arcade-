using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookParentScript : MonoBehaviour {
	public GameObject hand;
	public GameObject openBook;
	public GameObject closedBook;
	private bool open = true;
	private int deckSize = 10;
	private int spellPage = 0;
	private List<string> glyphs = new List<string>();
	private Transform glyphsParent;
	// Use this for initialization
	void Start () {
		int count = 0;
		glyphsParent = openBook.transform.Find("GlyphsParent");
		foreach(Transform r in glyphsParent){
			glyphs.Add(r.gameObject.name);
			if(count>0){
				r.gameObject.SetActive(false);
			}
			count++;
		}
		deckSize = count;
		ToggleOpen();
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
		if(open){
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(false);
			spellPage++;
			if(spellPage==deckSize)
				spellPage = 0;
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(true);
		}
	}
	public void FlipLeft(){
		if(open){
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(false);
			spellPage--;
			if(spellPage==-1)
				spellPage = deckSize-1;
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(true);
		}
	}
}
