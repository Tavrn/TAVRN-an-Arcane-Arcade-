using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel_GameManager : MonoBehaviour {
	public GameObject lights;

	void Start () {
		StartGame();
	}

	public void StartGame(){
		lights.GetComponent<Animator>().SetTrigger("StartGame");
	}
}
