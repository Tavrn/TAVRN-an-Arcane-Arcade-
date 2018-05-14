using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownPlayer : MonoBehaviour {
	public int prevHoop = 0;
	public Material passedHoopMat;
	public Material nextHoopMat;
	private int numHoops = 41;
	private Vector3 startPos;
	private Quaternion startRot;

	void Start () {
		startPos = transform.position;
		startRot = transform.rotation;
	}

	public void HitHoop(int number, GameObject h){
		if(number==prevHoop+1){
			prevHoop = number;
			h.GetComponent<MeshRenderer>().material = passedHoopMat;
			string num = "Hoop";
			if(prevHoop<9){
				num += "0" + (prevHoop+1);
			}else{
				num += (prevHoop+1) + "";
			}
			if(prevHoop<41){
				GameObject.Find(num).GetComponent<MeshRenderer>().material = nextHoopMat;
			}else{
				Finish();
			}
		}
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
	public void Finish(){
		if(gameObject.name.Contains("Multi")){

		}else{
			Invoke("ReturnToTavern", 5);
		}
	}
	public void ReturnToTavern(){
		SceneManager.LoadScene("Tavern");
	}
	void OnTriggerEnter(Collider c){
		if(c.transform.root.tag=="Town"){
			ResetToHoop();
		}
	}
}
