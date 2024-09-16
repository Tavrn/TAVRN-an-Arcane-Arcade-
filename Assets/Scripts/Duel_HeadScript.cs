using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel_HeadScript : MonoBehaviour {
	// Use this for initialization
	// public Transform wandTip;
	// private GameObject colliderReferenceObject;
	void Start () {
		GameObject.Find("Colliders").GetComponent<CollidersScript>().head = gameObject;
	}

	// Update is called once per frame
	void Update () {
		// colliderReferenceObject.transform.position = new Vector3(transform.position.x, wandTip.position.y, transform.position.z);
	}
}
