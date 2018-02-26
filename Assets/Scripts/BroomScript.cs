//
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class BroomScript : MonoBehaviour {
// 	public GameObject player;
// 	public GameObject head;
// 	// public GameObject headReferenceObject;
// 	// private HeadReference headReference;
// 	private float currentPressure=0f;
// 	private int acceleration = 10;
// 	// private float broomHeight = 222.5f;
// 	private bool isMounted = false;
// 	private Rigidbody prb;
// 	private int yrotatespeed = 1;
// 	private float zrotatespeed = 0.5f;
// 	private Vector3 mountLocation;
// 	private int maxSpeed = 15;
// 	private float maxDeviate = 0.1f;
// 	private float broomD = 0.75f;
// 	// Use this for initialization
// 	void Start () {
// 		prb = player.GetComponent<Rigidbody>();
// 		// headReference = headReferenceObject.GetComponent<HeadReference>();
// 		// broomHeight = head.transform.position.y-broomD;
// 	}
//
// 	// Update is called once per frame
// 	void Update () {
//
// 	}
// 	void FixedUpdate(){
// 		if(isMounted){
// 			Vector3 difference = head.transform.localPosition-mountLocation;
// 			float speed = prb.velocity.magnitude;
// 			if(difference.x<maxDeviate){
// 				player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed*difference.x*speed);
// 			}else{
// 				player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed*maxDeviate*speed);
// 			}
// 			if(difference.z<maxDeviate){
// 				player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed*difference.z*speed);
// 			}else{
// 				player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed*maxDeviate*speed);
// 			}
// 			prb.AddForce(player.transform.forward*acceleration*currentPressure);
// 			prb.velocity = transform.forward*prb.velocity.magnitude;
// 			if(prb.velocity.magnitude>maxSpeed){
// 				prb.velocity = transform.forward*maxSpeed;
// 			}
//             //stop button
//             if (OVRInput.GetDown(OVRInput.Button.One))
//             {
//                 bool flag = true;
//                 prb.velocity = transform.forward * 0;
//                 while (flag)
//                 {
//                     if (OVRInput.GetDown(OVRInput.Button.One))
//                     {
//                         flag = false;
//                     }
//                 }
//             }
// 		}
// 	}
// 	public void UpdateCurrentPressure(float f){
// 		currentPressure = f;
// 	}
// 	void OnTriggerEnter(Collider c){
// 		if(c.gameObject.tag != "Town"){
// 			Mount();
//
// 			// headReference.Mount();
// 		}
// 	}
// 	public void Mount(){
// 		if(!isMounted){
// 			Debug.Log("mounted");
// 			mountLocation = head.transform.localPosition;
// 			isMounted = true;
// 			transform.parent = player.transform;
// 			transform.position = new Vector3(head.transform.position.x, head.transform.position.y-broomD, head.transform.position.z);
// 		}
// 	}
// }

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomScript : MonoBehaviour
{
    //for selecting driving method
    [Range(-1, 1)]
    public int drivingMethod;

    //the current player & their head
    public GameObject player;
    public GameObject head;

    //is there anyone riding
    private bool isMounted = false;
    private Vector3 mountLocation;

    //for rotation control
    private float yrotatespeed = 0.5f;
    private float zrotatespeed = 0.5f;

    //for speed control
    public bool constVel;
    private const float CONST_SPD = 10f;
    private float currentPressure = 0f;
    private int acceleration = 10;
    private int maxSpeed = 15;

    //for controls control
    private float maxDeviate = 0.1f;

    //player's rigid body, velocity changes made on this
    private Rigidbody prb;

    //???
    private float broomD = 0.75f;


    void Start()
    {
        prb = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isMounted)
        {
            if      (drivingMethod == 1)  { TestMethod1(); }
            else if (drivingMethod == -1) { SammyTestMethod(); }
            else                          { DaniCurrentMethod(); }
        }
    }

    public void UpdateCurrentPressure(float f)
    {
        currentPressure = f;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag != "Town")
        {
            Mount();
        }
    }

    public void Mount()
    {
        if (!isMounted)
        {
            Debug.Log("mounted");
            mountLocation = head.transform.localPosition;
            isMounted = true;
            transform.parent = player.transform;
            transform.position = new Vector3(head.transform.position.x, head.transform.position.y - broomD, head.transform.position.z);
        }
    }
    public void DaniCurrentMethod()
    {
        Vector3 difference = head.transform.localPosition - mountLocation;
        float speed = prb.velocity.magnitude;
        if (difference.x < maxDeviate)
        {
            player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed * difference.x * speed);
        }
        else
        {
            player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed * maxDeviate * speed);
        }
        if (difference.z < maxDeviate)
        {
            player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * difference.z * speed);
        }
        else
        {
            player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * maxDeviate * speed);
        }
        prb.AddForce(player.transform.forward * acceleration * currentPressure);
        prb.velocity = transform.forward * prb.velocity.magnitude;
        if (prb.velocity.magnitude > maxSpeed)
        {
            prb.velocity = transform.forward * maxSpeed;
        }
    }

    public void SammyTestMethod() //only forward
    {
        //sync rotation with head (may need to smooth this out)
        transform.rotation = head.transform.localRotation;

        //Add forward velocity
        prb.velocity = head.transform.forward * CONST_SPD;

        CheckStop();
    }

    public void TestMethod1() //rotate and shift method
    {
        //finds head displacement from broom
        float zDif = head.transform.localPosition.z - mountLocation.z;
        float yRot = 1 * head.transform.localRotation.y;

        //finds you current speed
        float speed = prb.velocity.magnitude;

        //adjusts to rotation around y
        player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed * yRot * speed);

        //manage the up and down motion using the heads position

        if (zDif < maxDeviate)
        {
            player.transform.RotateAround(player.transform.position, head.transform.right, zrotatespeed * zDif * speed);
        }
        else
        {
            player.transform.RotateAround(player.transform.position, head.transform.right, zrotatespeed * maxDeviate * speed);
        }

        MoveForward();
        CheckStop();
    }

    public void MoveForward()
    {
        if (!constVel) //if you can accelerate with a button
        {
            //adds velocity to the player
            prb.AddForce(player.transform.forward * acceleration * currentPressure);

            //directs the velocity forward
            prb.velocity = transform.forward * prb.velocity.magnitude;

            //if velocity is too large it gets limited before being directed forward
            // prb.velocity = transform.up * prb.velocity.magnitude;
            if (prb.velocity.magnitude > maxSpeed)
            {
                prb.velocity = transform.forward * maxSpeed;
            }
        }
        else //if there is no acceleration
        {
            prb.GetComponent<Rigidbody>().velocity = transform.forward * CONST_SPD;
        }
    }

    public void CheckStop()
    {
        //stop button (I copied this code from above I think it just stops you)

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            bool flag = true;
            prb.velocity = transform.forward * 0;
            while (flag)
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    flag = false;
                }
            }
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomScript : MonoBehaviour
{
    //for selecting driving method
    [Range(-1, 1)]
    public int drivingMethod;

    //the current player & their head
    public GameObject player;
    public GameObject head;

    //is there anyone riding
    private bool isMounted = false;
    private Vector3 mountLocation;

    //used for better broom placement
    private bool firstPress = true;

    //for rotation control
    private float yrotatespeed = 0.5f;
    private float zrotatespeed = 0.5f;
    private Vector3 startingUp;

    //for speed control
    public bool constVel;
    private const float CONST_SPD = 10f;
    private float currentPressure = 0f;
    private int acceleration = 10;
    private int maxSpeed = 15;

    //for controls control
    private float maxDeviate = 0.1f;

    //player's rigid body, velocity changes made on this
    private Rigidbody prb;

    //???
    private float broomD = 0.75f;


    void Start()
    {
        prb = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isMounted)
        {
            if      (drivingMethod == 1)  { TestMethod1(); }
            else if (drivingMethod == -1) { SammyTestMethod(); }
            else                          { DaniCurrentMethod(); }
        }
    }

    public void UpdateCurrentPressure(float f)
    {
        currentPressure = f;
        if(isMounted && firstPress)
        {
            //put the broom under you when you first want to move
            mountLocation = head.transform.localPosition;
            transform.position = new Vector3(head.transform.position.x, head.transform.position.y - broomD, head.transform.position.z);
            firstPress = false;
        }
    }
    public void ButtonTwoPressed()
    {
        if(isMounted)
        {
          Debug.Log("Leveling");
          //match ups
          //player.transform.up = startingUp;
          //player.transform.rotation = Quaternion.FromToRotation(player.transform.up, Vector3.up);
          //find rotation around forward
          //rotate back around forward gradually

          //rotate so you are parallel to the ground
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag != "Town")
        {
            Mount();
        }
    }

    public void Mount()
    {
        if (!isMounted)
        {
            Debug.Log("mounted");
            mountLocation = head.transform.localPosition;
            isMounted = true;
            transform.parent = player.transform;
            transform.position = new Vector3(head.transform.position.x, head.transform.position.y - broomD, head.transform.position.z);
            startingUp = Vector3.up;
        }
    }
    public void DaniCurrentMethod()
    {
        Vector3 difference = head.transform.localPosition - mountLocation;
        float speed = prb.velocity.magnitude;
        if (difference.x < maxDeviate)
        {
            player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed * difference.x * speed);
        }
        else
        {
            player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed * maxDeviate * speed);
        }
        if (difference.z < maxDeviate)
        {
            player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * difference.z * speed);
        }
        else
        {
            player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * maxDeviate * speed);
        }
        prb.AddForce(player.transform.forward * acceleration * currentPressure);
        prb.velocity = transform.forward * prb.velocity.magnitude;
        if (prb.velocity.magnitude > maxSpeed)
        {
            prb.velocity = transform.forward * maxSpeed;
        }
    }

    //only forward
    public void SammyTestMethod()
    {
        //sync rotation with head (may need to smooth this out)
        transform.rotation = head.transform.localRotation;

        //Add forward velocity
        prb.velocity = head.transform.forward * CONST_SPD;

        CheckStop();
    }

    //rotate and shift method
    public void TestMethod1()
    {
        //finds head displacement from broom
        float zDif = head.transform.localPosition.z - mountLocation.z;
        float yRot = head.transform.localRotation.y;

        //finds you current speed
        float speed = prb.velocity.magnitude;

        //adjusts to rotation around y
        player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed * yRot * speed);


        //manage the up and down motion using the heads position
        float rotationFactor;
        if (zDif > 0)
        {
            //want rotating downward to be faster
            rotationFactor = 1.2f;
        }
        else
        {
            //want rotating upward to be slower
            rotationFactor = 0.8f;
        }
        //actually do the up and down rotation
        if (zDif < maxDeviate)
        {
            player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * rotationFactor * zDif * speed);
        }
        else
        {
            player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * rotationFactor * maxDeviate * speed);
        }

        MoveForward();
        CheckStop();
    }

    public void MoveForward()
    {
        if (!constVel) //if you can accelerate with a button
        {
            //adds velocity to the player
            prb.AddForce(player.transform.forward * acceleration * currentPressure);

            //directs the velocity forward
            prb.velocity = transform.forward * prb.velocity.magnitude;

            //if velocity is too large it gets limited before being directed forward
            prb.velocity = transform.forward * prb.velocity.magnitude;
            if (prb.velocity.magnitude > maxSpeed)
            {
                prb.velocity = transform.forward * maxSpeed;
            }
        }
        else //if there is no acceleration
        {
            prb.GetComponent<Rigidbody>().velocity = transform.forward * CONST_SPD;
        }
    }

    public void CheckStop()
    {
        //stop button (I copied this code from above I think it just stops you)

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            bool flag = true;
            prb.velocity = transform.forward * 0;
            while (flag)
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    flag = false;
                }
            }
        }
    }
}
