﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellGeneric : MonoBehaviour {

	void Start () {

	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag=="Duel_TargetCollider"){
			if(c.transform.root!=transform.root){
				Trigger(transform.position, c.transform.root.GetComponent<Duel_PlayerScript>(), c.transform.root.GetComponent<SpellManagerScript>());
			}
		}else if(c.gameObject.tag=="Duel_Shield"){
			if(c.transform.root!=transform.root){
				GetComponent<SphereCollider>().enabled = false;
				c.GetComponent<ShieldScript>().Hit();
				Destroy(gameObject);
			}
		}
	}
	public virtual void Trigger(Vector3 v, Duel_PlayerScript p, SpellManagerScript s){
		Debug.Log("generic spell triggered");
	}
}
