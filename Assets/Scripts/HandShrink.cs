using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShrink : MonoBehaviour {
	private float baseHeight;
	private bool changingVol = false;
	private GameObject hand;
	private AudioSource music;
	// Use this for initialization
	void Start () {
		music = GameObject.Find("Music").GetComponent<AudioSource>();
	}

	public void StartChangingVolume(float baseH, GameObject g){
		changingVol = true;
		baseHeight = baseH;
		hand = g;
	}
	public void StopChangingVolume(){
		changingVol = false;
	}

	// Update is called once per frame
	void Update () {
		if(changingVol){
			float vol = Mathf.Clamp(hand.transform.position.y-baseHeight, 0f, 1f);
			music.volume = vol;
			PlayerPrefs.SetFloat("Volume", vol);
		}
	}
}
