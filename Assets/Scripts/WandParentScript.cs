﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandParentScript : MonoBehaviour {
	public GameObject hand;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.position = hand.transform.position;
		transform.rotation = hand.transform.rotation;
	}
	}
