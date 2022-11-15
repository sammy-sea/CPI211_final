using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionSaveLoad : MonoBehaviour
{
    [SerializeField] public Platform[] platformSections; //stores platform sections
    private GameObject[] objectArray; //stores the objects in a respective section
    private Vector3[] positionArray; //stores the object positions
    private Quaternion[] rotationArray; //stores the object rotations
    //[SerializeField] public PlatformTimers platformTimers; //old code, kept here in case something goes wrong

    // Start is called before the first frame update
    void Start()
    {
        objectArray = Unpack(platformSections);

        if (objectArray != null)
        {
            positionArray = new Vector3[objectArray.Length];
            rotationArray = new Quaternion[objectArray.Length]; 


            for(int i = 0; i < objectArray.Length; i++)
            {
                if(objectArray[i] != null)
                {
                    positionArray[i] = objectArray[i].transform.position; //stores the objects in the obj array's positions
                    rotationArray[i] = objectArray[i].transform.rotation; //stores the objects' rotations
                }
            }
        }
    }

    public void Save()
    {

    }

    public void Load()
    {
        if (objectArray != null) //checks if its empty
        {
            for (int i = 0; i < objectArray.Length; i++)
            {
                if (objectArray[i] != null) //checks if the elemtent is empty
                {
                    objectArray[i].SetActive(true);
                    
                    //this if else statement will set any inactive child objects active 
                    if (objectArray[i].transform.childCount > 0) {
                        
                        objectArray[i].transform.GetChild(0).gameObject.SetActive(true);  
                    }

                    Destroy(objectArray[i].GetComponent<Rigidbody>()); //removes the physics component that was added in the collapse function

                    objectArray[i].transform.position = positionArray[i]; //sets position of the object
                    objectArray[i].transform.rotation = rotationArray[i]; //sets rotation of the object


                    //yandere dev type code (aka absolutely terrible) but it checks what type of object the thing is and issues a reinstantiate call
                    if (objectArray[i].GetComponentInChildren<GateNode>() != null)
                    {
                        objectArray[i].GetComponentInChildren<GateNode>().Reinstantiate();
                    }  

                    if (objectArray[i].GetComponent<Platform>() != null)
                    {
                        objectArray[i].GetComponent<Platform>().Reinstantiate();
                    }

                    if (objectArray[i].GetComponent<Gate>() != null)
                    {
                        objectArray[i].GetComponent<Gate>().Reinstantiate();
                    }

                    if (objectArray[i].GetComponentInChildren<MovingPlatform>() != null)
                    {
                        objectArray[i].GetComponentInChildren<MovingPlatform>().Reinstantiate();
                    }

                    if (objectArray[i].GetComponent<TimeExtender>() != null)
                    {
                        objectArray[i].GetComponent<TimeExtender>().Reinstantiate();
                    }

                }
            }
        }

        //platformTimers.playerStats.platformArray.Clear(); //old code that reset the timer. now done directly through the player scripts
    }

    public GameObject[] Unpack(Platform[] platformSections)
    {
        ArrayList objectList = new ArrayList(); 
        GameObject[] tempObjectList;
        
        if(platformSections != null)
        {
            for(int i = 0; i < platformSections.Length; i++)
            {
                if(platformSections[i] != null)
                {
                    objectList.Add(platformSections[i].gameObject);

                    for (int j = 0; j < platformSections[i].linkedObjects.Length; j++)
                    {
                        objectList.Add(platformSections[i].linkedObjects[j]);
                    }
                }
            }
        }

        tempObjectList = new GameObject[objectList.Count];

        for(int i = 0; i < tempObjectList.Length; i++)
        {
            tempObjectList[i] = (GameObject) objectList[i];
        }

        return tempObjectList;
    }
}
