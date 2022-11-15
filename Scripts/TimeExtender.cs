using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeExtender : MonoBehaviour
{
    public bool activated;
    private MeshRenderer meshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.SetColor("_EmissionColor", new Color(1, 0, 0, 1));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!activated)
            {
                activated = true;
                meshRenderer.material.SetColor("_EmissionColor", new Color(0, 1, 0, 1));

                if(gameObject.transform.childCount > 0)
                {
                    for(int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0, 1));
                    }
                }


                collision.gameObject.GetComponent<PlayerStats>().lastPlatformTouched.timer += 5;
            }
        }
    }

    public void Activate()
    {
        if (!activated)
        {
            activated = true;
            meshRenderer.material.SetColor("_EmissionColor", new Color(0, 1, 0, 1));

            if (gameObject.transform.childCount > 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 1, 0, 1));
                }
            }

        }
    }

    public void Reinstantiate()
    {
        activated = false;
        meshRenderer.material.SetColor("_EmissionColor", new Color(1, 0, 0, 1));

        if (gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1,0, 0, 1));
            }
        }

    }

}
