using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour {
	private Color alb;
	// Use this for initialization
	void Start () {
		alb = transform.Find("Bottle").GetComponent<MeshRenderer>().materials[1].color;
		// print(alb);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider c){
		print(alb);
		// print(gameObject.name);
	}
}
