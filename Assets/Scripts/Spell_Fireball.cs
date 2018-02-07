using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Fireball : SpellGeneric {
	int damage = 10;
	void Start () {

	}

	void Update () {

	}
	public override void Trigger(Vector3 v, Duel_PlayerScript p, SpellManagerScript s){
		Debug.Log("fireball triggered with " + v.z);
		p.tryHit(v.z, damage);
	}
}
