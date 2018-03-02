using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIndicatorScript : MonoBehaviour {
	public Duel_PlayerScript player;
	public GameObject RedP;
	public Transform head;

	private Vector3 d = new Vector3(0,1,0);
	// Update is called once per frame
	void Update () {
		transform.position = head.position + d;
		RedP.transform.localScale = new Vector3(1f, 1f, ((float) player.HP)/100);
	}
}
