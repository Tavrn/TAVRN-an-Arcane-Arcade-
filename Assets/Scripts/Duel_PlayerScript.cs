using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel_PlayerScript : MonoBehaviour {
	public Transform head;
	public GameObject posSlot;
	private float pZ = 0.6f;
	private float zZ = 0.3f;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float headZ = head.position.z;
		if(headZ>pZ){
			posSlot.transform.position = new Vector3(posSlot.transform.position.x, posSlot.transform.position.y, 2/3f);
		}else if(headZ<zZ && headZ>-zZ){
			posSlot.transform.position = new Vector3(posSlot.transform.position.x, posSlot.transform.position.y, 0);
		}else if(headZ<-pZ){
			posSlot.transform.position = new Vector3(posSlot.transform.position.x, posSlot.transform.position.y, -2/3f);
		}
	}

}
