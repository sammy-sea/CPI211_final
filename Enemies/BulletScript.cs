using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletLife;
    [SerializeField] private float bulletSpeed;
    [SerializeField] public float bulletDamage;
    private Transform parent;
    private float bulletTimeAlive;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * bulletSpeed);
        bulletTimeAlive = 0;
        bulletDamage = 50f;
    }

    void Update() {
        bulletTimeAlive += Time.deltaTime;
        if (bulletTimeAlive >= bulletLife) //if bullet exceeds its lifetime, destroy itself
            Destroy(transform.parent.gameObject);
    }

    void OnTriggerEnter(Collider other) {
        GameObject targetObject = other.transform.gameObject;
        Destroy(transform.parent.gameObject); //destroy self on collision    
        if (other.transform.gameObject.tag == "Player")
        {
            print("hit player for " + bulletDamage + " damage");
            targetObject.GetComponent<PlayerHealth>().Hit(bulletDamage);
        }
        
    }
}
