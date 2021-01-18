using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{

    public static MobileInput Instance {get; set;}

    //It means 100 in Pixels.. 
    private const float DEAD_ZONE = 100.0f;

    private bool tap, swipeLeft, swipeRight;

    //Represents the current position the player is located.. 

    private Vector2 swipeDelta;


    //Beging touch.. 
    private Vector2 startTouch;

    public bool Tap {get {return tap;}}

    public Vector2 SwipeDelta {get{return swipeDelta;}}
    public bool SwipeLeft { get { return swipeLeft; }}
    public bool SwipeRight{ get { return swipeRight; }}

	// Use this for initialization
	void Start () 
    {
		
	}

    private void Awake()
    {
        //It means we would have this script somewhere in the scene.. 
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Resetting all the booleans to false.. 
        tap = false;
        swipeLeft = false;
        swipeRight = false;

        //Checking inputs.., left mouse click..

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            Debug.Log("Mouse has been pressed");

            //Start touch to the positon of the mouse at this exact position.. 
            startTouch = Input.mousePosition;
        }
        //If we released the left click of the mouse.. 
        else if (Input.GetMouseButtonUp(0))
        {
            //Resetting our Input.. 
            startTouch = swipeDelta = Vector2.zero;

            //Apague esto.. y lo puse en ManagerG
            //Stamima.Instance.staminaDamageLoss();
            //FroggieMovement.Instance.testingMovement();

        }
        #endregion

        #region MobileInputs
        //If at least we have one touch on the screen... 
        if (Input.touches.Length != 0)
        {
            //What's the current phase of this touch.. 
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Debug.Log("Se Presiono el dedo");

                tap = true;
                startTouch = Input.mousePosition;

                //Damaging the player per second, This class was called before in the Stamina Class..
                //Stamima.Instance.staminaDamageLoss();

            

                //New class Jumping Forward.. 

            }

            //If we ended the touch phase or either has been cancelled.. 
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }
        #endregion


        //Calcuting the distance if we have a swipe or not.. 
        //Distance between your start touch and where you're right not.. 
        swipeDelta = Vector2.zero;

        //if is not equal, that means we started touching somewhere.. 
        if(startTouch != Vector2.zero)
        {
            //Checking for mobile.. 
            //if is not equal to zero, it means we have a touch.. 
            if(Input.touches.Length != 0)
            {
                //it will give us the position between my finger is and where I started dragging my finger.. 
                swipeDelta = Input.touches[0].position - startTouch;
            }
            //lets check with Standalone input.. 
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        //Calculating if we're beyond the dead zone.. 
        if(swipeDelta.magnitude > DEAD_ZONE)
        {
            //Confirmed swipe.. 
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            //It will return a positive number..
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Either left or Right.. 
                if(x < 0)
                {
                    //Si el número es negativo.. 
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                //Up or Down.. 
            }

            //We don't want a confirmed swipe two frames in a row, so lets reset it until we drop the finger..
            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
