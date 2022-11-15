using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwap : MonoBehaviour
{
    public Vector3 dimensionOffset;
    public Camera mainCamera;
    public Camera alternateCamera;
    public PlayerDimension playerDimension;
    public Material simulationSkybox;
    public Material realWorldSkybox;
    private GroundCheck groundCheck;
    private FirstPersonMovement firstPersonMovement;
    private float dimensionSwapTimer = 0f;

    private new Rigidbody rigidbody;

    public enum PlayerDimension
    {
        inSimulation = -1,
        inRealWorld = 1
    }
    // Start is called before the first frame update

    void Start()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
        firstPersonMovement = GetComponent<FirstPersonMovement>();
        rigidbody = GetComponent<Rigidbody>();
        playerDimension = PlayerDimension.inSimulation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            print("B key pressed");
            if(playerDimension == PlayerDimension.inSimulation)
                SwapToRealWorld();
            else if(playerDimension == PlayerDimension.inRealWorld)
                SwapToSimulation();
           
        }

        alternateCamera.transform.position = rigidbody.transform.position - ((int) playerDimension) * dimensionOffset;
        alternateCamera.transform.rotation = mainCamera.transform.rotation;
        
        //stops the players momentum if they touch the ground briefly after switching dimensions
        //this is primarily for swapping dimensions on a moving platform
        if(dimensionSwapTimer > 0f)
        {
            dimensionSwapTimer = Mathf.Max(0, dimensionSwapTimer - Time.deltaTime);

            if (!groundCheck || groundCheck.isGrounded)
            {
                firstPersonMovement.momentumTimer = 0f;
                firstPersonMovement.momentumDividend = 0f;
                firstPersonMovement.momentumSpeedX = 0f;
                firstPersonMovement.momentumSpeedZ = 0f;
            }
        }
    }

    public void SwapToSimulation()
    { 
        playerDimension = PlayerDimension.inSimulation;
        rigidbody.transform.position = rigidbody.transform.position - dimensionOffset;
        RenderSettings.skybox = simulationSkybox;

        dimensionSwapTimer = 0.1f;
       
    }

    public void SwapToRealWorld()
    {
        playerDimension = PlayerDimension.inRealWorld;
        rigidbody.transform.position = rigidbody.transform.position + dimensionOffset;
        RenderSettings.skybox = realWorldSkybox;

        dimensionSwapTimer = 0.1f;
    }
}
