using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMov : MonoBehaviour
{

    //Distances between lanes.. 
    private const float LANE_DISTANCE = 1.4f;
    private const float TURN_FROG_SPEED = 0.05f;

    private Vector3 targetPosition; 

    //Lanes..
    private int desiredLanes = 1;

    //Turning speed of the frog when it moves to the right.. 

    private Vector3 playerVector3;
    private float velocity = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Velocity variable changing every second..
        velocity = Mathf.Clamp(velocity, 5.7f, 6.5f);


        if(ManagerG.Instance.isGameStarted == true)
        {
            //Damage loss..
            //Stamima.Instance.staminaDamageLoss();
        }
        
        if (MobileInput.Instance.SwipeLeft)
        {
            moveLanes(false);
        }

        if (MobileInput.Instance.SwipeRight)
        {
            moveLanes(true);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Go and move character to the Left");
            moveLanes(false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Go and move Character to the Right");
            moveLanes(true);
        }

        targetPosition = transform.position.z * Vector3.forward;
        if(desiredLanes == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLanes == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        //If player goes below a certain area.. Restart Level..
        if(this.transform.position.y < -0.76)
        {
            //Restart Level if the player goes below certain level in the map.. 
            RestartLevel.Instance.levelRestart();
        }
    }

    private void FixedUpdate()
    {
        //If player has tapped, start moving.. 
        if (ManagerG.Instance.isGameStarted == true)
        {
            //Actual movement of the player.. 
            movementSpeeed(velocity);
       
        }
      
    }


    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Water")
        {
            RestartLevel.Instance.levelRestart();
        }
    }

    private void movementSpeeed(float speedModifier)
    {
        //Movement sideways calculator..
        Vector3 movingSideWays = Vector3.zero;
        float testSpeed = speedModifier * Time.deltaTime;

        //Desired movement in the X position
        movingSideWays.x = (targetPosition - transform.position).normalized.x * speedModifier;
        //this.transform.position = Vector3.MoveTowards(this.transform.position, movingSideWays, testSpeed);

        //For the X Axis Movement.. 
        Vector3 xMovement = Vector3.MoveTowards(this.transform.position, movingSideWays, testSpeed);
        
        //Movement forward, and X Movement.. Multiplied X Movement Testing.. 
        this.transform.position = new Vector3 (xMovement.x, this.transform.position.y, this.transform.position.z + speedModifier * Time.deltaTime);
      
        
    }

    private void moveLanes(bool isMovingRight)
    {
        //It will return 1 if is moving right..
        desiredLanes += (isMovingRight) ? 1 : -1;
        desiredLanes = Mathf.Clamp(desiredLanes, 0, 2);
    }
}
