using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
//using UnityEngine.SceneManagement;

public class AutoNetMM : NetworkLobbyManager {

/*
	void Awake()
	{
		//Delagates the function
	  //SceneManager.activeSceneChanged += OnSceneWasSwitched;
	}

	void OnSceneWasSwitched()
	{
		want everything to be destroyed when you go back to the tavern
		Debug.Log("OnSceneWasSwitched");
	  if (SceneManager.GetActiveScene().name == "Tavern")
		{
			MMDestroyMatch();
		}
	}
*/
	void Start ()
	{
		//Change this to happen when you go through the door instead
  	// MMStart();
  	// MMListMatches();
	}
	public void MMStart()
  {
    Debug.Log("MMStart");

    this.StartMatchMaker();
		MMListMatches();
  }

  void MMListMatches()
	{
    Debug.Log("MMListMatches");

		if(this.playScene == "Town_Multiplayer")
		{
	 		this.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
		}
		else
		{
    	this.matchMaker.ListMatches(0, 20, "", true, 0, 1, OnMatchList);
		}
  }

  public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
  {
    Debug.Log("OnMatchList");

    base.OnMatchList(success, extendedInfo, matchList);

    if(!success)
    {
      Debug.Log("List failed: " + extendedInfo);
    }
    else
    {
      if (matchList.Count > 0)
      {
        Debug.Log("Listed matches; first: " + matchList[0]);
        MMJoinMatch(matchList[0]);
      }
      else
      {
        // no rooms open
        MMCreateMatch();
      }
    }
	}

  void MMJoinMatch(MatchInfoSnapshot firstMatch)
  {
    Debug.Log("MMJoinMatch");

		if(this.playScene == "Town_Multiplayer")
		{
    	this.matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoined);
		}
		else
		{
    	this.matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 1, OnMatchJoined);
		}
  }

  public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
    Debug.Log("OnMatchJoined");

    base.OnMatchJoined(success, extendedInfo, matchInfo);

    if(!success)
    {
      Debug.Log("Failed to join match: " + extendedInfo);
    }
    else
    {
      // Success
      Debug.Log("Successfully joined: " + matchInfo.networkId);
			//StartClient();
    }
  }

  void MMCreateMatch()
  {
    Debug.Log("MMCreateMatch");

		if(this.playScene == "Town_Multiplayer")
		{
    	this.matchMaker.CreateMatch("MM", 2, true, "", "", "", 0, 0, OnMatchCreate);
		}
		else
		{
    	this.matchMaker.CreateMatch("MM", 2, true, "", "", "", 0, 1, OnMatchCreate);
		}
  }

  public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
  {
    Debug.Log("OnMatchCreate");
    base.OnMatchCreate(success, extendedInfo, matchInfo);

    if(!success)
    {
      Debug.Log("failed to create: " + extendedInfo);
    }
    else
    {
      //success
      Debug.Log("successful creation: " + matchInfo.networkId);
			//StartHost();
  	}
	}

/*
	void MMDestroyMatch()
	{
		StopServer();
		StopClient();
		StopHost();
		StopMatchMaker();
	}
*/
}
