﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using VRTK;
public class Duel_PlayerScript : NetworkBehaviour {
	public bool isLive;
	public Transform head;
	public GameObject posSlot;
	public GameObject myTargetCollider;
	public GameObject wandtip;
	[SyncVar]
	public int HP = 100;
	[SyncVar]
	public int mana = 100;

	private float pZ = 0.6f;
	private float zZ = 0.3f;
	void Start () {
		if(isLive){
			if(gameObject.name.Contains("Multi")){
				if(isLocalPlayer){
					VRTK_SDKManager m = gameObject.AddComponent(typeof(VRTK_SDKManager)) as VRTK_SDKManager;
					m.scriptAliasLeftController = transform.Find("VRTK_Scripts").Find("LeftHand").gameObject;
					m.scriptAliasRightController = transform.Find("VRTK_Scripts").Find("RightHand").gameObject;
					VRTK_SDKSetup[] list = {transform.Find("NewtonSDK").GetComponent<VRTK_SDKSetup>()};
					m.TryLoadSDKSetup(0, true, list);
					transform.Find("VRTK_Scripts").Find("LeftHand").GetComponent<VRTK_ControllerEvents>().enabled = true;
					transform.Find("VRTK_Scripts").Find("RightHand").GetComponent<VRTK_ControllerEvents>().enabled = true;
					CollidersScript c = GameObject.Find("Colliders").GetComponent<CollidersScript>();
					c.head = head.gameObject;
					c.WandTip = wandtip;
					c.wandScript = c.WandTip.GetComponent<WandScript>();
				}
			}
			else
			{
				CollidersScript c = GameObject.Find("Colliders").GetComponent<CollidersScript>();
				c.head = head.gameObject;
				c.WandTip = wandtip;
				c.wandScript = c.WandTip.GetComponent<WandScript>();
			}
		}
	}
	public void tryHit(float zhit, int damage){
		if(gameObject.name.Contains("Multi")){
			if(!isServer){
				return;
			}
		}
		float posSlotZ = posSlot.transform.position.z;
		if(posSlotZ>0.5f && zhit > 1/3f){
			hit(damage);
		}else if(posSlotZ<0.5f && posSlotZ>-0.5f && zhit<1/3f && zhit>-1/3f){
			hit(damage);
		}else if(posSlotZ<-0.5f && zhit<-1/3f){
			hit(damage);
		}
	}
	public void hit(int damage){
		HP = Mathf.Clamp(HP-damage, 0, 100);
	}
	// Update is called once per frame
	void Update () {
		// Vector3 mtcp = myTargetCollider.transform.position;
		// myTargetCollider.transform.position =  new Vector3(head.transform.position.x, mtcp.y, mtcp.z);
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
