using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomDoor : MonoBehaviour {
	public int sceneCounter = 0;
	public ParticleSystem single_PS;
	public ParticleSystem multi_PS;
	public GameObject single_txt;
	public GameObject multi_txt;
	public GameObject mpp;
	public string ToScene0;
	public string ToScene1;

	public void Open(){
		if(sceneCounter==0){
			// Debug.Log("to scene " + ToScene0);
			SceneManager.LoadScene(ToScene0);
		}else{
			// Debug.Log("to scene " + ToScene1);
			AutoNetMM anMM = GameObject.Find("AutoNetMM").GetComponent<AutoNetMM>();
			anMM.gamePlayerPrefab = mpp;
			anMM.playScene = ToScene1;
			anMM.MMStart();
			// SceneManager.LoadScene(ToScene1);
		}
	}
	public void Toggle(){
		sceneCounter = (sceneCounter+1)%2;
		if(sceneCounter == 0)
		{
			multi_PS.Stop();
			multi_PS.Clear();
			single_PS.Play();
			single_txt.SetActive(true);
			multi_txt.SetActive(false);

		}
		else
		{
			single_PS.Stop();
			single_PS.Clear();
			multi_PS.Play();
			multi_txt.SetActive(true);
			single_txt.SetActive(false);
		}

	}
}
