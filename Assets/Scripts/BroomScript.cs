using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomScript : MonoBehaviour {
	public GameObject player;
	public GameObject head;
	// public GameObject headReferenceObject;
	// private HeadReference headReference;
	private float currentPressure=0f;
	private int acceleration = 10;
	// private float broomHeight = 222.5f;
	private bool isMounted = false;
	private Rigidbody prb;
	private int yrotatespeed = 1;
	private float zrotatespeed = 0.5f;
	private Vector3 mountLocation;
	private int maxSpeed = 15;
	private float maxDeviate = 0.1f;
	private float broomD = 0.75f;
	// Use this for initialization
	void Start () {
		prb = player.GetComponent<Rigidbody>();
		// headReference = headReferenceObject.GetComponent<HeadReference>();
		// broomHeight = head.transform.position.y-broomD;
	}

	// Update is called once per frame
	void Update () {

	}
	void FixedUpdate(){
		if(isMounted){
			Vector3 difference = head.transform.localPosition-mountLocation;
			float speed = prb.velocity.magnitude;
			if(difference.x<maxDeviate){
				player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed*difference.x*speed);
			}else{
				player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed*maxDeviate*speed);
			}
			if(difference.z<maxDeviate){
				player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed*difference.z*speed);
			}else{
				player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed*maxDeviate*speed);
			}
			prb.AddForce(player.transform.forward*acceleration*currentPressure);
			prb.velocity = transform.forward*prb.velocity.magnitude;
			if(prb.velocity.magnitude>maxSpeed){
				prb.velocity = transform.forward*maxSpeed;
			}
            //stop button
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                bool flag = true;
                prb.velocity = transform.forward * 0;
                while (flag)
                {
                    if (OVRInput.GetDown(OVRInput.Button.One))
                    {
                        flag = false;
                    }
                }
            }
		}
	}
	public void UpdateCurrentPressure(float f){
		currentPressure = f;
	}
	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag != "Town"){
			Mount();

			// headReference.Mount();
		}
	}
	public void Mount(){
		if(!isMounted){
			Debug.Log("mounted");
			mountLocation = head.transform.localPosition;
			isMounted = true;
			transform.parent = player.transform;
			transform.position = new Vector3(head.transform.position.x, head.transform.position.y-broomD, head.transform.position.z);
		}
	}
}
