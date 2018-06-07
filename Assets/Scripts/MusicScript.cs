using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music", 0.1f);
	}

	// Update is called once per frame
	void Update () {

	}
}
