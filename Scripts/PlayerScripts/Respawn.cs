using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] private float respawnX = -15f;
    [SerializeField] private float respawnY = 2f;
    [SerializeField] private float respawnZ = 0f;
    public SectionSaveLoad currentSection;
    private PlayerStats playerStats;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(respawnX, respawnY, respawnZ);
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -15)
        {
            InitiateRespawn();
        }

        if(playerStats.health == 0)
        {
            InitiateRespawn();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        

        if(other.gameObject.tag.Equals("Respawn") == true)
        {
            respawnX = transform.position.x;
            respawnY = transform.position.y + 2f;
            respawnZ = transform.position.z;

            currentSection = other.gameObject.GetComponent<SectionSaveLoad>();
            playerStats.health = 200f;


        }

        if (other.gameObject.tag.Equals("Hazard") == true)
        {
            transform.position = new Vector3(respawnX, respawnY, respawnZ);
        }
    }

    void InitiateRespawn()
    {
        currentSection.Load();
        playerStats.Start();
        transform.position = new Vector3(respawnX, respawnY, respawnZ);
    }
}
