using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour {
	public Color emissionColor;
	public Material wandMat;
	public MeshRenderer mshr;
	public int matNum;

	// Use this for initialization
	void Start () {
		// print(alb);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider c){
		if(c.transform.root.tag=="Player"){
			c.transform.root.GetComponent<HandShrink>().emissionColor = emissionColor;
			mshr.material = wandMat;
			PlayerPrefs.SetInt("wandMatNum", matNum);
		}
	}
}
