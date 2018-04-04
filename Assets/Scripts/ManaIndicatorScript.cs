using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaIndicatorScript : MonoBehaviour {
	public Duel_PlayerScript player;
	public GameObject RedP;
	public Transform head;

	private Vector3 d = new Vector3(0,1,0);
	private bool isHud = false;
	void Start(){
		isHud = !(transform.parent.tag == "Player");
	}
	// Update is called once per frame
	void Update () {
		if(!isHud){
			transform.position = head.position + d;
		}
		RedP.transform.localScale = new Vector3(1f, 1f, ((float) player.mana)/100);
	}
}
