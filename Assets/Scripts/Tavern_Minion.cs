using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern_Minion : MonoBehaviour {
	private Animator animator;
	private float p = 0.5f;
	private float step;
	private float lastCheck;
	private bool leanForward = false;
	void Start () {
		animator = GetComponent<Animator>();
		step = Random.Range(2f, 5f);
	}

	// Update is called once per frame
	void Update () {
		if(Time.time>lastCheck + step){
			lastCheck = Time.time;
			float cf = Random.Range(0f, 1f);
			if(cf<p){
				leanForward = !leanForward;
				animator.SetBool("LeanForward", leanForward);
			}
		}
	}
}
