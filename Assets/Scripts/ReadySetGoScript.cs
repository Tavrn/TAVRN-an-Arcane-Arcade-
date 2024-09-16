using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadySetGoScript : NetworkBehaviour {
	public float timeToChange = 2f;

	private TextMesh text;
	[SyncVar]
	private int stage = 0;
	private float prevTime;
	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh>();
		prevTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if(stage == 1){
			text.text = "Set!";
		}else if(stage==2){
			text.text = "Go!";
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")){
				g.GetComponent<TownPlayer>().setStartTime(Time.time);
			}
		}else if(stage==3){
			Destroy(gameObject);
		}
		if(!isServer){
			return;
		}
		if(Time.time>prevTime+timeToChange){
			prevTime = Time.time;
			stage += 1;
		}
	}
}
