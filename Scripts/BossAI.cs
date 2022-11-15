using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public int inhibitorCount;
    public float health;
    private NavMeshAgent navMesh;

    public float stunTimer;
    private BossState state;



    enum BossState
    {
        idle,
        chase,
        dash,
        swipe,
        stunned,
        spiderWalk,
        attack,
        dying
    }


    // Start is called before the first frame update
    void Start()
    {
        health = 10000;
        state = BossState.idle;

        stunTimer = 20f;
        navMesh = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }
}
