using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {
	public int hitPoints = 1;

	public void Hit(){
		hitPoints -= 1;
		if(hitPoints<=0){
			Destroy(gameObject);
		}
	}
}
