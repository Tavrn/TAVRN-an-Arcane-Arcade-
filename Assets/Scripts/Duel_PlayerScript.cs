using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel_PlayerScript : MonoBehaviour {
	public Transform head;
	public GameObject posSlot;
	public GameObject myTargetCollider;
	public int HP = 100;
	private float pZ = 0.6f;
	private float zZ = 0.3f;
	// private static Vector3 leftPos = new Vector3(posSlot.transform.position.x, posSlot.transform.position.y, 2/3f);
	// private static Vector3 midPos = new Vector3(posSlot.transform.position.x, posSlot.transform.position.y, 0);
	// private static Vector3 rightPos = new Vector3(posSlot.transform.position.x, posSlot.transform.position.y, -2/3f);
	void Start () {
		//TODO HERE
		//if(isLocalPlayer){
			myTargetCollider.layer = 8;
		//}
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
