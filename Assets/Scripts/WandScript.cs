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
	public GameObject myTargetSlot;
	private bool isChanneling = false;
	private float pz = 1/3f;

	void Start () {
		pattern = new List<Coordinate>();
		originCollider = GameObject.Find("Colliders").GetComponent<CollidersScript>().originCollider;
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
		if(spellManager.isAiming()){
			bool didhit = false;
			RaycastHit[] hits;
			hits = Physics.RaycastAll(transform.position, transform.forward, 50, 1 << LayerMask.NameToLayer("TargetRaycast"));
			for(int i=0; i<hits.Length; i++){
				RaycastHit hit = hits[i];
				if(hit.transform.root!=transform.root){
					if(hit.point.z>pz){
						didhit = true;
						myTargetSlot.transform.position = new Vector3(myTargetSlot.transform.position.x, myTargetSlot.transform.position.y, 2/3f);
					}else if(hit.point.z<pz && hit.point.z>-pz){
						didhit = true;
						myTargetSlot.transform.position = new Vector3(myTargetSlot.transform.position.x, myTargetSlot.transform.position.y, 0f);
					}else if(hit.point.z<-pz){
						didhit = true;
						myTargetSlot.transform.position = new Vector3(myTargetSlot.transform.position.x, myTargetSlot.transform.position.y, -2/3f);
					}
				}
			}
			myTargetSlot.SetActive(didhit);
		}else{
			myTargetSlot.SetActive(false); //not aiming
		}
	}
}
