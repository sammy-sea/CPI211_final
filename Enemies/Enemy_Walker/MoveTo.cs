using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform goal;
    private GameObject playerTarget;
    private NavMeshAgent agent;
    [SerializeField] private float resetTimer;
    private float timer;
    private float distance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        if (playerTarget != null) //if player object exists, move to player
        {
            goal = playerTarget.transform;
            agent.destination = goal.position;
        }
    }

    void LateUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= resetTimer) //recalculate path after short delay
        {
            //print(distanceToPlayer());
            if (distanceToPlayer() > 4)
            {
                //print("Recalculating path");
                findPath(); //repath
            }
            else
            {
                //print("Target reached");
                timer = 0; //dont repath
            }
        }
    }

    // length of vector between enemy and player
    float distanceToPlayer()
    {
        Vector3 distanceVector = playerTarget.transform.position - transform.position;
        float distance = distanceVector.magnitude;
        return distance;
    }
    /// recaluclates path to player if player exists
    void findPath()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");   
        if (playerTarget != null)
        {
            goal = playerTarget.transform;
            agent.destination = goal.position;
        }    
    }


}
