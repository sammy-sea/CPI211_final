using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] string playerTag;
    [SerializeField] float bounceForce;
    [SerializeField] PlayerStats playerStats;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals(playerTag))
        {
            Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>(); ;
            otherRB.AddForce(0, bounceForce, 0, ForceMode.Impulse);
            playerStats = collision.gameObject.GetComponent<PlayerStats>();
        }
    }
}