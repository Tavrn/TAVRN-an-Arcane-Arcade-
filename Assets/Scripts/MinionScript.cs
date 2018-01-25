using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionScript : MonoBehaviour {
	[Tooltip("0=Fire, 1=Water, 2=Sand, 3=Air")]
	public int type;
	private int speed = 1;
	private int direction = 1;
	void Start () {
		if(transform.position.x>0){
			direction = -1;
		}
	}

	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){
		transform.position += (speed*direction*Time.deltaTime*new Vector3(1,0,0));
	}
}
