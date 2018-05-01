using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour {
	public int number;
	// Use this for initialization
	void Start () {
		number = int.Parse(gameObject.name.Substring(4, 2));
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag=="Broom"){
			if((transform.position-c.transform.position).magnitude<5){
				c.transform.root.GetComponent<TownPlayer>().HitHoop(number, gameObject);
			}
		}
	}
}
