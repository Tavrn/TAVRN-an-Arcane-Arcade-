using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using VRTK;

public class TownPlayer : NetworkBehaviour {
	public int prevHoop = 0;
	public Material passedHoopMat;
	public Material nextHoopMat;
	public AudioSource hoopMusic;
	private bool finished = false;
	private int numHoops = 41;
	private Vector3 startPos;
	private Quaternion startRot;
	private bool isMulti;
	private float startTime=-1f;

	void Start () {
		Debug.Log(gameObject);
		isMulti = gameObject.name.Contains("Multi");
		if(isMulti){
			if(isLocalPlayer){
				VRTK_SDKManager m = gameObject.AddComponent(typeof(VRTK_SDKManager)) as VRTK_SDKManager;
				m.scriptAliasLeftController = transform.Find("VRTK_Scripts").Find("LeftHand").gameObject;
				m.scriptAliasRightController = transform.Find("VRTK_Scripts").Find("RightHand").gameObject;
				VRTK_SDKSetup[] list = {transform.Find("NewtonSDK").GetComponent<VRTK_SDKSetup>()};
				m.TryLoadSDKSetup(0, true, list);
				transform.Find("VRTK_Scripts").Find("LeftHand").GetComponent<VRTK_ControllerEvents>().enabled = true;
				transform.Find("VRTK_Scripts").Find("RightHand").GetComponent<VRTK_ControllerEvents>().enabled = true;
			}
		}
		startPos = transform.position;
		startRot = transform.rotation;
	}
	public void setStartTime(float f){
		startTime = f;
	}
	public float getStartTime(){
		return startTime;
	}
	public bool IsMulti(){
		return isMulti;
	}
	public void HitHoop(int number, GameObject h){
		if(!isMulti){
			if(number==prevHoop+1){
				hoopMusic.Play();
				prevHoop = number;
				h.GetComponent<MeshRenderer>().material = passedHoopMat;
				string num = "Hoop";
				if(prevHoop<9){
					num += "0" + (prevHoop+1);
				}else{
					num += (prevHoop+1) + "";
				}
				if(prevHoop<40){
					GameObject.Find(num).GetComponent<MeshRenderer>().material = nextHoopMat;
				}else{
					Finish();
				}
			}
		}else{
			if(isLocalPlayer){
				if(number==prevHoop+1){
					hoopMusic.Play();
					prevHoop = number;
					h.GetComponent<MeshRenderer>().material = passedHoopMat;
					string num = "Hoop";
					if(prevHoop<9){
						num += "0" + (prevHoop+1);
					}else{
						num += (prevHoop+1) + "";
					}
					if(prevHoop<40){
						GameObject.Find(num).GetComponent<MeshRenderer>().material = nextHoopMat;
					}else{
						Finish();
					}
				}
			}
		}
	}
	public bool isFinished(){
		return finished;
	}
	public void ResetToHoop(){
		if(prevHoop>0){
			string num = "Hoop";
			if(prevHoop<9){
				num += "0" + (prevHoop);
			}else{
				num += (prevHoop) + "";
			}
			GameObject pHoop = GameObject.Find(num);
			Vector3 pos = pHoop.transform.position;
			transform.position = pos;
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.MovePosition(pos);
			rb.velocity = Vector3.zero;
			transform.rotation = pHoop.transform.rotation;
		}else{
			transform.position = startPos;
			transform.rotation = startRot;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}
	[Command]
	public void CmdSetFinished(){
		finished = true;
	}
	[ClientRpc]
	public void RpcSetFinished(){
		finished = true;
	}
	public void SetFinished(){
		finished = true;
		if(isServer){
			RpcSetFinished();
		}else{
			CmdSetFinished();
		}
	}
	public void Finish(){
		if(gameObject.name.Contains("Multi")){
			SetFinished();
			foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player")){
				if(p.GetComponent<NetworkIdentity>().netId != GetComponent<NetworkIdentity>().netId){
					if(p.GetComponent<TownPlayer>().isFinished()){
						print("lose " + (Time.time-startTime));
						GameObject.Find("Text").GetComponent<TextMesh>().text = "Defeat \n in \n" + (Time.time-startTime) + "\n seconds!";
						//LOSE
						Invoke("EndMultiMatch", 5);
					}else{
						print("win " + (Time.time-startTime));
						GameObject.Find("Text").GetComponent<TextMesh>().text = "Victory \n in \n" + (Time.time-startTime) + "\n seconds!";
						//WIN
					}
				}
			}
			// GameObject.Find("MultiplayerBroomManager").GetComponent<MultiplayerBroomManager>().Finish();
		}else{
			Invoke("ReturnToTavern", 5);
		}
	}
	public void ReturnToTavern(){
		SceneManager.LoadScene("Tavern");
	}
	public void EndMultiMatch(){
		GameObject.Find("AutoNetMM").GetComponent<AutoNetMM>().MMEndMatch(); //destroys match
	}
	void OnTriggerEnter(Collider c){
		if(c.transform.root.tag=="Town"){
			ResetToHoop();
		}
	}
}
