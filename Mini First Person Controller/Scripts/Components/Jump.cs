using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    private FirstPersonMovement firstPersonMovement;
    public float jumpStrength = 4;
    public event System.Action Jumped;
    private float coyoteTime = 0f; //this allows the player to jump briefly after leaving a surface, .1 seconds of time.
    private bool doubleJump = false;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
        firstPersonMovement = GetComponent<FirstPersonMovement>();

    }
    
    void Update()
    {
        if(coyoteTime > 0f)
        {
            coyoteTime = Mathf.Max(0, coyoteTime - Time.deltaTime);
        }

        if (!groundCheck || groundCheck.isGrounded)
        {
            doubleJump = true;
        }

        

    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if ((Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded)) || (Input.GetButtonDown("Jump") && (coyoteTime>0f)))
        {
            coyoteTime = 0f;
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }

        if ((((Input.GetButtonDown("Jump") && doubleJump) && !(!groundCheck || groundCheck.isGrounded))) && firstPersonMovement.movementState != FirstPersonMovement.MovementState.wallRunning)
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            doubleJump = false;
        }


    }

    void OnCollisionExit(Collision collision)
    {
        coyoteTime = 0.2f;
    }
}
