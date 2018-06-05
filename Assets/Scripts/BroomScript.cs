using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomScript : MonoBehaviour
{
    //for selecting leveling method
    public bool emergencyReverse;
    public bool levelInstantly;
    public bool isLeveling;
    public bool isFrozen;
    public bool doesDrag;

    //the current player & their head
    public GameObject player;
    public GameObject head;
    public bool isMulti = false;

    //is there anyone riding
    private bool isMounted = false;
    private Vector3 mountLocation;

    //used for better broom placement
    private bool firstPress = true;

    //for rotation control
    private float yrotatespeed = 0.003f;
    private float zrotatespeed = 0.5f;
    private Vector3 startingUp;

    //for speed control
    public bool constVel;
    private const float CONST_SPD = 10f;
    private float currentPressure = 0f;
    private int acceleration = 6;
    private int maxSpeed = 20;

    //for controls control
    private float maxDeviate = 0.1f;

    //player's rigid body, velocity changes made on this
    private Rigidbody prb;

    //for tutorial stuff
    public int tutLvl = 0;
    public float waitLeft = 1f;
    public bool completed = false;
    public bool reorienting = false;

    //???
    private float broomD = 0.75f;

    public TownPlayer plrScr;

    void Start()
    {
      prb = player.GetComponent<Rigidbody>();
      if(isMulti){
        if(!transform.root.GetComponent<TownPlayer>().isLocalPlayer){
          transform.Find("Character").gameObject.SetActive(true);
        }
      }
    }

    void FixedUpdate()
    {
        if (isMounted)
        {
          if (!isMulti)
          {
            TestMethod1();
          }
          else if (plrScr.getStartTime() != -1f)
          {
            TestMethod1();
          }
        }

        waitLeft = waitLeft - Time.deltaTime;
        if(!isMulti)
        {
          if(reorienting)
          {
            if(waitLeft <= 0)
            {
              if(tutLvl == 4) {
                waitLeft = 1f;
              } else {
                waitLeft = 5f;
              }
              reorienting = false;
              UnfreezePrb();
            }
          }
          else if(completed && waitLeft <= 0)
          {
            if(tutLvl == 0 || tutLvl >= 3) //levels with no wait time
            {
              ++tutLvl;
              completed = false;
              waitLeft = 1f;
            }
            else
            {
              ++tutLvl;
              completed = false;
              waitLeft = 1f;
            }
            if (tutLvl < 5)
            {
              reorienting = true;
              FreezePrb();
            }
          }
        }
    }

    public void UpdateCurrentPressure(float f)
    {
        currentPressure = f;
        if(isMounted)
        {
          if(firstPress)
          {
            //put the broom under you when you first want to move
            mountLocation = head.transform.localPosition;
            transform.position = new Vector3(head.transform.position.x, head.transform.position.y - broomD, head.transform.position.z);
            firstPress = false;
          }
          else if(doesDrag && currentPressure < .5)
          {
            //Probably need to rescale this
            prb.GetComponent<Rigidbody>().drag = 1;
          }
        }
        if(!isMulti && tutLvl == 1)
        {
          completed = true;
        }
    }

    public void ButtonOnePressed()
    {
      if(isMounted)
      {
        FreezePrb();
        if(tutLvl == 5)
        {
          completed = true;
        }
      }
    }

    public void ButtonOneReleased()
    {
      if(isMounted) { UnfreezePrb(); }
    }

    public void ButtonTwoPressed()
    {
        if(isMounted)
        {
          if(levelInstantly)  { InstantLevel(); }
          else                { TimedLevel(); }

          if(tutLvl == 4)
          {
            completed = true;
          }
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
            mountLocation = head.transform.localPosition;
            isMounted = true;
            transform.parent = player.transform;
            transform.position = new Vector3(head.transform.position.x, head.transform.position.y - broomD, head.transform.position.z);
            startingUp = Vector3.up;
            if(!isMulti && tutLvl == 0){
              completed = true; //level 0 to 1
            }
            if(isMulti){
              maxSpeed = 50;
              acceleration = 12;
            }
        }
    }

    //rotate and shift method
    public void TestMethod1()
    {
        if(!isFrozen)
        {
          //finds head displacement from broom
          float zDif = head.transform.localPosition.z - mountLocation.z;
          float yRot = 0.0f;

          yRot = head.transform.localRotation.eulerAngles.y;
          if(yRot > 180)
          {
            yRot = yRot - 360;
          }
          if(Mathf.Abs(yRot) >= 15.0f && tutLvl == 2)
          {
            completed = true;
          }

          //finds you current speed
          float speed = prb.velocity.magnitude;

          //adjusts to rotation around y
          if(tutLvl >= 2)
          {
            if(tutLvl != 3)
            {
              player.transform.RotateAround(player.transform.position, player.transform.up, yrotatespeed * yRot * speed);
            }
            if(Mathf.Abs(zDif) >= 0.05 && tutLvl == 3)
            {
              completed = true;
            }
          }

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
          if(tutLvl >= 3)
          {
            if (zDif < maxDeviate)
            {
              player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * rotationFactor * zDif * speed);
            }
            else
            {
              player.transform.RotateAround(player.transform.position, player.transform.right, zrotatespeed * rotationFactor * maxDeviate * speed);
            }
          }

          MoveForward();
          //CheckStop();  instead use an event listener
        }
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
            prb.velocity = transform.forward * CONST_SPD;
        }
    }

    private void InstantLevel()
    {
      //find intended right vector
      Vector3 intendedRight = Vector3.Cross(Vector3.up, player.transform.forward);

      //project right and intendedRight onto the plane with normal equal to the brooms forward
      Vector3 projRight = Vector3.ProjectOnPlane(player.transform.right, player.transform.forward);
      Vector3 projIntendedRight = Vector3.ProjectOnPlane(intendedRight, player.transform.forward);

      //find angle between projected vectors
      float angle = SignedAngle(projRight, projIntendedRight, player.transform.forward);

      //rotate so that the rights are in line
      player.transform.RotateAround(player.transform.position, player.transform.forward, angle);
    }

    private void TimedLevel()
    {
      //find intended right vector
      Vector3 intendedRight = Vector3.Cross(Vector3.up, player.transform.forward);

      //project right and intendedRight onto the plane with normal equal to the brooms forward
      Vector3 projRight = Vector3.ProjectOnPlane(player.transform.right, player.transform.forward);
      Vector3 projIntendedRight = Vector3.ProjectOnPlane(intendedRight, player.transform.forward);

      //find angle between projected vectors
      float angle = SignedAngle(projRight, projIntendedRight, player.transform.forward);

      //rotate so that the rights are in line
      if(!isLeveling)
      {
        isLeveling = true;
        StartCoroutine(LevelStepProp(angle));
      }
    }

    IEnumerator LevelStepProp(float angle)
    {
      //duration of level
      float duration = 1f;
      for(var t = 0f; t < duration; t += Time.deltaTime)
      {
        player.transform.RotateAround(transform.position, transform.forward, Time.deltaTime * angle / duration);
        yield return null;
      }
      isLeveling = false;
    }

    private float SignedAngle(Vector3 from, Vector3 to, Vector3 n)
    {
        return(Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(from, to)), Vector3.Dot(from, to)) * Mathf.Rad2Deg);
    }

    private void FreezePrb()
    {
      isFrozen = true;
      prb.constraints = RigidbodyConstraints.FreezePositionX;
      prb.constraints = RigidbodyConstraints.FreezePositionY;
      prb.constraints = RigidbodyConstraints.FreezePositionZ;
      //consider messing with momentum
    }

    private void UnfreezePrb()
    {
      isFrozen = false;
      prb.constraints &= ~RigidbodyConstraints.FreezePositionX;
      prb.constraints &= ~RigidbodyConstraints.FreezePositionY;
      prb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
      //consider messing with momentum
    }
}
