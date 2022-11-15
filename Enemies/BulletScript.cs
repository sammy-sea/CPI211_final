using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletLife;
    [SerializeField] private float bulletSpeed;
    private Transform parent;
    private float bulletTimeAlive;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * bulletSpeed);
        bulletTimeAlive = 0;
    }

    void Update() {
        bulletTimeAlive += Time.deltaTime;
        //print(bulletTimeAlive);
        if (bulletTimeAlive >= bulletLife)
            Destroy(transform.parent.gameObject);
    }
    void OnTriggerEnter(Collider other) {
        Destroy(transform.parent.gameObject);    
    }
}
