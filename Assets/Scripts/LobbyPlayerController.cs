using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyPlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("START OF LOBBY PLAYER CONTROLLER");
		gameObject.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
	}

	// Update is called once per frame
	void Update () {

	}

	//consider killing yourself on scene change
}
