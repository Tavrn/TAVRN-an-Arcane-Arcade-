using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_BasicProjectile : SpellGeneric {
		public int damage = 10;
		public GameObject deathPS1;
		public GameObject deathPS2;
		public GameObject deathPS3;
		public GameObject deathPS4;
		public override void Trigger(Vector3 v, Duel_PlayerScript p, SpellManagerScript s){
			p.tryHit(v.z, damage);
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
			Destroy(gameObject);
		}

}
