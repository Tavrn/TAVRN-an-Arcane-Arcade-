using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayer : MonoBehaviour {
	public int prevHoop = 0;
	public Material passedHoopMat;
	public Material nextHoopMat;


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
			GameObject.Find(num).GetComponent<MeshRenderer>().material = nextHoopMat;
		}
	}
}
