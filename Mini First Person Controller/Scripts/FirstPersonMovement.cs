using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{

    #region variable declarations
    public float speed = 9;
    GroundCheck groundCheck;
    public Camera FPSCamera;
    
    public enum MovementState
    {
        running,
        wallRunning,
        dashing,
        crouching,
        climbing,
        platformBuilding
    }

    [Header("State")]
    public MovementState movementState = MovementState.running;
    private Vector2 target2DVelocity;
    public Vector3 targetVelocity;
    private float stateSwitchTimer = 0f;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;


    [Header("Dashing")]
    public bool canDash = true;
    public KeyCode dash = KeyCode.LeftShift;
    public float dashSpeed = 45f;
    private float dashTimer = 0f;

    //these 2checks are here for hyperdash purposes
    private bool preGroundCheck; //checks if the player is on the ground before dashing
    private bool postGroundCheck; //checks if the player is on the ground after dashing


    [Header("Momentum")]
    public float momentumTimer = 0f; //how much time the player has momentum for
    public float momentumDividend = 0f; //this value is used to divide for lerp
    public float momentumSpeedX = 0f;
    public float momentumSpeedZ = 0f;

    [Header("Platform Building")]
    public GameObject buildObject; //the prefab that the player will be able to use and build
    public GameObject temporaryBuildObject; //collisionless transparent version
    private GameObject temporaryObject; //the object the player sees when determining where to place the object
    private GameObject currentObject; //the finalized place object
    private float platformDistance = 10f;


    public new Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    //public float[] speedOverrides = new float[0];

    #endregion

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        //Physics.gravity = new Vector3(0, -15f, 0);

    }

    void Update()
    {
        print(movementState);
        StateHandler();

        if (!groundCheck || groundCheck.isGrounded)
        {
            canDash = true;
        }
        //this section keeps track of all timers
        #region Timers 
        if (momentumTimer > 0)
        {
            momentumTimer = Mathf.Max(0, momentumTimer - Time.deltaTime);
        }
        else
        {
            momentumDividend = 0f;
        }

        if(stateSwitchTimer > 0)
        {
            stateSwitchTimer = Mathf.Max(0, stateSwitchTimer - Time.deltaTime);
        }


        #endregion
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        //float targetMovingSpeed = IsRunning ? runSpeed : speed;
        speed = 9;
        if (speedOverrides.Count > 0)
        {
            speed = speedOverrides[speedOverrides.Count - 1]();
        }
        // Apply movement.
        rigidbody.velocity = targetVelocity;

        if (momentumDividend > 0f) //adds momentum to the players velocity if there should be some
        {
            rigidbody.velocity = rigidbody.velocity + new Vector3(Mathf.Lerp(0, momentumSpeedX, momentumTimer / momentumDividend), 0, Mathf.Lerp(0, momentumSpeedZ, momentumTimer / momentumDividend));
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!groundCheck || groundCheck.isGrounded)
        {
            momentumTimer = 0f;
            momentumDividend = 0f;

            momentumSpeedX = 0f;
            momentumSpeedZ = 0f;
        }
    }

    public void Momentum(float momentumTime)
    {
        momentumTimer += momentumTime;
        momentumDividend += momentumTime;
    }

    public void StateHandler()
    {
        if (movementState == MovementState.running)
        {
            target2DVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
            targetVelocity = transform.rotation * new Vector3(target2DVelocity.x, rigidbody.velocity.y, target2DVelocity.y);


            if (Input.GetKeyDown(dash) && canDash)
            {
                movementState = MovementState.dashing;
                StartDash();
            }

            if (Input.GetKeyDown(KeyCode.F) && stateSwitchTimer == 0)
            {
                PlatformBuildStart();
            }
        }

        if (movementState == MovementState.climbing)
        {

        }

        if (movementState == MovementState.dashing)
        {
            target2DVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
            target2DVelocity = target2DVelocity + new Vector2(Input.GetAxis("Horizontal") * dashSpeed, Input.GetAxis("Vertical") * dashSpeed);
            targetVelocity = transform.rotation * new Vector3(target2DVelocity.x, rigidbody.velocity.y, target2DVelocity.y);
            if (dashTimer > 0)
            {
                dashTimer = Mathf.Max(0, dashTimer - Time.deltaTime);
            } else
            {
                EndDash();
                movementState = MovementState.running;
            }
        }

        if (movementState == MovementState.wallRunning)
        {
            //Enter and exit conditions are in the WallRunning Script
        }

        if (movementState == MovementState.crouching)
        {

        }

        if (movementState == MovementState.platformBuilding)
        {
            target2DVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
            targetVelocity = transform.rotation * new Vector3(target2DVelocity.x, rigidbody.velocity.y, target2DVelocity.y);

            temporaryObject.transform.position = FPSCamera.transform.position + platformDistance*FPSCamera.transform.forward;

            if(Input.mouseScrollDelta.y > 0)
                platformDistance += 1;

            if (Input.mouseScrollDelta.y < 0)
                platformDistance -= 1;

            platformDistance = Mathf.Max(5f, platformDistance);
            platformDistance = Mathf.Min(50f, platformDistance);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (temporaryObject != null)
                {
                    if (temporaryObject.GetComponent<TemporaryBuildPlatform>().valid)
                    {
                        PlatformPlace();
                        PlatformBuildEnd();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F) && stateSwitchTimer == 0)
            {
                PlatformBuildEnd();
            }

        }
    }

    public void StartDash()
    {
        dashTimer = .25f;

        //the following if else checks if the player is on the ground on the initial dash.
        if (!groundCheck || groundCheck.isGrounded)
            preGroundCheck = true;
        else
            preGroundCheck = false;

        canDash = false;
    }

    public void EndDash()
    {
        //these check if the player is touching the ground when the dash ends
        if (!groundCheck || groundCheck.isGrounded)
            postGroundCheck = true;
        else
            postGroundCheck = false;

        // if the player jumps during their dash, the player preserves momentum
        if (preGroundCheck && !postGroundCheck)
        {
            Momentum(1.5f);
            Vector2 momentumDirection = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
            momentumDirection.Normalize();

            momentumSpeedX = dashSpeed * momentumDirection.x;
            momentumSpeedZ = dashSpeed * momentumDirection.y;
        }
    }

    public void ClimbStart()
    {

    }

    public void ClimbEnd()
    {

    }

    public void PlatformBuildStart()
    {
        movementState = MovementState.platformBuilding; //sets state
        stateSwitchTimer = 0.1f; //makes its so the player cant spam state changes
        temporaryObject = Instantiate(temporaryBuildObject, FPSCamera.transform.position, Quaternion.identity); //creates the temporary object
    }

    public void PlatformBuildEnd()
    {

        movementState = MovementState.running;
        stateSwitchTimer = 0.1f;
        platformDistance = 10f;

        Destroy(temporaryObject); //destroys the temporaryone
    }

    public void PlatformPlace()
    {
        Destroy(currentObject); //destroys the old platform
        currentObject = Instantiate(buildObject, temporaryObject.transform.position, temporaryObject.transform.rotation); //replaces the temp platform with a real one
    }

}