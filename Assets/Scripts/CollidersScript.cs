using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersScript : MonoBehaviour {
	public GameObject head;
	public GameObject WandTip;
	public GameObject colliderPrefab;
	public GameObject colliderReferenceObject;
	public WandScript wandScript;

	private int height = 5;
	private int width = 5;
	private int depth = 5;
	// Use this for initialization
	void Start () {
		for(int h=-height; h<=height; h++){
			for(int d=-depth; d<=depth; d++){
				for(int w=-width; w<=width; w++){
					GameObject c = Instantiate(colliderPrefab) as GameObject;
					c.transform.parent = transform;
					c.transform.position = new Vector3(w*0.1f,h*0.1f,d*0.1f);
					ColliderScript s = c.GetComponent<ColliderScript>();
					s.x = w;
					s.y = h;
					s.z = d;
					c.name = ""+w+""+h+""+d;
					if(h==0 && d==0 && w==0){
							wandScript.originCollider = c;
					}
				}
			}
		}
	}
	public void JumpToWand(){
		transform.position = WandTip.transform.position;
		colliderReferenceObject.transform.position = new Vector3(head.transform.position.x, transform.position.y, head.transform.position.z);
		transform.LookAt(colliderReferenceObject.transform);
		transform.Rotate(0,180,0);
	}
	// Update is called once per frame
	void Update () {

	}
}
