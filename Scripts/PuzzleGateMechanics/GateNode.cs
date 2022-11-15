using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateNode : MonoBehaviour
{
    public bool activated;
    private MeshRenderer meshRenderer;
    [SerializeField] public Gate gate; //This is reference to the gate that needs to be unlocked. Make sure to set the counter in the gate
    [SerializeField] public GameObject[] nodePeripherals = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        meshRenderer= GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(){
        if(!activated){
            activated = true;
            gate.nodeCounter++;
            meshRenderer.material.SetColor("_EmissionColor",new Color(0,1,0,1)); 
            
            for(int i = 0; i < nodePeripherals.Length; i++)
            {
                if(nodePeripherals[i]!= null)
                {
                    nodePeripherals[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0, 1));
                }
            }
        }
    }

    public void Reinstantiate()
    {
        activated = false;
        meshRenderer.material.SetColor("_EmissionColor", new Color(1,0,0,1));

        for (int i = 0; i < nodePeripherals.Length; i++)
        {
            if (nodePeripherals[i] != null)
            {
                nodePeripherals[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1,0, 0, 1));
            }
        }
    }
}
