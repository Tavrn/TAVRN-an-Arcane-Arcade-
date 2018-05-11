using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class AutoNetMM : NetworkLobbyManager {

	void Start () {
        MMStart();
        MMListMatches();
	}

	void MMStart()
    {
        Debug.Log("MMStart");

        this.StartMatchMaker();
    }

    void MMListMatches()
    {
        Debug.Log("MMListMatches");

        this.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
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

        this.matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoined);
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
        }
    }

    void MMCreateMatch()
    {
        Debug.Log("MMCreateMatch");

        this.matchMaker.CreateMatch("MM", 2, true, "", "", "", 0, 0, OnMatchCreate);
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
        }
    }

}
