using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadReference : MonoBehaviour {
	public GameObject head;
	private Vector3 start;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = head.transform.localPosition;
	}
	public void Mount(){
		start = transform.localPosition;
	}
	public Vector3 getDifference(){
		return transform.position - start;
	}
}
