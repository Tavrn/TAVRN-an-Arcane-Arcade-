namespace VRTK.Examples
{
    using UnityEngine;

    public class Tavern_EventListener : MonoBehaviour
    {
        public GameObject dsBook;
				public BoxCollider door1C;
				public BoxCollider door2C;
				public BoxCollider d1SC;
				public BoxCollider d2SC;

        public static bool idleMulti;

        private float baseVolumeHeight=0f;
        private Transform plyr;
        private bool hasFlip = false;

        private void Start()
        {

            idleMulti = false;

            plyr = transform.parent.parent.Find("NewtonSDK/NVRPlayer");
//            Debug.Log("ayy " + gameObject + " " + plyr + " " + plyr.Find("LeftHand").childCount);
//            transform.parent.Find("Render Model for RightHand").GetComponent<SphereCollider>().radius = 0.7f;

            if (GetComponent<VRTK_ControllerEvents>() == null)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_ControllerEvents_ListenerExample", "VRTK_ControllerEvents", "the same"));
                return;
            }

            //Setup controller event listeners
            GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
            GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);

            GetComponent<VRTK_ControllerEvents>().TriggerTouchStart += new ControllerInteractionEventHandler(DoTriggerTouchStart);
            GetComponent<VRTK_ControllerEvents>().TriggerTouchEnd += new ControllerInteractionEventHandler(DoTriggerTouchEnd);

            GetComponent<VRTK_ControllerEvents>().TriggerHairlineStart += new ControllerInteractionEventHandler(DoTriggerHairlineStart);
            GetComponent<VRTK_ControllerEvents>().TriggerHairlineEnd += new ControllerInteractionEventHandler(DoTriggerHairlineEnd);

            GetComponent<VRTK_ControllerEvents>().TriggerClicked += new ControllerInteractionEventHandler(DoTriggerClicked);
            GetComponent<VRTK_ControllerEvents>().TriggerUnclicked += new ControllerInteractionEventHandler(DoTriggerUnclicked);

            GetComponent<VRTK_ControllerEvents>().TriggerAxisChanged += new ControllerInteractionEventHandler(DoTriggerAxisChanged);

            GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(DoGripPressed);
            GetComponent<VRTK_ControllerEvents>().GripReleased += new ControllerInteractionEventHandler(DoGripReleased);

            GetComponent<VRTK_ControllerEvents>().GripTouchStart += new ControllerInteractionEventHandler(DoGripTouchStart);
            GetComponent<VRTK_ControllerEvents>().GripTouchEnd += new ControllerInteractionEventHandler(DoGripTouchEnd);

            GetComponent<VRTK_ControllerEvents>().GripHairlineStart += new ControllerInteractionEventHandler(DoGripHairlineStart);
            GetComponent<VRTK_ControllerEvents>().GripHairlineEnd += new ControllerInteractionEventHandler(DoGripHairlineEnd);

            GetComponent<VRTK_ControllerEvents>().GripClicked += new ControllerInteractionEventHandler(DoGripClicked);
            GetComponent<VRTK_ControllerEvents>().GripUnclicked += new ControllerInteractionEventHandler(DoGripUnclicked);

            GetComponent<VRTK_ControllerEvents>().GripAxisChanged += new ControllerInteractionEventHandler(DoGripAxisChanged);

            GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
            GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);

            GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart += new ControllerInteractionEventHandler(DoTouchpadTouchStart);
            GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += new ControllerInteractionEventHandler(DoTouchpadTouchEnd);

            GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);

            GetComponent<VRTK_ControllerEvents>().ButtonOnePressed += new ControllerInteractionEventHandler(DoButtonOnePressed);
            GetComponent<VRTK_ControllerEvents>().ButtonOneReleased += new ControllerInteractionEventHandler(DoButtonOneReleased);

            GetComponent<VRTK_ControllerEvents>().ButtonOneTouchStart += new ControllerInteractionEventHandler(DoButtonOneTouchStart);
            GetComponent<VRTK_ControllerEvents>().ButtonOneTouchEnd += new ControllerInteractionEventHandler(DoButtonOneTouchEnd);

            GetComponent<VRTK_ControllerEvents>().ButtonTwoPressed += new ControllerInteractionEventHandler(DoButtonTwoPressed);
            GetComponent<VRTK_ControllerEvents>().ButtonTwoReleased += new ControllerInteractionEventHandler(DoButtonTwoReleased);

            GetComponent<VRTK_ControllerEvents>().ButtonTwoTouchStart += new ControllerInteractionEventHandler(DoButtonTwoTouchStart);
            GetComponent<VRTK_ControllerEvents>().ButtonTwoTouchEnd += new ControllerInteractionEventHandler(DoButtonTwoTouchEnd);

            GetComponent<VRTK_ControllerEvents>().StartMenuPressed += new ControllerInteractionEventHandler(DoStartMenuPressed);
            GetComponent<VRTK_ControllerEvents>().StartMenuReleased += new ControllerInteractionEventHandler(DoStartMenuReleased);

            GetComponent<VRTK_ControllerEvents>().ControllerEnabled += new ControllerInteractionEventHandler(DoControllerEnabled);
            GetComponent<VRTK_ControllerEvents>().ControllerDisabled += new ControllerInteractionEventHandler(DoControllerDisabled);

            GetComponent<VRTK_ControllerEvents>().ControllerIndexChanged += new ControllerInteractionEventHandler(DoControllerIndexChanged);
        }

        private void DebugLogger(uint index, string button, string action, ControllerInteractionEventArgs e)
        {
//            VRTK_Logger.Info("Controller on index '" + index + "' " + button + " has been " + action
//                    + " with a pressure of " + e.buttonPressure + " / trackpad axis at: " + e.touchpadAxis + " (" + e.touchpadAngle + " degrees)");
        }

        private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "pressed", e);
        }

        private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "released", e);
        }

        private void DoTriggerTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "touched", e);
        }

        private void DoTriggerTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "untouched", e);
        }

        private void DoTriggerHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "hairline start", e);
        }

        private void DoTriggerHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "hairline end", e);
        }

        private void DoTriggerClicked(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "clicked", e);
        }

        private void DoTriggerUnclicked(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "unclicked", e);
        }

        private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TRIGGER", "axis changed", e);
        }

        private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "pressed", e);
            if(!idleMulti)
            {
						  if(door1C.bounds.Contains(transform.position)){
							  door1C.transform.parent.GetComponent<CustomDoor>().Open();
                door1C.transform.parent.GetComponent<CustomDoor>().multi_txt.GetComponent<TextMesh>().text = "Waiting \n for \n opponent";
                // door2C.GetComponent<CustomDoor>().multi_txt.GetComponent<TextMesh>().text = "Waiting \n for \n opponent";
                idleMulti = true;
						  }else if(door2C.bounds.Contains(transform.position)){
							  door2C.transform.parent.GetComponent<CustomDoor>().Open();
                // door1C.GetComponent<CustomDoor>().multi_txt.GetComponent<TextMesh>().text = "Waiting \n for \n opponent";
                door2C.transform.parent.GetComponent<CustomDoor>().multi_txt.GetComponent<TextMesh>().text = "Waiting \n for \n opponent";
                idleMulti = true;
						  }else if(d1SC.bounds.Contains(transform.position)){
							  d1SC.transform.parent.GetComponent<CustomDoor>().Toggle();
						  }else if(d2SC.bounds.Contains(transform.position)){
							  d2SC.transform.parent.GetComponent<CustomDoor>().Toggle();
						  }else{
                float v = PlayerPrefs.GetFloat("Volume", 0.5f);
                baseVolumeHeight = transform.position.y-v;
                transform.root.GetComponent<HandShrink>().StartChangingVolume(baseVolumeHeight, gameObject);
              }
            }
        }


        private void DoGripReleased(object sender, ControllerInteractionEventArgs e)
        {
            transform.root.GetComponent<HandShrink>().StopChangingVolume();
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "released", e);
        }

        private void DoGripTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "touched", e);
        }

        private void DoGripTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "untouched", e);
        }

        private void DoGripHairlineStart(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "hairline start", e);
        }

        private void DoGripHairlineEnd(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "hairline end", e);
        }

        private void DoGripClicked(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "clicked", e);
        }

        private void DoGripUnclicked(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "unclicked", e);
        }

        private void DoGripAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "GRIP", "axis changed", e);
        }

        private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "pressed down", e);
            //Makes hand colliders smaller for easier deck selection use
            //Debug.Log("ayy " + gameObject + " " + plyr + " " + plyr.Find("LeftHand/Render Model for LeftHand"));
            plyr.Find("LeftHand/Render Model for LeftHand").GetComponent<SphereCollider>().radius = 0.07f;
            plyr.Find("RightHand/Render Model for RightHand").GetComponent<SphereCollider>().radius = 0.07f;
            Debug.Log("shrunk");
        }

        private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "released", e);
        }

        private void DoTouchpadTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "touched", e);
        }

        private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "untouched", e);
        }

        private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "TOUCHPAD", "axis changed", e);

            if(!hasFlip)
            {
              if(e.touchpadAxis.x >= .7) {
                hasFlip = true;
                dsBook.GetComponent<DeckSelect>().FlipRight();
              }
              else if(e.touchpadAxis.x <= -.7) {
                hasFlip = true;
                dsBook.GetComponent<DeckSelect>().FlipLeft();
              }
            }
            else {
              if(e.touchpadAxis.x < .7 && e.touchpadAxis.x > -.7) {
                hasFlip = false;
              }
            }
        }

        private void DoButtonOnePressed(object sender, ControllerInteractionEventArgs e)
        {
            Debug.Log("pressed");
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "pressed down", e);
            DeckSelect dsScript;
            dsScript = dsBook.GetComponent<DeckSelect>();
            Debug.Log("pressed" + dsBook);
            dsScript.AddPage(); //SOMETHING ABOUT THIS LINE IS NOT WORKING. DOES NOT SEEM TO CALL ADDPAGE ???
        }

        private void DoButtonOneReleased(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "released", e);
        }

        private void DoButtonOneTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "touched", e);
        }

        private void DoButtonOneTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON ONE", "untouched", e);
        }

        private void DoButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "pressed down", e);
        }

        private void DoButtonTwoReleased(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "released", e);
        }

        private void DoButtonTwoTouchStart(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "touched", e);
        }

        private void DoButtonTwoTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "BUTTON TWO", "untouched", e);
        }

        private void DoStartMenuPressed(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "START MENU", "pressed down", e);
        }

        private void DoStartMenuReleased(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "START MENU", "released", e);
        }

        private void DoControllerEnabled(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "ENABLED", e);
        }

        private void DoControllerDisabled(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "DISABLED", e);
        }

        private void DoControllerIndexChanged(object sender, ControllerInteractionEventArgs e)
        {
            DebugLogger(VRTK_ControllerReference.GetRealIndex(e.controllerReference), "CONTROLLER STATE", "INDEX CHANGED", e);
        }
    }
}
