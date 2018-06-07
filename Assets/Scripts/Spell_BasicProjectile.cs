using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_BasicProjectile : SpellGeneric {
		public int empoweredDamage = 20;
		public int myType = 0;
		public int damage = 10;
		public GameObject deathPS1;
		public GameObject deathPS2;
		public GameObject deathPS3;
		public GameObject deathPS4;
		public GameObject deathSound;
		public override void Trigger(Vector3 v, Duel_PlayerScript p, SpellManagerScript s){
			if(myType!=0){ //arcane spells don't change based on weather
				if(GameObject.FindWithTag("Player").GetComponent<SpellManagerScript>().weather==myType){
					damage = empoweredDamage;
				}
			}
			if(deathPS1!=null){
				deathPS1.transform.parent = null;
				deathPS1.GetComponent<ParticleSystem>().Play();
			}
			if(deathPS2!=null){
				deathPS2.transform.parent = null;
				deathPS2.GetComponent<ParticleSystem>().Play();
			}
			if(deathPS3!=null){
				deathPS3.transform.parent = null;
				deathPS3.GetComponent<ParticleSystem>().Play();
			}
			if(deathPS4!=null){
				deathPS4.transform.parent = null;
				deathPS4.GetComponent<ParticleSystem>().Play();
			}
			if(deathSound!=null){
				deathSound.transform.parent = null;
				deathSound.GetComponent<AudioSource>().Play();
			}
			Debug.Log("triggered");
			p.tryHit(v.z, damage);
			gameObject.SetActive(false);
		}

}
