using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloakParentScript : MonoBehaviour {
	public Transform head;
  private Vector3 offset = new Vector3(0f, -1.7f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        transform.position = head.position + offset;
        transform.eulerAngles = new Vector3(0f, head.rotation.y, 0f);
    }
}
