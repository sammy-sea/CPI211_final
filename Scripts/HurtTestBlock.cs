using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTestBlock : MonoBehaviour
{
    public float damage = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Hit(damage);
        }
    }
}
