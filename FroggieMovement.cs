using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggieMovement : MonoBehaviour
{

    public static FroggieMovement Instance {get; set;}
    //Because the asset is 3 meters in lenght.. whatever the size it is..
    //you should go there
    private const float LANE_DISTANCE = 1.4f;


    private const float TURN_FROGGIE_SPEED = 0.05f;

    //
    //private bool isGameHasStarted = false;

    //Which layers we would like to look out (ground layers)
    //private LayerMask groundLayers;


    /*
    //Jumping Mechanic.. check if our character is grounded.. 
    private Rigidbody rg;
    private float jumpForce = 7;
    private SphereCollider col;
    */



    //Animation Controller broo.. ! lol
    Animator anim;

    //Movement Variables.. 
    private CharacterController controller;
    private float jumpForce = 5.0f;
    private float gravity = 12.0f;
    private float verticalVelocity;
    private Transform playerTransform; 

    //Bouncincess effect via material
    private PhysicMaterial physicMaterial;

    //Testing Purposes..
    private float jumpJumpForce = 12.0f;
    private float downForceForce = 18.0f;
    private float TestingGravity = 9.7f;
    private float jumpVelocity;
    private float movementSpeed = 1.0f;


    //Froggie original speed.. 
    private float originalSpeed = 6.0f;

    //Speed Modifier.. 
    private float speed;

    //Last time we increased the Speed..
    private float speedLastIncreasedTick;

    //How it will take us to increase the speed..
    private float speedIncreaseTime = 2.5f;

    //How much we increased the speed.. 
    private float speedIncreaseAmount = 0.1f;

    //private bool isMoving;

    //0 = Left, 1= Middle, 2= Right
    private int desiredLane = 1;

    private Vector3 targetPosition;

    //Testing Variable, for Jumping Testing Purposes.. 
    private Rigidbody rg;


    // Use this for initialization
    void Start()
    {
        //Speed original value.. 
        speed = originalSpeed;

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();

        //Testing purposes.. 
        //rg = GetComponent<Rigidbody>();

        rg = GetComponent<Rigidbody>();
        

        //isMoving = false;
    }

    private void Awake()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        //Value has to be true in order to start the game.. 
        //Testing esto se esta llamando en el managerGame Script..
        //startRunning();

        if(Time.time - speedLastIncreasedTick > speedIncreaseTime )
        {
            //Resetting the Last Time velocity was increased.. 
            speedLastIncreasedTick = Time.time;

            //Increasing velocity.. 
            //speed += speedIncreaseAmount;

            //Change the modifier text.. 
            //ManagerG.Instance.UpdateModifier(speed - originalSpeed);
        }

        //As soon as we pressed the X button, the player will move FORWARD.. 
        //Pressed forward..
        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Up key has been pressed");

            isMoving = true;
            return;
        }
        */

        if (MobileInput.Instance.SwipeLeft)
        {
            MoveLane(false);
        }

        if(MobileInput.Instance.SwipeRight)
        {
            MoveLane(true);
        }


        /*
        //OLD MOVING SCRIPT
        //Getting input on which lane we should be.. 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLane(false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);
        }
        */

        //Where we wanna go next..
        //our current position.. 
        targetPosition = transform.position.z * Vector3.forward;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }

        //If player goes down in certain area, died.. Restart Level
        if(playerTransform.position.y < -0.76)
        {
            RestartLevel.Instance.levelRestart();
        }

        //Place movement function.. 
        //ForwardMovement(isMoving);

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "RockPrefab" || col.gameObject.tag == "SmallCortezPrefab" || col.gameObject.tag == "SandMount")
        {
            Debug.Log("It entered..");

            froggieJump(); 
        }
    }

    private void MoveLane(bool isGoingRight)
    {
        desiredLane += (isGoingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);
    }

    //Movement forward. THIS HAS BEEN ALSO IMPLEMENTE ON THE GAME MANAGER SCRIPT.. SO IT HAS BEEN CALLED OUT THERE
    public void ForwardMovement(bool moving)
    {
        /*
        if (moving == true)
        {
            //Movement calculation.. 
            Vector3 moveVector = Vector3.zero;
            moveVector.x = (targetPosition - transform.position).normalized.x * speed;

            //Calculating Y
            //If true, isGrounded.. 
            if(isGrounded())
            {
                verticalVelocity = -0.1f;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    //Check if we're doing collision in order to trigger jumping animation.. 
                    Debug.Log("SpaceButton has been pressed");
                    verticalVelocity = jumpForce;
                    anim.SetBool("isJumping", true);
                }

            }
            else
            {
                //Reduce altitue per frame.., slowly
                verticalVelocity -= (gravity * Time.deltaTime);
                //Number goes down whenever you're not grounded.. 
                if (Input.GetKeyDown(KeyCode.F))
                {
                    verticalVelocity = -jumpForce;
                }
            }

            //Gravity system..
            moveVector.y = verticalVelocity;

            //Moving forward..
            moveVector.z = speed;

            //Moving the froggie..
            controller.Move(moveVector * Time.deltaTime);

            //Rotating Froggie where he's actually going.. 
            rotatingFroggie();

            //Playing animation..
            anim.SetBool("isJumping", true);
        }
        else
        {
            return;
        }
        */

    }

    private void jumping()
    {
        //rg.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
    }

    /*
    private bool itsGrounded()
    {

        //.9f reason: because we wanna 90 percent actual size of the sphere.. , 
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
        col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);

       
    } 
    */

    //Maybe removing this method from here to the Manager Game (update call there)
    /*
    private void startRunning()
    {
        if (!isMoving)
        {
            return; 
        }

    }
    */

    private void rotatingFroggie()
    {
        //Rotating froggie to the direction he's about to go.. 
        Vector3 dir = controller.velocity;

        //This if I can remove it if I want to.. 
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_FROGGIE_SPEED);
        }

    }   

    private bool isGrounded()
    {
        //It takes an original position and direction..
        Ray groundRay = new Ray(new Vector3(controller.bounds.center.x,
                                           (controller.bounds.center.y -
                                            controller.bounds.extents.y) + 0.2f,
                                            controller.bounds.center.z),
                                            Vector3.down);

        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.cyan, 1.0f);

        //It returns either true or false.. Distance from the floor..
        return Physics.Raycast(groundRay, 0.2f + 0.1f);
    }

   public void froggieJump()
    {
        //rg.AddForce(0, jumpForce, 0);

        rg.AddForce(Vector3.up * jumpJumpForce, ForceMode.Impulse);
    }

    public void testingMovement(bool moving)
    {
        if(moving == true)
        {
            if(controller.isGrounded)
            {
                movementSpeed = 5.0f;

                //Stays the same and we are not applying the down-force as our character is not jumping yet.
                jumpVelocity = jumpJumpForce;

            }
            else if (!controller.isGrounded)
            {
                movementSpeed = 0.0f;

                jumpJumpForce -= downForceForce * Time.deltaTime;
            }

            //To Handle the jumping.. 
            Vector3 moveVector = new Vector3(0, jumpVelocity, 0);

            //Actuall jump..
            controller.Move(moveVector * Time.deltaTime);
        }
    }
}
