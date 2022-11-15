using UnityEngine;

[System.Serializable]
public class GateData
{
    public int unlockRequirement;
    public float[] position = new float[3];
    public float[] rotation = new float[4];

    public GateData(Gate gate)
    {
        unlockRequirement = gate.unlockRequirement;

        Vector3 pos = gate.transform.position;
        Quaternion rot = gate.transform.rotation;

        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;

        rotation[0] = rot.x;
        rotation[1] = rot.y;
        rotation[2] = rot.z;
        rotation[3] = rot.w;
    }
}
