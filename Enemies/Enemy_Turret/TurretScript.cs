using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    public Transform targetPlayer;
    [SerializeField] private float reloadTime;
    [SerializeField] private GameObject bullet;
    private Transform bulletSpawn;
    private GameObject firedBullet;
    private float cooldown;
    private Vector3 playerDirection;

    void Start()
    {
        cooldown = reloadTime;
        targetPlayer = GameObject.FindWithTag("Player").transform;
        bulletSpawn = transform.Find("BulletSpawn");    
    }

    void Update()
    {
        if (targetPlayer)
        {
            Quaternion newRotation = Quaternion.LookRotation(targetPlayer.position - transform.position, Vector3.up); // use calculated rotation to rotate to player
            transform.rotation = newRotation;
        }
    }

    void FixedUpdate() {
        if (cooldown < reloadTime) // cooldown before fire
            cooldown++;
        else if (cooldown >= reloadTime) // fire bullet when cooldown is over
        {
            FireBullet();
            cooldown = 0;
        }
    }

    /// creates a bullet and fires it at the player
    void FireBullet()
    {
        firedBullet = Instantiate(bullet, bulletSpawn.position, transform.rotation);
    }
    
}
