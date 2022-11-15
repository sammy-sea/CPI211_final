using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 pointA; //starting point
    [SerializeField] private Vector3 pointB; //end point
    [SerializeField] public float speed = 20; //speed moving forward
    [SerializeField] public float returnSpeed = 4; // speed moving backwards
    [SerializeField] private float timer = 0f;
    private float alertTimer; //state timer

    private float distance; //distance between pointA and pointB. used for adjusting speed for variable distancs
    private Vector3 offset; //distance moved between steps. used for moving the player on the platform
    private bool riding = false; //true if player is on it
    private Transform ridingObject; //the object that is on the platform
    private PlatformState platformState; //state the platform is in

    //colors
    private MeshRenderer meshRenderer;
    private Color idleColor = new Color(1, 0, 1, 1); //purple
    private Color activeColor = new Color(0, 1, 0, 1); //green
    private Color returnColor = new Color(1, 1, 0, 1); //yellow

    [SerializeField] private MovingPlatform alternateDimensionPlatform;

    //what state the platform is in
    private enum PlatformState{
        idle,
        alert,
        activated,
        reset
    }

    // Start is called before the first frame update
    void Start()
    {
        platformState = PlatformState.idle;
        distance = Vector3.Distance(pointA, pointB);
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        transform.position = pointA;


        meshRenderer.material.SetColor("_EmissionColor", idleColor);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 old = transform.position; //records the position prior to transformation

        
        if (platformState == PlatformState.alert)
        {
            if(alertTimer == 0)
            {
                platformState = PlatformState.activated;
            }

            alertTimer = Mathf.Max(0, alertTimer - Time.deltaTime);
        }

        if (platformState == PlatformState.activated)
        {
            if(timer == 1)
            {
                platformState = PlatformState.reset;
                meshRenderer.material.SetColor("_EmissionColor", returnColor);
            }

            transform.position = Vector3.Lerp(pointA, pointB, timer);
            timer = Mathf.Min(1, timer + speed * Time.deltaTime / distance);       
        }

        if(platformState == PlatformState.reset)
        {
            if(timer == 0)
            {
                platformState = PlatformState.idle;
                meshRenderer.material.SetColor("_EmissionColor", idleColor);
            }

            transform.position = Vector3.Lerp(pointA, pointB, timer);
            timer = Mathf.Max(0, timer - returnSpeed * Time.deltaTime / distance); 
        }

        offset = transform.position - old;

        if (riding)
        {
            ridingObject.position += offset;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(alternateDimensionPlatform!= null)
                ActivateAlternate(alternateDimensionPlatform);

            riding = true;
            ridingObject = collision.gameObject.transform;

            if(platformState == PlatformState.idle)
            {
                platformState = PlatformState.alert;
                alertTimer = 0.25f;
                meshRenderer.material.SetColor("_EmissionColor", activeColor);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Vector2 newOffset = new Vector2(offset.x, offset.z);
            newOffset.Normalize();

            if (platformState == PlatformState.activated)
            {
                collision.gameObject.GetComponent<FirstPersonMovement>().momentumSpeedX = speed*newOffset.x;
                collision.gameObject.GetComponent<FirstPersonMovement>().momentumSpeedZ = speed*newOffset.y;//momentumMultiplier * offset.z / Time.deltaTime;
                collision.gameObject.GetComponent<FirstPersonMovement>().Momentum(1.5f);
            }

            if (platformState == PlatformState.reset)
            {
                collision.gameObject.GetComponent<FirstPersonMovement>().momentumSpeedX = returnSpeed*newOffset.x;
                collision.gameObject.GetComponent<FirstPersonMovement>().momentumSpeedZ = returnSpeed*newOffset.y;
                collision.gameObject.GetComponent<FirstPersonMovement>().Momentum(1f);
            }

            print(collision.gameObject.GetComponent<Rigidbody>().velocity);

            riding = false;
            ridingObject = null;
        }
    }

    public void Reinstantiate()
    {
        Start();
        alertTimer = 0f;
        timer = 0;
    }

    public void ActivateAlternate(MovingPlatform platform)
    {

        if (platform.platformState == PlatformState.idle)
        {
            platform.platformState = PlatformState.alert;
            platform.alertTimer = 0.25f;
            platform.meshRenderer.material.SetColor("_EmissionColor", activeColor);
        }
    }
}
