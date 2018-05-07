using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomTutorial : MonoBehaviour {

  public GameObject broom;

	private BroomScript brms;
	private TextMesh txtMsh;
  public int stage = 0;
	private float storedTime;
	private bool done;
	//steps
	//mount
	//press the trigger to move forward
	//turn your head left or right to turn in that direction
	//move your head forward or backward to turn down or up
	//press the A button to stop
	//press the B button to level out
	//have fun
	//gone

	// Use this for initialization
	void Start () {
		txtMsh = gameObject.GetComponent<TextMesh>();
		brms = broom.GetComponent<BroomScript>();
	}

	// Update is called once per frame
	void Update () {
		if(stage < 6)
		{
			stage = brms.tutLvl;
		}
		if (!done)
		{
			switch (stage)
			{
				case 1: //mount -> trigger
					txtMsh.text = "Press the trigger\nto move forward";
					break;
				case 2: ///trigger -> turn
					txtMsh.text = "Turn your head left or right\nto turn in that direction";
					break;
				case 3: //turn -> move
					txtMsh.text = "Move your head forward or\nbackward to turn down or up";
					break;
				case 4: //move -> A
					txtMsh.text = "Press and hold the A button to stop";
					break;
				case 5: //A -> B
					txtMsh.text = "Press the B button\nto level out";
					storedTime = Time.time;
					break;
				case 6: //B -> fun
					txtMsh.text = "Enjoy your ride!";
					if(Time.time - storedTime >= 3f)
					{
						stage = 7;
					}
					break;
				case 7: //fun -> none
					txtMsh.text = "";
					done = true;
					break;
				default:
					break;
			}
		}
	}
}
