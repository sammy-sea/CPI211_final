using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    [SerializeField] private Vector3 pointA = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 pointB = new Vector3(0, 0, 0);
    //[SerializeField] private float length = 1;

    [SerializeField] private bool move = false;
    //[SerializeField] private bool spin = false;
    [SerializeField] private bool spinX = false;
    [SerializeField] private bool spinZ = false;
    public float damage;

    private float lerpTimer = 0f;
    private float spinLerpTimer = 0f;
    private float colorTimer = 0f;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        damage = 50;
        //transform.localScale = new Vector3(0.3f,length,0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(pointA, pointB, lerpTimer);
        }

        if (spinX)
        {
            transform.Rotate(spinLerpTimer,0,0);
        }

        if (spinZ)
        {
            transform.Rotate(0, 0, spinLerpTimer);
        }

        if (lerpTimer > 1f || lerpTimer < 0f)
        {
            speed *= -1;
        }

        if (spinX || spinZ)
        {
            spinLerpTimer = 90 * Time.deltaTime;
        }



        lerpTimer += speed * Time.deltaTime;
        GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.5f + 0.25f * Mathf.Sin(Mathf.PI * colorTimer / 0.25f), 0, 0, 1));
        colorTimer = (colorTimer + Time.deltaTime) % 360;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().Hit(damage);
        }
    }
}
