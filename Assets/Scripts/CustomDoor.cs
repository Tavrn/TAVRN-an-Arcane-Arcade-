using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomDoor : MonoBehaviour {
	public int sceneCounter = 0;
	public string ToScene0;
	public string ToScene1;

	public void Open(){
		if(sceneCounter==0){
			// Debug.Log("to scene " + ToScene0);
			SceneManager.LoadScene(ToScene0);
		}else{
			// Debug.Log("to scene " + ToScene1);
			SceneManager.LoadScene(ToScene1);
		}
	}
	public void Toggle(){
		sceneCounter = (sceneCounter+1)%2;
	}
}
