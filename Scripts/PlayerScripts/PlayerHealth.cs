using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerStats playerStats;

    public float recoveryTimer;
    public float damagedTimer;
    public float stasisTimer;
    public PlayerHealthState state;

    public enum PlayerHealthState
    {
        normal,
        damaged,
        stasis,
        recovering
    }


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        state = PlayerHealthState.normal;

    }

    // Update is called once per frame
    void Update()
    {
        if (state == PlayerHealthState.damaged)
        {
            if (damagedTimer == 0)
            {
                stasisTimer = 9.5f;
                state = PlayerHealthState.stasis;
            }
            damagedTimer = Mathf.Max(0, damagedTimer - Time.deltaTime);

        }   else if(state == PlayerHealthState.stasis)
        {
            if(stasisTimer == 0f)
            {
                state = PlayerHealthState.recovering;
            }

            stasisTimer = Mathf.Max(0, stasisTimer - Time.deltaTime);

        }   else if(state == PlayerHealthState.recovering)
        {
            if (playerStats.health == 200f)
            {
                state = PlayerHealthState.normal;
            }

            playerStats.health = Mathf.Min(200, playerStats.health + 100 * Time.deltaTime);
        }

        playerStats.health = Mathf.Max(0, playerStats.health);
        playerStats.health = Mathf.Min(200, playerStats.health);

    }

    public void Hit(float damage)
    {
        if (state != PlayerHealthState.damaged)
        {
            playerStats.health -= damage;
            damagedTimer = 0.5f;

            state = PlayerHealthState.damaged;
        }
    }

}
