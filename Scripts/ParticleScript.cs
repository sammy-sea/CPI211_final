using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    [SerializeField]GameObject Emit1;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("fuck");
           ParticleSystem ps = GameObject.Find("GunParticles").GetComponent<ParticleSystem>();
 ps.Play();
        }
    }
}
