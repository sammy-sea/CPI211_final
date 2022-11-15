using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] public float health;
    
    void Update()
    {
        if (health <= 0)
        {
            death();
        }
    }

    public void hit(float damage)
    {
        health -= damage;
    }

    public void death()
    {
        Destroy(gameObject);
    }
}
