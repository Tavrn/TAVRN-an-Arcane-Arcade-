using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CNamespace;
namespace CNamespace{
	public struct Coordinate{
		int x, y, z;
		public Coordinate(int x, int y, int z){
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public int getX(){
			return x;
		}
		public int getY(){
			return y;
		}
		public int getZ(){
			return z;
		}
	}
}
public class WandScript : MonoBehaviour {
	public SpellManagerScript spellManager;
	public List<Coordinate> pattern;
	public List<GameObject> patternColliders;
	public GameObject originCollider;
	private bool isChanneling = false;

	void Start () {
		pattern = new List<Coordinate>();
	}
	public void setChanneling(bool b){
		isChanneling = b;
		if(b){
			pattern.Clear();
			originCollider.GetComponent<MeshRenderer>().enabled = true;
			patternColliders.Add(originCollider);
		}else{
			// for(int i=0; i<pattern.Count; i++){
			// 	// Debug.Log(pattern[i].getX() + " " + pattern[i].getY() + " " + pattern[i].getZ());
			// }
			spellManager.checkForSpell();
			for(int i=0; i<patternColliders.Count; i++){
				patternColliders[i].GetComponent<MeshRenderer>().enabled=false;
			}
		}
	}

	void OnTriggerEnter(Collider c){
		if(isChanneling){
			GameObject g = c.gameObject;
			if(g.tag=="WandCollider"){
				g.GetComponent<MeshRenderer>().enabled = true;
				patternColliders.Add(g);
				ColliderScript s = g.GetComponent<ColliderScript>();
				pattern.Add(new Coordinate(s.x, s.y, s.z));
			}
		}
	}
	public void FireSpell(){
		spellManager.FireSpell();
	}
	void Update () {

	}
}
