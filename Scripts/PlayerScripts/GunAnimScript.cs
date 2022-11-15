using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimScript : MonoBehaviour
{
    [SerializeField] GameObject beam; // the bullet tracer (should be childed to camera)
    [SerializeField] float beamDuration = .2f; // for easy editing
    [SerializeField] public int ammo = 6000; // not yet implemented
    private Animator animator; // animator

    //stuff for shooting
    public bool canShoot = true;
    [SerializeField] private float cooldown= 1f;
    [SerializeField] private float timer = 0f;
    [SerializeField] public float range = 20f;

    public Camera fpsCam; //camera used to rayscan
    private Target target; //this will be for enemies. Variable type can change if needed
    private GateNode gateNode; //this will be for Gate Node buttons
    private PlayerStats playerStats;
    private TimeExtender timeExtender;


 
    void Start()
    {
        beam.SetActive(false);
        animator = GetComponent<Animator>();
        fpsCam = GetComponentInParent<Camera>();
        playerStats = GetComponentInParent<PlayerStats>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)&&canShoot){
            animator.SetTrigger("ShootTrig"); // play the animation
            beam.SetActive(true); // activate the beam

                //activate the particles
            ParticleSystem ps = GameObject.Find("GunParticles").GetComponent<ParticleSystem>();  // this breaks if particle system is renamed
            ps.Play(); // do the thing
            Shoot(); //scans for the bullet hit

            ammo-=1;
            canShoot = false;
            timer = 0f; // reset timer
           


        }
        timer += Time.deltaTime;
        if(!canShoot&&ammo>0){
            if(timer>=cooldown){
            canShoot = true;
            }
        
        }
        if(timer>=beamDuration&&beam.activeSelf){
            beam.SetActive(false);
        }

        // use for walking agent.SetDestination(targeteeeee);
        //then set a boolean thing
    }

    //this method detects and hits the target object
    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            GameObject targetObject = hit.transform.gameObject;
            target = hit.transform.GetComponent<Target>(); //gets the target script from the hit object
            gateNode = hit.transform.GetComponent<GateNode>(); //gets the gate node it hit if there is one.
            timeExtender = hit.transform.GetComponent<TimeExtender>();

            if(target != null){ 
            
            }

            if(gateNode != null && !gateNode.activated){ //if the object shot has a node element, then it become activated
                gateNode.Activate();
                playerStats.lastPlatformTouched.timer += 4.0f;

            }

            if(timeExtender != null && !timeExtender.activated){ //if the object shot has a node element, then it become activated
                timeExtender.Activate();
                playerStats.lastPlatformTouched.timer += 5.0f;

            }
            
            if(targetObject.tag == "Enemy")
            {
                print("enemy hit");
                targetObject.GetComponent<EnemyStats>().hit(50f);
            }
        }
    }
}
