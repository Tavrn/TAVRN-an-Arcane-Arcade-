﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour {
	public Material wandMat;
	public MeshRenderer mshr;
	// Use this for initialization
	void Start () {
		// print(alb);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider c){
		if(c.transform.root.tag=="Player"){
				mshr.material = wandMat;
		}
	}
}
