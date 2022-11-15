using UnityEngine;


[System.Serializable]
public class PlatformData
{
    public bool collapsing;
    public bool fall;
    public float[] position = new float[3];
    public float[] rotation = new float[4];

    public PlatformData(Platform platform)
    {
        collapsing = platform.collapsing;
        fall = platform.fall;

        Vector3 pos = platform.transform.position;
        Quaternion rot = platform.transform.rotation;

        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;

        rotation[0] = rot.x;
        rotation[1] = rot.y;
        rotation[2] = rot.z;
        rotation[3] = rot.w;
    }

}
