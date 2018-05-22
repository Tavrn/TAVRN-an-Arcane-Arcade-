using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour {
	public int number;
	public bool isBackwards;
	// Use this for initialization
	void Start () {
		number = int.Parse(gameObject.name.Substring(4, 2));
		if(isBackwards){
			transform.RotateAround(transform.position, transform.up, 180);
		}
	}
	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag=="Broom"){
			if((transform.position-c.transform.position).magnitude<5){
				TownPlayer p = c.transform.root.GetComponent<TownPlayer>();
				if(p.IsMulti()){
					if(p.isLocalPlayer){
						c.transform.root.GetComponent<TownPlayer>().HitHoop(number, gameObject);
					}
				}else{
					c.transform.root.GetComponent<TownPlayer>().HitHoop(number, gameObject);
				}
			}
		}
	}
}
