using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    private FirstPersonMovement movementScript;
    private GroundCheck groundCheck;
    public Transform orientation;
    private new Rigidbody rigidbody;

    [Header("Wall Running")]
    public float wallRunSpeed;
    public float wallJumpForce;
    public float wallJumpUpForce;
    //private float coyoteTime = 0.0f;


    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detections")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool leftWall;
    private bool rightWall;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementScript = GetComponent<FirstPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        orientation = transform;
        CheckForWall();
        StateMachine();
        if (movementScript.movementState == FirstPersonMovement.MovementState.wallRunning)
            WallRunMovement();
    }

    private void CheckForWall()
    {
        rightWall = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance);
        leftWall = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance);    
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if((rightWall || leftWall) && verticalInput > 0 && AboveGround())
        {
            if(movementScript.movementState == FirstPersonMovement.MovementState.running)
                StartWallRun();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                WallJump();
            }
        }
        else
        {
            if(movementScript.movementState == FirstPersonMovement.MovementState.wallRunning)
                StopWallRun();
        }

    }

    private void StartWallRun()
    {
        movementScript.movementState = FirstPersonMovement.MovementState.wallRunning;
    }

    private void WallRunMovement()
    {
        rigidbody.useGravity = false;

        Vector3 wallNormal = rightWall ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);
        //wallForward.Normalize();

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        movementScript.targetVelocity = wallRunSpeed * wallForward;
       // movementScript.targetVelocity = wallRunSpeed*(new Vector3(wallForward.x, rigidbody.velocity.y, wallForward.z));



    }

    private void StopWallRun()
    {
        movementScript.movementState = FirstPersonMovement.MovementState.running;
        rigidbody.useGravity = true;
    }

    public void WallJump()
    {
        Vector3 wallNormal = rightWall ? rightWallHit.normal : leftWallHit.normal;
        Vector3 jumpForce = transform.up * wallJumpUpForce + wallNormal * wallJumpForce;

        rigidbody.AddForce(jumpForce, ForceMode.Impulse);
    }
}
