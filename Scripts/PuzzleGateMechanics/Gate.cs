using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private float lerpTimer;
    public int nodeCounter;
    [SerializeField] public int unlockRequirement;
    Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        lerpTimer = 1f;
        nodeCounter = 0;
        currentScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(nodeCounter >= unlockRequirement){
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(0,currentScale.y,currentScale.z),currentScale,lerpTimer);
            lerpTimer = Mathf.Max(0, (lerpTimer - Time.deltaTime));
        }

        if(lerpTimer == 0){
            gameObject.SetActive(false);
        }
    }

    public void Reinstantiate()
    {
        nodeCounter = 0;
        lerpTimer = 1f;
        gameObject.transform.localScale = currentScale;
    }
}
