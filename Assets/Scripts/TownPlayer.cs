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
	public void Finish(){
		if(gameObject.name.Contains("Multi")){

		}else{
			Invoke("ReturnToTavern", 5);
		}
	}
	public void ReturnToTavern(){
		SceneManager.LoadScene("Tavern");
	}
}
