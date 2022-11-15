using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLight : MonoBehaviour
{
    private MeshRenderer lightChange;
    private Platform platform;

    // Start is called before the first frame update
    void Start()
    {
        lightChange = GetComponent<MeshRenderer>();
        platform = GetComponent<Platform>();

        //lightChange.material.SetColor("_EmissionColor",new Color(1,0.1f,0,1));
    }

    // Update is called once per frame
    void Update()
    {
        lightChange.material.SetColor("_EmissionColor",new Color(Mathf.Lerp(1,0,platform.timer/10f), Mathf.Lerp(0.5f,1,platform.timer/10f), Mathf.Lerp(0,1,platform.timer/10f), 1));


        //this sets the attached objects the same color as the paltform, if and only if they are not mechanic based
        if (platform.linkedObjects != null)
        {
            for(int i = 0; i < platform.linkedObjects.Length; i++)
            {
                if (platform.linkedObjects[i] != null)
                {
                    if (platform.linkedObjects[i].GetComponent<MeshRenderer>() != null)
                    {
                        if (!platform.linkedObjects[i].transform.tag.Equals("Mechanic"))
                        {
                            platform.linkedObjects[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(Mathf.Lerp(1, 0, platform.timer / 10f), Mathf.Lerp(0.5f, 1, platform.timer / 10f), Mathf.Lerp(0, 1, platform.timer / 10f), 1));
                        }
                    }
                }
            }
        }
    }
}
