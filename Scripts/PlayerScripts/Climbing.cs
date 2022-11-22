using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    private FirstPersonMovement firstPersonMovement;
    private new Rigidbody rigidbody;
    //public Transform orientation;
    private GroundCheck groundCheck;

    [Header("Climbing")]
    public float maxStamina;
    private float stamina = 0f;
    public float climbSpeed;
    public float climbHorizontalSpeed;
    private Vector2 target2DVelocity;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float currentWallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallInFront; 

    // Start is called before the first frame update
    void Start()
    {
        firstPersonMovement = GetComponent<FirstPersonMovement>();
        rigidbody = GetComponent<Rigidbody>();
        groundCheck = GetComponent<GroundCheck>();

        climbHorizontalSpeed = firstPersonMovement.speed;
    }

    // Update is called once per frame
    void Update()
    {
        WallCheck();
        StateMachine();

        if (firstPersonMovement.movementState == FirstPersonMovement.MovementState.climbing)
            ClimbingMovement();

        print(stamina);
    }

    private void WallCheck()
    {
        wallInFront = Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out frontWallHit, detectionLength);
        currentWallLookAngle = Vector3.Angle(transform.forward, - frontWallHit.normal);

    }

    private void StateMachine()
    {
        if((wallInFront && Input.GetKey(KeyCode.W)) && currentWallLookAngle < maxWallLookAngle)
        {
            if ((firstPersonMovement.movementState != FirstPersonMovement.MovementState.climbing) && stamina > 0f)
            {
                StartClimbing();

                if (stamina > 0)
                    stamina = Mathf.Max(0, stamina - Time.deltaTime);

                if (stamina <= 0)
                    EndClimbing();
            }
        }
        else
        {
            if ((firstPersonMovement.movementState == FirstPersonMovement.MovementState.climbing))
                EndClimbing();

            if (!groundCheck || groundCheck.isGrounded)
                stamina = maxStamina;
        }

       
    }

    private void StartClimbing()
    {
        firstPersonMovement.movementState = FirstPersonMovement.MovementState.climbing;
    }


    private void ClimbingMovement()
    {
        target2DVelocity = new Vector2(Input.GetAxis("Horizontal") * climbHorizontalSpeed, 0);
        firstPersonMovement.targetVelocity = transform.rotation * new Vector3(target2DVelocity.x, climbSpeed, target2DVelocity.y);
    }

    private void EndClimbing()
    {
        firstPersonMovement.movementState = FirstPersonMovement.MovementState.running;
    }
}
