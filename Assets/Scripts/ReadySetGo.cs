using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadySetGo : MonoBehaviour {
	public float timeToSwitch = 3f;
	public int stage = 0;
	private float prevTime;
	private TextMesh text;
	void Start () {
		prevTime = Time.time;
		text = GetComponent<TextMesh>();
	}

	void Update () {
		if(Time.time>prevTime+timeToSwitch){
			stage++;
			prevTime = Time.time;
		}
		if(stage==1){
			text.text = "Set!";
		}else if(stage==2){
			text.text = "Go!";
		}else if(stage==3){
			Destroy(gameObject);
		}
	}
}
