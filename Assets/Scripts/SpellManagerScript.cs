using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using CNamespace;
public class SpellManagerScript : NetworkBehaviour {
	public Transform wandTip;
	public Transform wandHandle;
	public WandScript wandScript;
	public Transform spellsParent;
	public GameObject aimLine;
	[Space(10)]
	public int weather = 0; //0 = clear, 1 = fire, 2 = rain, 3 = sandstorm, 4 = wind
	[Space(10)]
	public Transform myMinionSpawn;
	public GameObject minionPrefab;
	public Transform meteorSpawn;
	[Space(10)]
	public Material clearSkiesSkybox;
	public Material fireSkybox;
	public Material rainSkybox;
	public Material sandSkybox;
	public Material windSkybox;
	[Space(10)]
	public GameObject fireballPrefab;
	public GameObject confettiPrefab;
	public GameObject magicMissilePrefab;
	public GameObject arcaneSpherePrefab;
	public GameObject aquaOrbPrefab;
	public GameObject bubblePrefab;
	public GameObject scalingShotPrefab;
	public GameObject lightningBoltPrefab;
	public GameObject meteorPrefab;
	public GameObject shieldPrefab;
	public GameObject healEffectPrefab;
	public GameObject conversionEffectPrefab;

	private int cuedSpellNum = -1;
	private bool aiming = false;
	List<List<Coordinate>> spellCompendium = new List<List<Coordinate>>();
	List<string> spellNameCompendium = new List<string>();
	List<int> spellManaCosts = new List<int>();

	List<float> effectEndTimes = new List<float>();
	List<string> effectNames = new List<string>();
	List<float> effectPrevTimes = new List<float>();
	List<float> effectTickL = new List<float>();

	private Duel_PlayerScript myPlayer;
	private bool isMulti = false;

	void Start () {
		isMulti = transform.root.name.Contains("Multi");
		SetUpSpells();
		myPlayer = GetComponent<Duel_PlayerScript>();
		BasicManaRegenStart();
	}
	void FixedUpdate(){
		if(effectEndTimes.Count>0){
			for(int i=0; i<effectEndTimes.Count; i++){
				if(effectPrevTimes[i]+effectTickL[i]<Time.time){
					effectPrevTimes[i] = Time.time;
					// if(!NetworkServer.active){
					// 	Invoke("Cmd"+effectNames[i], 0f);
					// }else{
					Invoke(effectNames[i], 0f);
					// }
				}
				float end = effectEndTimes[i];
				if(end<Time.time){
					effectNames.RemoveAt(i);
					effectEndTimes.RemoveAt(i);
					effectTickL.RemoveAt(i);
					effectPrevTimes.RemoveAt(i);
				}
			}
		}
	}
	void SetUpSpells(){
		CreateSpell("MagicMissile", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(-1,1,1) });
		CreateSpell("ArcaneSphere", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0)});
		CreateSpell("ScalingShot", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0), new Coordinate(1,1,0) });
		CreateSpell("LightningBolt", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0), new Coordinate(0,2,0), new Coordinate(1, 2, 0), new Coordinate(1, 3, 0) });
		CreateSpell("AquaOrb", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, -1, 1) });
		CreateSpell("Bubble", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, 0, 0), new Coordinate(-1, 0, 1), new Coordinate(0, 0, 1), new Coordinate(0, 1, 1) });
		CreateSpell("Fireball", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1) });
		CreateSpell("Meteor", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-3, 1, 1) });
		CreateSpell("I_Conversion", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(0, 1, 1) });
		CreateSpell("I_Heal", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-1,2,1) });
		CreateSpell("I_WeatherClear", 10, new Coordinate[] {new Coordinate(0,0,0), new Coordinate(0,0,1),new Coordinate(0, 1, 1), new Coordinate(0, 1, 2)});
		CreateSpell("I_WeatherFire", 10, new Coordinate[] {new Coordinate(0,0,0), new Coordinate(0,1,0),new Coordinate(-1, 1, 0), new Coordinate(-1, 2, 0)});
		CreateSpell("I_WeatherWater", 10, new Coordinate[] {new Coordinate(0,0,0), new Coordinate(0, -1,0),new Coordinate(-1, -1, 0), new Coordinate(-1, -2, 0)});


		CreateSpell("I_Convert", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0), new Coordinate(-1, 2, 0) });
		CreateSpell("I_SpawnMinion", 10, new Coordinate[] {new Coordinate(0,0,0), new Coordinate(0,0,1), new Coordinate(0,0,2)});
		CreateSpell("I_Confetti", 10, new Coordinate[] {new Coordinate(0,0,0), new Coordinate(0,-1,0), new Coordinate(0,-2,0), new Coordinate(0,-2,-1), new Coordinate(0,-2,-2)});

		CreateSpell("TripleLock", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0), new Coordinate(1,1,0), new Coordinate(2, 1, 0), new Coordinate(2, 2, 0) });
		CreateSpell("WaterHose", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, 0, 0), new Coordinate(-1, 0, 1), new Coordinate(0, 0, 1) });
		CreateSpell("Airslice", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1) });
		CreateSpell("BoomerangBlast", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(1, 0, 1), new Coordinate(0, 0, 1) });
		CreateSpell("Typhoon", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(0, 1, 1), new Coordinate(0, 2, 1), new Coordinate(-1, 2, 1) });
		CreateSpell("StoneShot", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1 ,1) });
		CreateSpell("CentralizedSand", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1,1,0), new Coordinate(2, 1, 0), new Coordinate(2, 0, 0) });
		CreateSpell("PocketSand", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1,1,0), new Coordinate(1, 1, 1), new Coordinate(2, 1, 0), new Coordinate(2, 0, 0) });
		CreateSpell("StalkingFlare", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-2, 1, 1), new Coordinate(-3, 1, 1) });
		CreateSpell("ShockingBlast", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 2, 0), new Coordinate(-1, 2, 1) });
		CreateSpell("Caltrops", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1) });
		CreateSpell("TidalWave", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-2, -1, 0), new Coordinate(-2, -1, 1) });
		CreateSpell("Tailwind", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(0,1,0), new Coordinate(0, 1, 1) });
		CreateSpell("Fissure", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(0, 1, 1) });
		CreateSpell("FlameCarpet", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0,1,0), new Coordinate(-1, 1, 0), new Coordinate(-1, 0, 0), new Coordinate(-1, 0, 1) });
		CreateSpell("Blind", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0) });
		CreateSpell("Invisible", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 1, 1) });
		CreateSpell("Taunt", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-3, 1, 1) });
		CreateSpell("Weaken", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-2, 0, 1) });
		CreateSpell("LifeTie", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 1, 1), new Coordinate(0, 1, 2) });
		CreateSpell("Purge", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-2, 1, 1), new Coordinate(-2, 0, 1) });
		CreateSpell("Immobilize", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-1, 1, 1) });
		CreateSpell("Return", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0), new Coordinate(0, 2, 1), new Coordinate(1, 2, 1) });
		CreateSpell("Counterspell", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-1, 2, 1), new Coordinate(-1, 2, 2), new Coordinate(-1, 1, 2) });
		CreateSpell("Helmets", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 2, 0), new Coordinate(-1, 2, 0), new Coordinate(-1,1,0) });
		CreateSpell("Shields", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0), new Coordinate(-1, 2, 0) });
		CreateSpell("ControlledFlow", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, -2, 0), new Coordinate(-2, -2, 0) });
		CreateSpell("AssistingAir", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(1, 1, 2) });
		CreateSpell("RoughSkin", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(1, 0, 1) });
		CreateSpell("ExplodingGuard", 10, new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 0, 0), new Coordinate(0,0,0) });


	}
	void CreateSpell(string name, int cost, Coordinate[] coordinates){
		List<Coordinate> pat = new List<Coordinate>();
		for(int i=0; i<coordinates.Length; i++){
			pat.Add(coordinates[i]);
		}
		spellNameCompendium.Add(name);
		spellCompendium.Add(pat);
		spellManaCosts.Add(cost);
	}
	int SpellAtPg(int page)
	{
		//checks if page number is in range
		//if so, it will return the spell number corresponding with the spell at that page
		//else it will return -1
		//untested but "should" work
		return(PlayerPrefs.GetInt("Spell_" + page, -1));
	}
	bool DeckContains(int target) {
		//checks target int against the deck stored in player prefs
		//idk if it actually works actually selecting your deck is still wip
		//default deck is {0,1,2,3,4,5,6,7,8,9}
	 	for(int i = 0; i < 13; ++i)
	 	{
		 	int temp;
		 	temp = PlayerPrefs.GetInt("Spell_" + i, -1);
		 	if (temp == target)
		 	{
			 	return true;
		 	}
	 	}
	 	return false;
 }
	public void checkForSpell(){
		List<Coordinate> pattern = wandScript.pattern;
		if(pattern.Count>0){ //make sure the origin collider is in the pattern list
			if(pattern[0].getX()!=0 || pattern[0].getY() != 0 || pattern[0].getZ()!=0){
				pattern.Insert(0, new Coordinate(0,0,0));
			}
		}
		for(int spellNum = 0; spellNum<spellCompendium.Count; spellNum++){ //check pattern against every spell in the compendium
			if(compareSpells(spellCompendium[spellNum], pattern)){
				if(DeckContains(spellNum)){
					if(myPlayer.mana >= spellManaCosts[spellNum]){
						GetComponent<AudioSource>().Play();
						DecreaseMana(spellManaCosts[spellNum]);
						string temp = spellNameCompendium[spellNum];
						if(temp[0] == 'I' && temp[1] == '_'){ //check if spell is instantly cast or not
							Invoke(spellNameCompendium[spellNum], 0f);
							cuedSpellNum = -1;
						}else{
							cuedSpellNum = spellNum;
						}
					}
				}
			}
		}

		wandScript.pattern.Clear();
	}
	private void DecreaseMana(int cost){
		if(isMulti){
			if(NetworkServer.active){ //on server
				myPlayer.mana -= cost;
			}else{
				CmdDecreaseMana(cost);
			}
		}else{
			myPlayer.mana -= cost;
		}
	}
	[Command]
	private void CmdDecreaseMana(int cost){
		myPlayer.mana -= cost;
	}
	bool compareSpells(List<Coordinate> spell1, List<Coordinate> spell2){
		if(spell1.Count!=spell2.Count){
			return false;
		}else{
			bool result = true;
			for(int i=0; i<spell1.Count; i++){
				if(!compareCoordinates(spell1[i], spell2[i])){
					result = false;
				}
			}
			return result;
		}
	}
	bool compareCoordinates(Coordinate a, Coordinate b){
		return a.getX()==b.getX() && a.getY() == b.getY() && a.getZ() == b.getZ();
	}
	public void FireSpell(){
		if(cuedSpellNum!=-1){
			aiming = false;
			aimLine.SetActive(false);
			Invoke(spellNameCompendium[cuedSpellNum], 0f);
			cuedSpellNum = -1;
		}
	}
	[ClientRpc]
	void RpcParentTo(NetworkInstanceId c, NetworkInstanceId p){
		ClientScene.FindLocalObject(c).transform.parent = ClientScene.FindLocalObject(p).transform;
		Debug.Log("rpc called");
		// c.transform.parent = p.transform;
	}
	void BasicManaRegenStart(){
		if(isMulti){
			if(NetworkServer.active){
				int dur = 10;
				float tick = 0.5f;
				if(!effectNames.Contains("BasicManaRegenHelper")){
					effectEndTimes.Add(Time.time+dur);
					effectNames.Add("BasicManaRegenHelper");
					effectTickL.Add(tick);
					effectPrevTimes.Add(-tick);
				}else{
					effectEndTimes[effectNames.IndexOf("BasicManaRegenHelper")] = Time.time+dur;
				}
			}else{
				CmdBasicManaRegenStart();
			}
		}else{
			int dur = 10;
			float tick = 0.5f;
			if(!effectNames.Contains("BasicManaRegenHelper")){
				effectEndTimes.Add(Time.time+dur);
				effectNames.Add("BasicManaRegenHelper");
				effectTickL.Add(tick);
				effectPrevTimes.Add(-tick);
			}else{
				effectEndTimes[effectNames.IndexOf("BasicManaRegenHelper")] = Time.time+dur;
			}
		}
	}
	[Command]
	void CmdBasicManaRegenStart(){
		int dur = 10;
		float tick = 0.5f;
		if(!effectNames.Contains("BasicManaRegenHelper")){
			effectEndTimes.Add(Time.time+dur);
			effectNames.Add("BasicManaRegenHelper");
			effectTickL.Add(tick);
			effectPrevTimes.Add(-tick);
		}else{
			effectEndTimes[effectNames.IndexOf("BasicManaRegenHelper")] = Time.time+dur;
		}
	}
	void BasicManaRegenHelper(){
		int tickManaRegen = 1;
		myPlayer.mana = Mathf.Clamp(myPlayer.mana+tickManaRegen, 0, 100);
		effectEndTimes[effectNames.IndexOf("BasicManaRegenHelper")] = Time.time + 10;
		// Debug.Log("helper");
	}
	void MagicMissile(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("MagicMissile called");
				float speed = 1;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(magicMissilePrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = wandTip.position+dir;
				fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdMagicMissile();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("MagicMissile called");
			float speed = 1;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(magicMissilePrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = wandTip.position+dir;
			fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		}

	}
	[Command]
	void CmdMagicMissile(){
		Debug.Log("CmdMagicMissile called");
		float speed = 1;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(magicMissilePrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}

	void ArcaneSphere(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("ArcaneSphere called");
				float speed = 0.5f;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(arcaneSpherePrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = wandTip.position+dir;
				fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdArcaneSphere();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("ArcaneSphere called");
			float speed = 0.5f;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(arcaneSpherePrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = wandTip.position+dir;
			fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		}

	}
	[Command]
	void CmdArcaneSphere(){
		Debug.Log("CmdArcaneSphere called");
		float speed = 0.5f;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(arcaneSpherePrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}
	void ScalingShot(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("ScalingShot called");
				float speed = 1f;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(scalingShotPrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = wandTip.position+dir;
				fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdScalingShot();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("ScalingShot called");
			float speed = 1f;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(scalingShotPrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = wandTip.position+dir;
			fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		}

	}
	[Command]
	void CmdScalingShot(){
		Debug.Log("CmdScalingShot called");
		float speed = 1f;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(scalingShotPrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}
	void TripleLock(){
		Debug.Log("TripleLock called");
	}
	void LightningBolt(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("LightningBolt called");
				float speed = 2f;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(lightningBoltPrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = wandTip.position+dir;
				fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdLightningBolt();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("LightningBolt called");
			float speed = 2f;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(lightningBoltPrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = wandTip.position+dir;
			fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		}

	}
	[Command]
	void CmdLightningBolt(){
		Debug.Log("CmdLightningBolt called");
		float speed = 2f;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(lightningBoltPrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}
	void AquaOrb(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("AquaOrb called");
				float speed = 0.75f;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(aquaOrbPrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = wandTip.position+dir;
				fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdAquaOrb();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("AquaOrb called");
			float speed = 0.75f;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(aquaOrbPrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = wandTip.position+dir;
			fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		}

	}
	[Command]
	void CmdAquaOrb(){
		Debug.Log("CmdAquaOrb called");
		float speed = 0.75f;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(aquaOrbPrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}
	void WaterHose(){
		Debug.Log("WaterHose called");
	}
	void Bubble(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("Bubble called");
				float speed = 0.5f;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(bubblePrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = wandTip.position+dir;
				fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdBubble();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("Bubble called");
			float speed = 0.5f;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(bubblePrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = wandTip.position+dir;
			fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		}

	}
	[Command]
	void CmdBubble(){
		Debug.Log("CmdBubble called");
		float speed = 0.5f;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(bubblePrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}
	void Airslice(){
		Debug.Log("Airslice called");
	}
	void BoomerangBlast(){
		Debug.Log("BoomerangBlast called");
	}
	void Typhoon(){
		Debug.Log("Typhoon called");
	}
	void StoneShot(){
		Debug.Log("StoneShot called");
	}
	void CentralizedSand(){
		Debug.Log("CentralizedSand called");
	}
	void PocketSand(){
		Debug.Log("PocketSand called");
	}
	void Fireball(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("Fireball called");
				float speed = 1f;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(fireballPrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = wandTip.position+dir;
				fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdFireball();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("Fireball called");
			float speed = 1f;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(fireballPrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = wandTip.position+dir;
			fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		}

	}
	[Command]
	void CmdFireball(){
		Debug.Log("CmdFireball called");
		float speed = 1f;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(fireballPrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
		// return fb;
	}
	void StalkingFlare(){
		Debug.Log("StalkingFlare called");
	}
	void Meteor(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("Meteor called");
				float speed = 1f;
				Vector3 dir = wandTip.position-wandHandle.position;
				GameObject fb = Instantiate(meteorPrefab) as GameObject;
				fb.transform.parent = spellsParent.transform;
				fb.transform.position = meteorSpawn.position;
				fb.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2, dir.normalized.z)*speed);
				NetworkServer.Spawn(fb);
				RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdMeteor();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("Meteor called");
			float speed = 1f;
			Vector3 dir = wandTip.position-wandHandle.position;
			GameObject fb = Instantiate(meteorPrefab) as GameObject;
			fb.transform.parent = spellsParent.transform;
			fb.transform.position = meteorSpawn.position;
			fb.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2, dir.normalized.z)*speed);
		}

	}
	[Command]
	void CmdMeteor(){
		Debug.Log("CmdMeteor called");
		float speed = 1f;
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(meteorPrefab) as GameObject;
		fb.transform.parent = spellsParent.transform;
		fb.transform.position = meteorSpawn.position;
		fb.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2, dir.normalized.z)*speed);
		NetworkServer.Spawn(fb);
		RpcParentTo(fb.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
		// return fb;
	}
	void I_Convert(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("I_Convert called");
				GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
				if(minions.Length>0){
					int i=0;
					bool converted = false;
					while(i<minions.Length && !converted){
						if(minions[i].transform.root!=transform.root){
							converted = true;
							minions[i].transform.parent = spellsParent;
							minions[i].GetComponent<MinionScript>().Convert();
							RpcParentTo(minions[i].GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
						}
						i++;
					}
				}
			}else{
				CmdConvert();
			}
		}else{
			Debug.Log("I_Convert called");
			GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
			if(minions.Length>0){
				int i=0;
				bool converted = false;
				while(i<minions.Length && !converted){
					if(minions[i].transform.root!=transform.root){
						converted = true;
						minions[i].transform.parent = spellsParent;
						minions[i].GetComponent<MinionScript>().Convert();
					}
					i++;
				}
			}
		}
	}

	[Command]
	void CmdConvert(){
		Debug.Log("CmdConvert called");
		GameObject[] minions = GameObject.FindGameObjectsWithTag("Minion");
		if(minions.Length>0){
			int i=0;
			bool converted = false;
			while(i<minions.Length && !converted){
				if(minions[i].transform.root!=transform.root){
					converted = true;
					minions[i].transform.parent = spellsParent;
					minions[i].GetComponent<MinionScript>().Convert();
					RpcParentTo(minions[i].GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
				}
				i++;
			}
		}
	}
	void ShockingBlast(){
		Debug.Log("ShockingBlast called");
	}
	void Caltrops(){
		Debug.Log("Caltrops called");
	}
	void TidalWave(){
		Debug.Log("TidalWave called");
	}
	void Tailwind(){
		Debug.Log("Tailwind called");
	}
	void Fissure(){
		Debug.Log("Fissure called");
	}
	void FlameCarpet(){
		Debug.Log("FlameCarpet called");
	}
	void I_Heal(){
		//20 hp healed over 5 seconds
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("Heal called");
				int dur = 5;
				float tick = 0.25f;
				GameObject hep = Instantiate(healEffectPrefab) as GameObject;
				hep.transform.parent = transform;
				hep.transform.localPosition = new Vector3(0,0,0);
				NetworkServer.Spawn(hep);
				if(!effectNames.Contains("HealHelper")){
					effectEndTimes.Add(Time.time+dur);
					effectNames.Add("HealHelper");
					effectTickL.Add(tick);
					effectPrevTimes.Add(-tick);
				}else{
					effectEndTimes[effectNames.IndexOf("HealHelper")] = Time.time+dur;
				}
			}else{
				CmdHeal();
			}
		}else{
			Debug.Log("Heal called");
			int dur = 5;
			float tick = 0.25f;
			GameObject hep = Instantiate(healEffectPrefab) as GameObject;
			hep.transform.parent = spellsParent.transform;
			hep.transform.localPosition = new Vector3(0,0,0);
			NetworkServer.Spawn(hep);
			if(!effectNames.Contains("HealHelper")){
				effectEndTimes.Add(Time.time+dur);
				effectNames.Add("HealHelper");
				effectTickL.Add(tick);
				effectPrevTimes.Add(-tick);
			}else{
				effectEndTimes[effectNames.IndexOf("HealHelper")] = Time.time+dur;
			}
		}
	}
	[Command]
	void CmdHeal(){
		Debug.Log("CmdHeal called");
		int dur = 5;
		float tick = 0.25f;
		GameObject hep = Instantiate(healEffectPrefab) as GameObject;
		hep.transform.parent = transform;
		hep.transform.localPosition = new Vector3(0,0,0);
		NetworkServer.Spawn(hep);
		if(!effectNames.Contains("HealHelper")){
			effectEndTimes.Add(Time.time+dur);
			effectNames.Add("HealHelper");
			effectTickL.Add(tick);
			effectPrevTimes.Add(-tick);
		}else{
			effectEndTimes[effectNames.IndexOf("HealHelper")] = Time.time+dur;
		}
	}
	void HealHelper(){
		int tickHeal = 1;
		myPlayer.HP = Mathf.Clamp(myPlayer.HP+tickHeal, 0, 100);
		Debug.Log("Heal helper called");
	}
	void I_Conversion(){
		//20 hp healed over 5 seconds
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("Conversion called");
				int dur = 10;
				float tick = 1f;
				GameObject hep = Instantiate(conversionEffectPrefab) as GameObject;
				hep.transform.parent = spellsParent.transform;
				hep.transform.localPosition = new Vector3(0,0,0);
				NetworkServer.Spawn(hep);
				if(!effectNames.Contains("ConversionHelper")){
					effectEndTimes.Add(Time.time+dur);
					effectNames.Add("ConversionHelper");
					effectTickL.Add(tick);
					effectPrevTimes.Add(-tick);
				}else{
					effectEndTimes[effectNames.IndexOf("ConversionHelper")] = Time.time+dur;
				}
			}else{
				CmdConversion();
			}
		}else{
			Debug.Log("Conversion called");
			int dur = 10;
			float tick = 1f;
			GameObject hep = Instantiate(conversionEffectPrefab) as GameObject;
			hep.transform.parent = spellsParent.transform;
			hep.transform.localPosition = new Vector3(0,0,0);
			NetworkServer.Spawn(hep);
			if(!effectNames.Contains("ConversionHelper")){
				effectEndTimes.Add(Time.time+dur);
				effectNames.Add("ConversionHelper");
				effectTickL.Add(tick);
				effectPrevTimes.Add(-tick);
			}else{
				effectEndTimes[effectNames.IndexOf("ConversionHelper")] = Time.time+dur;
			}
		}
	}
	[Command]
	void CmdConversion(){
		Debug.Log("CmdConversion called");
		int dur = 10;
		float tick = 1f;
		GameObject hep = Instantiate(conversionEffectPrefab) as GameObject;
		hep.transform.parent = spellsParent.transform;
		hep.transform.localPosition = new Vector3(0,0,0);
		NetworkServer.Spawn(hep);
		if(!effectNames.Contains("ConversionHelper")){
			effectEndTimes.Add(Time.time+dur);
			effectNames.Add("ConversionHelper");
			effectTickL.Add(tick);
			effectPrevTimes.Add(-tick);
		}else{
			effectEndTimes[effectNames.IndexOf("ConversionHelper")] = Time.time+dur;
		}
	}
	void ConversionHelper(){
		Debug.Log("Conversion helper called");
		int tickDamage = 5;
		int tickMana = 5;
		if(myPlayer.HP>tickDamage){
			myPlayer.HP = Mathf.Clamp(myPlayer.HP-tickDamage, 0, 100);
			myPlayer.mana = Mathf.Clamp(myPlayer.mana + tickMana, 0, 100);
		}
	}
	void Blind(){
		Debug.Log("Blind called");
	}
	void Invisible(){
		Debug.Log("Invisible called");
	}
	void Taunt(){
		Debug.Log("Taunt called");
	}
	void Weaken(){
		Debug.Log("Weaken called");
	}
	void LifeTie(){
		Debug.Log("LifeTie called");
	}
	void Purge(){
		Debug.Log("Purge called");
	}
	void Immobilize(){
		Debug.Log("Immobilize called");
	}
	void Return(){
		Debug.Log("Return called");
	}
	void Counterspell(){
		Debug.Log("Counterspell called");
	}
	void Helmets(){
		Debug.Log("Helmets called");
	}
	void I_Shields(){
		Transform posSlot = GetComponent<Duel_PlayerScript>().posSlot.transform;
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("Shield called");
				GameObject s = Instantiate(shieldPrefab) as GameObject;
				s.transform.parent = spellsParent;
				s.transform.position = posSlot.position-posSlot.right+posSlot.up;
				NetworkServer.Spawn(s);
				RpcParentTo(s.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdI_Shields();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("Shield called");
			GameObject s = Instantiate(shieldPrefab) as GameObject;
			s.transform.parent = spellsParent;
			s.transform.position = posSlot.position-posSlot.right+posSlot.up;
		}
	}
	[Command]
	void CmdI_Shields(){
		Debug.Log("CmdShield called");
		Transform posSlot = GetComponent<Duel_PlayerScript>().posSlot.transform;
		GameObject s = Instantiate(shieldPrefab) as GameObject;
		s.transform.parent = spellsParent;
		s.transform.position = posSlot.position-posSlot.right+posSlot.up;
		NetworkServer.Spawn(s);
		RpcParentTo(s.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}
	void ControlledFlow(){
		Debug.Log("ControlledFlow called");
	}
	void AssistingAir(){
		Debug.Log("AssistingAir called");
	}
	void RoughSkin(){
		Debug.Log("RoughSkin called");
	}
	void ExplodingGuard(){
		Debug.Log("ExplodingGuard called");
	}

	void I_SpawnMinion(){
		if(isMulti){
			if(NetworkServer.active){
				Debug.Log("spawn minion called");
				GameObject m = Instantiate(minionPrefab) as GameObject;
				m.transform.position = myMinionSpawn.position;
				m.transform.parent = spellsParent;
				NetworkServer.Spawn(m);
				RpcParentTo(m.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
			}else{
				CmdI_SpawnMinion();
				// g.transform.parent = transform.root.Find("SpellsParent");
			}
		}else{
			Debug.Log("spawn minion called");
			GameObject minion = Instantiate(minionPrefab) as GameObject;
			minion.transform.position = myMinionSpawn.position;
			minion.transform.parent = spellsParent;
		}
	}
	[Command]
	void CmdI_SpawnMinion(){
		Debug.Log("CmdSpawnMinion called");
		GameObject m = Instantiate(minionPrefab) as GameObject;
		m.transform.parent = spellsParent;
		m.transform.position = myMinionSpawn.position;
		NetworkServer.Spawn(m);
		RpcParentTo(m.GetComponent<NetworkIdentity>().netId, spellsParent.GetComponent<NetworkIdentity>().netId);
	}
	void I_WeatherClear(){
		Debug.Log("clear");
		// playerSkybox.material = clearSkiesSkybox;
		RenderSettings.skybox = clearSkiesSkybox;
		weather = 0;
		if(isMulti){
			if(NetworkServer.active){
				RpcI_WeatherClear();
			}else{
				CmdI_WeatherClear();
			}
		}
	}
	[ClientRpc]
	void RpcI_WeatherClear(){
		Debug.Log("rpcclear");
		RenderSettings.skybox = clearSkiesSkybox;
		weather = 0;
	}
	[Command]
	void CmdI_WeatherClear(){
		Debug.Log("cmdclear");
		RenderSettings.skybox = clearSkiesSkybox;
		weather = 0;
	}
	void I_WeatherFire(){
		// playerSkybox.material = clearSkiesSkybox;
		RenderSettings.skybox = fireSkybox;
		weather = 1;
		if(isMulti){
			if(NetworkServer.active){
				RpcI_WeatherFire();
			}else{
				CmdI_WeatherFire();
			}
		}
	}
	[ClientRpc]
	void RpcI_WeatherFire(){
		RenderSettings.skybox = fireSkybox;
		weather = 1;
	}
	[Command]
	void CmdI_WeatherFire(){
		RenderSettings.skybox = fireSkybox;
		weather = 1;
	}
void I_WeatherWater(){
	// playerSkybox.material = clearSkiesSkybox;
	RenderSettings.skybox = rainSkybox;
	weather = 2;
	if(isMulti){
		if(NetworkServer.active){
			RpcI_WeatherWater();
		}else{
			CmdI_WeatherWater();
		}
	}
}
[ClientRpc]
void RpcI_WeatherWater(){
	RenderSettings.skybox = rainSkybox;
	weather = 2;
}
[Command]
void CmdI_WeatherWater(){
	RenderSettings.skybox = rainSkybox;
	weather = 2;
}
void I_WeatherSand(){
	// playerSkybox.material = clearSkiesSkybox;
	RenderSettings.skybox = sandSkybox;
	weather = 3;
	if(isMulti){
		if(NetworkServer.active){
			RpcI_WeatherSand();
		}else{
			CmdI_WeatherSand();
		}
	}
}
[ClientRpc]
void RpcI_WeatherSand(){
	RenderSettings.skybox = sandSkybox;
	weather = 3;
}
[Command]
void CmdI_WeatherSand(){
	RenderSettings.skybox = sandSkybox;
	weather = 3;
}
void I_WeatherWind(){
	// playerSkybox.material = clearSkiesSkybox;
	RenderSettings.skybox = windSkybox;
	weather = 4;
	if(isMulti){
		if(NetworkServer.active){
			RpcI_WeatherWind();
		}else{
			CmdI_WeatherWind();
		}
	}
}
[ClientRpc]
void RpcI_WeatherWind(){
	RenderSettings.skybox = windSkybox;
	weather = 4;
}
[Command]
void CmdI_WeatherWind(){
	RenderSettings.skybox = windSkybox;
	weather = 4;
}
	void I_Confetti(){
		Debug.Log("confetti");
		GameObject c = Instantiate(confettiPrefab) as GameObject;
		c.transform.position = wandTip.position+new Vector3(0,1,0);
	}
	public void TryBeginAiming(){
		if(cuedSpellNum!=-1){
			aiming = true;
			aimLine.SetActive(true);
		}
	}
	public bool isAiming(){
		return aiming;
	}
}
