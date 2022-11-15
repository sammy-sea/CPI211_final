using UnityEngine;

[System.Serializable]
public class GateNodeData
{
    public int gateId;
    public int peripheralId1, peripheralId2;
    public float[] position = new float[3];
    public float[] rotation = new float[4];

    public GateNodeData(GateNode gateNode)
    {
        gateId = gateNode.gate.GetInstanceID();
        peripheralId1 = gateNode.nodePeripherals[0].GetInstanceID();
        peripheralId2 = gateNode.nodePeripherals[1].GetInstanceID();

        Vector3 pos = gateNode.transform.position;
        Quaternion rot = gateNode.transform.rotation;

        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;

        rotation[0] = rot.x;
        rotation[1] = rot.y;
        rotation[2] = rot.z;
        rotation[3] = rot.w;
    }

}
