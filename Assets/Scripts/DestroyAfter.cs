using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour {
	public float lifetime = 3f;
	private bool noParent = false;
	private float startTime = 0f;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(transform.parent == null){
			if(noParent){
				if(Time.time>startTime+lifetime){
					Destroy(gameObject);
				}
			}else{
				noParent = true;
				startTime = Time.time;
			}
		}
	}
}
