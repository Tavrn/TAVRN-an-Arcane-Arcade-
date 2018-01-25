﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CNamespace;
public class SpellManagerScript : MonoBehaviour {
	public Transform wandTip;
	public Transform wandHandle;
	public WandScript wandScript;
	[Space(10)]
	public Transform minionSpawn1;
	public Transform minionSpawn2;
	public GameObject minionPrefab;
	[Space(10)]
	public Skybox playerSkybox;
	public Material clearSkiesSkybox;
	public Material fireSkybox;
	[Space(10)]
	public GameObject fireballPrefab;
	public GameObject confettiPrefab;
	public GameObject arcanePrefab;
	private int cuedSpellNum = -1;
	List<List<Coordinate>> spellCompendium = new List<List<Coordinate>>();
	List<string> spellNameCompendium = new List<string>();

	void Start () {
		SetUpSpells();
	}
	void SetUpSpells(){
		CreateSpell("MagicMissile", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(-1,1,1) });
		CreateSpell("ArcaneSphere", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0)});
		CreateSpell("Rekka", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0), new Coordinate(0,0,0) });
		CreateSpell("TripleLock", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0), new Coordinate(1,1,0), new Coordinate(2, 1, 0), new Coordinate(2, 2, 0) });
		CreateSpell("LightningBolt", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1,1,0),new Coordinate(0,1,0), new Coordinate(0,2,0), new Coordinate(1, 2, 0), new Coordinate(1, 3, 0) });
		CreateSpell("AquaOrb", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, -1, 1) });
		CreateSpell("WaterHose", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, 0, 0), new Coordinate(-1, 0, 1), new Coordinate(0, 0, 1) });
		CreateSpell("Bubble", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, 0, 0), new Coordinate(-1, 0, 1), new Coordinate(0, 0, 1), new Coordinate(0, 1, 1) });
		CreateSpell("Airslice", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1) });
		CreateSpell("BoomerangBlast", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(1, 0, 1), new Coordinate(0, 0, 1) });
		CreateSpell("Typhoon", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(0, 1, 1), new Coordinate(0, 2, 1), new Coordinate(-1, 2, 1) });
		CreateSpell("StoneShot", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1 ,1) });
		CreateSpell("CentralizedSand", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1,1,0), new Coordinate(2, 1, 0), new Coordinate(2, 0, 0) });
		CreateSpell("PocketSand", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1,1,0), new Coordinate(1, 1, 1), new Coordinate(2, 1, 0), new Coordinate(2, 0, 0) });
		CreateSpell("Fireball", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1) });
		CreateSpell("StalkingFlare", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-2, 1, 1), new Coordinate(-3, 1, 1) });
		CreateSpell("Meteor", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-3, 1, 1), new Coordinate(-4, 1, 1) });
		CreateSpell("Convert", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0), new Coordinate(-1, 2, 0) });
		CreateSpell("ShockingBlast", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 2, 0), new Coordinate(-1, 2, 1) });
		CreateSpell("Caltrops", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1) });
		CreateSpell("TidalWave", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-2, -1, 0), new Coordinate(-2, -1, 1) });
		CreateSpell("Tailwind", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(0,1,0), new Coordinate(0, 1, 1) });
		CreateSpell("Fissure", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(0, 1, 1) });
		CreateSpell("FlameCarpet", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0,1,0), new Coordinate(-1, 1, 0), new Coordinate(-1, 0, 0), new Coordinate(-1, 0, 1) });
		CreateSpell("Heal", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-1,2,1) });
		CreateSpell("Conversion", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(0, 1, 1) });
		CreateSpell("Blind", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0) });
		CreateSpell("Invisible", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 1, 1) });
		CreateSpell("Taunt", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-3, 1, 1) });
		CreateSpell("Weaken", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-2, 0, 1) });
		CreateSpell("LifeTie", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 1, 1), new Coordinate(0, 1, 2) });
		CreateSpell("Purge", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-2, 1, 1), new Coordinate(-2, 0, 1) });
		CreateSpell("Immobilize", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 1, 1), new Coordinate(-1, 1, 1) });
		CreateSpell("Return", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0), new Coordinate(0, 2, 1), new Coordinate(1, 2, 1) });
		CreateSpell("Counterspell", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 1, 1), new Coordinate(-1, 2, 1), new Coordinate(-1, 2, 2), new Coordinate(-1, 1, 2) });
		CreateSpell("Helmets", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0), new Coordinate(-2, 2, 0), new Coordinate(-1, 2, 0), new Coordinate(-1,1,0) });
		CreateSpell("Shields", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(-1, 0, 0), new Coordinate(-1, 1, 0), new Coordinate(0, 1, 0), new Coordinate(0, 2, 0), new Coordinate(-1, 2, 0), new Coordinate(-1, 1, 0) });
		CreateSpell("ControlledFlow", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, -1, 0), new Coordinate(-1, -1, 0), new Coordinate(-1, -2, 0), new Coordinate(-2, -2, 0) });
		CreateSpell("AssistingAir", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(1, 0, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(1, 1, 2) });
		CreateSpell("RoughSkin", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(1, 1, 0), new Coordinate(1, 1, 1), new Coordinate(1, 0, 1) });
		CreateSpell("ExplodingGuard", new Coordinate[] { new Coordinate(0,0,0), new Coordinate(0, 1, 0), new Coordinate(-1, 1, 0), new Coordinate(-1, 0, 0), new Coordinate(0,0,0) });

		CreateSpell("I_SpawnMinion", new Coordinate[] {new Coordinate(0,0,0), new Coordinate(0,0,1), new Coordinate(0,0,2)});
		CreateSpell("I_WeatherClear", new Coordinate[] {new Coordinate(0,0,0), new Coordinate(1,0,0),new Coordinate(1, 1, 0), new Coordinate(2, 1, 0)});
		CreateSpell("I_WeatherFire", new Coordinate[] {new Coordinate(0,0,0), new Coordinate(-1,0,0),new Coordinate(-1, 1, 0), new Coordinate(-2, 1, 0)});
		CreateSpell("I_Confetti", new Coordinate[] {new Coordinate(0,0,0), new Coordinate(0,-1,0), new Coordinate(0,-2,0), new Coordinate(0,-2,-1), new Coordinate(0,-2,-2)});

	}
	void CreateSpell(string name, Coordinate[] coordinates){
		List<Coordinate> pat = new List<Coordinate>();
		for(int i=0; i<coordinates.Length; i++){
			pat.Add(coordinates[i]);
		}
		spellNameCompendium.Add(name);
		spellCompendium.Add(pat);
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
				GetComponent<AudioSource>().Play();
				string temp = spellNameCompendium[spellNum];
				if(temp[0] == 'I' && temp[1] == '_'){ //check if spell is instantly cast or not
					Invoke(spellNameCompendium[spellNum], 0f);
					cuedSpellNum = -1;
				}else{
					cuedSpellNum = spellNum;
				}
			}
		}
		wandScript.pattern.Clear();
	}
	bool compareSpells(List<Coordinate> spell1, List<Coordinate> spell2){
		// Debug.Log("Counts: " + spell1.Count + " " + spell2.Count);
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
		// Debug.Log(a.getX() + " " + b.getX() + " " + a.getY() + " " + b.getY() + " " + a.getZ() + " " + b.getZ());
		return a.getX()==b.getX() && a.getY() == b.getY() && a.getZ() == b.getZ();
	}
	public void FireSpell(){
		if(cuedSpellNum!=-1){
			Invoke(spellNameCompendium[cuedSpellNum], 0f);
			cuedSpellNum = -1;
		}
	}


	void MagicMissile(){
		Debug.Log("called magic missile");
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(arcanePrefab) as GameObject;
		fb.transform.position = wandTip.position+dir;
		fb.transform.LookAt(new Vector3(0,transform.position.y, 0));
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*5);
	}

	void ArcaneSphere(){
		Debug.Log("ArcaneSphere called");
	}
	void Rekka(){
		Debug.Log("Rekka called");
	}
	void TripleLock(){
		Debug.Log("TripleLock called");
	}
	void LightningBolt(){
		Debug.Log("LightningBolt called");
	}
	void AquaOrb(){
		Debug.Log("AquaOrb called");
	}
	void WaterHose(){
		Debug.Log("WaterHose called");
	}
	void Bubble(){
		Debug.Log("Bubble called");
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
		Debug.Log("Fireball called");
		Vector3 dir = wandTip.position-wandHandle.position;
		GameObject fb = Instantiate(fireballPrefab) as GameObject;
		fb.transform.position = wandTip.position+dir;
		fb.GetComponent<Rigidbody>().AddForce(dir.normalized*5);
	}
	void StalkingFlare(){
		Debug.Log("StalkingFlare called");
	}
	void Meteor(){
		Debug.Log("Meteor called");
	}
	void Convert(){
		Debug.Log("Convert called");
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
	void Heal(){
		Debug.Log("Heal called");
	}
	void Conversion(){
		Debug.Log("Conversion called");
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
	void Shields(){
		Debug.Log("Shields called");
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
		Debug.Log("spawn minion called");
		GameObject minion = Instantiate(minionPrefab) as GameObject;
		minion.transform.position = minionSpawn1.position;
	}
	void I_WeatherClear(){
		playerSkybox.material = clearSkiesSkybox;
	}
	void I_WeatherFire(){
		playerSkybox.material = fireSkybox;
	}
	void I_Confetti(){
		Debug.Log("confetti");
		GameObject c = Instantiate(confettiPrefab) as GameObject;
		c.transform.position = wandTip.position+new Vector3(0,1,0);
	}
}
