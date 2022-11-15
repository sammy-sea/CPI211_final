using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
   public float ScrollX = 5.01f;
   public float ScrollY = 0.0f;
    // Update is called once per frame
    void Update()
    {
        float OffsetX = Time.time /10;
        float OffsetY = Time.time * ScrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX,OffsetY);
    }
}
