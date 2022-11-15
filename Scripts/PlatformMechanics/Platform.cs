using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float timer;
    public bool collapsing;
    public bool fall;
    public PlayerStats playerStats;

    [SerializeField] public GameObject[] linkedObjects; //contains all the object on a puzzle platform that need to fall and collapse as well. very important
    //[SerializeField] private PlatformTimers platformTimers; //code exists in case something goes wrong

    public Platform previous { get; private set; } //allows a reference to the previous platform the player activated
    public Platform next; //allows a reference to the next platform the player lands on
    


    // Start is called before the first frame update
    void Start()
    {
        timer = 10f;
        collapsing = false;
        fall = false;
    }

    // Update is called once per frame
    void Update()
    {
        //counts down
        if(collapsing && previous.timer <= 0){
            timer = Mathf.Max(0,(timer - Time.deltaTime));
        }

        if(timer == 0 && !fall){
            fall = true;
            Collapse();
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.transform.tag.Equals("Player")){
            if(!collapsing){ //checks if the plat form has been collided with already
                playerStats = collision.gameObject.GetComponent<PlayerStats>(); //retrieves the players base information
                previous = playerStats.lastPlatformTouched; //grabs the last valid platform the player touched
                playerStats.lastPlatformTouched = this; //sets this as the last platform the player touched
                playerStats.InsertPlatform(); //adds this platform to the player's list of touched platforms

                collapsing = true; //starts the timer on the platform
                //platformTimers.platformArray.Insert(0,this); //sends the platform's information to the timer UI //old code
                print("hello");
            }
        }
    }

    void Collapse(){
        gameObject.AddComponent<Rigidbody>();


        //this part just adds a random force to each of the parts. makes it look more explody
        if (linkedObjects != null)
        {
            for (int i = 0; i < linkedObjects.Length; i++)
            {
                if(linkedObjects[i] != null){
                    linkedObjects[i].AddComponent<Rigidbody>();
                    linkedObjects[i].GetComponent<Rigidbody>().AddForce(Random.Range(-5f, 5f), Random.Range(2f, 10f), Random.Range(-5f, 5f), ForceMode.Impulse); 
                } 
            }
        }
    }

    public void Reinstantiate()
    {
        previous = null;
        next = null;

        Start();
    }
}
