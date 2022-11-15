using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBuildPlatform : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private Color invalidColor = new Color(0.5f,0.25f,0.25f,1f);
    private Color validColor = new Color(0.25f,0.5f,0.25f,1f);
    public bool valid = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.OverlapBox(gameObject.transform.position, new Vector3(2.5f,0.5f,2.5f), Quaternion.identity).Length <= 1)
        {
            meshRenderer.material.SetColor("_EmissionColor", validColor);
            valid = true;
        }
        else
        {
            meshRenderer.material.SetColor("_EmissionColor", invalidColor);
            valid = false;
        }

    }

}
