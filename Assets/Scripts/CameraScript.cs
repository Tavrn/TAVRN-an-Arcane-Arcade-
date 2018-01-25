using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public int maxX, maxZ, minX, minZ;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float x = transform.position.x;
		float z = transform.position.z;
		if (x < minX) {
			transform.position = new Vector3 (minX, transform.position.y, transform.position.z);
		} else if (x > maxX) {
			transform.position = new Vector3 (maxX, transform.position.y, transform.position.z);
		}
		Debug.Log(transform.position.x);
		if (z < minZ) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, minZ);
		} else if (z > maxZ) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, maxZ);
		}
	}
}
