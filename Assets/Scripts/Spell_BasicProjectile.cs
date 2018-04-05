using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_BasicProjectile : SpellGeneric {
		public int damage = 10;
		public GameObject deathPS;
		public override void Trigger(Vector3 v, Duel_PlayerScript p, SpellManagerScript s){
			p.tryHit(v.z, damage);
			if(deathPS!=null){
				deathPS.transform.parent = null;
				deathPS.GetComponent<ParticleSystem>().Play();
			}
			Destroy(gameObject);
		}

}
