using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPIndicatorScript : MonoBehaviour {
	public Duel_PlayerScript player;
	public GameObject RedP;
	// Update is called once per frame
	void Update () {
		RedP.transform.localScale = new Vector3(1f, 1f, ((float) player.HP)/100);
	}
}
