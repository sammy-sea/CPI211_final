using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Platform lastPlatformTouched;
    public ArrayList platformArray;
    public float health;
    public int ammo;
    

 

    // Start is called before the first frame update
    public void Start()
    {
        platformArray = new ArrayList();
        lastPlatformTouched = new Platform();
        lastPlatformTouched.timer = -5f;
        lastPlatformTouched.collapsing = true;
        health = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        print(health);
    }

    public void InsertPlatform()
    {
        platformArray.Insert(0,lastPlatformTouched);
    }


}
