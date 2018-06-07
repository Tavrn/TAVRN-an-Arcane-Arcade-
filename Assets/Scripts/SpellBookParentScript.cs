using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookParentScript : MonoBehaviour {
	public GameObject hand;
	public GameObject openBook;
	public GameObject closedBook;
	public List<AudioClip> bookCloses = new List<AudioClip>();
	public List<AudioClip> pageFlips = new List<AudioClip>();

	private bool open = true;
	private int deckSize = 10;
	private int spellPage = 0;
	private List<string> allGlyphs = new List<string>(); //ADDED FOR REWRITE
	private List<string> glyphs = new List<string>();
	private AudioSource au;
	private Transform glyphsParent;
	// Use this for initialization
	void Start () {
		int count = 0;
		glyphsParent = openBook.transform.Find("GlyphsParent");
		au = GetComponent<AudioSource>();
		//BEGIN OF REWRITE

		//fills a list with all possible spells
		foreach(Transform r in glyphsParent){
			allGlyphs.Add(r.gameObject.name);
			r.gameObject.SetActive(false); //used to leave the first one active
			count++;
		}

		for(int i = 0; i < deckSize; ++i)
		{
			int j = PlayerPrefs.GetInt(("Spell_" + i), -1);
			if (j < allGlyphs.Count && j >= 0)
			{
				glyphs.Add(allGlyphs[j]);
			}
		}

		//END OF REWRITE

		//OLD CODE
		/*
		foreach(Transform r in glyphsParent){
			glyphs.Add(r.gameObject.name);
			if(count>0){
				r.gameObject.SetActive(false);
			}
			count++;
		}

		deckSize = count;
		*/

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
			Debug.Log("Called");
			openBook.SetActive(true);
			closedBook.SetActive(false);
			FlipLeft();
			FlipRight();
		}else{
			au.clip = bookCloses[(int)(Random.Range(0,3))];
			au.Play();
			openBook.SetActive(false);
			closedBook.SetActive(true);
		}
	}
	public void FlipRight(){
		if(open){
			au.clip = pageFlips[(int)Random.Range(0, 4)];
			au.Play();
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(false);
			spellPage++;
			if(spellPage==deckSize)
				spellPage = 0;
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(true);
		}
	}
	public void FlipLeft(){
		if(open){
			au.clip = pageFlips[(int)Random.Range(0, 4)];
			au.Play();
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(false);
			spellPage--;
			if(spellPage==-1)
				spellPage = deckSize-1;
			glyphsParent.Find(glyphs[spellPage]).gameObject.SetActive(true);
		}
	}
}
