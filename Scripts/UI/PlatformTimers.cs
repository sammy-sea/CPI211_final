using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformTimers : MonoBehaviour
{
    private TextMeshProUGUI platformTimers;
    private Platform listPlatform;
    [SerializeField] public PlayerStats playerStats;


    // Start is called before the first frame update
    void Start()
    {
        platformTimers = GetComponent<TextMeshProUGUI>();
        platformTimers.text = "";
        //platformArray = new ArrayList(); //no longer used, the arraylist in the player's stats is now used instead.
    }

    // Update is called once per frame
    void Update()
    {
        platformTimers.text = ""; //Resets the text every updates

        if (playerStats.platformArray != null)
        {
            for (int i = 0; i < playerStats.platformArray.Count; i++)
            {
                if (playerStats.platformArray[i] != null)
                { //checks to see if the index is empty or not
                    listPlatform = (Platform)playerStats.platformArray[i]; //casts the element from object to Platform

                    if (listPlatform.timer > 0f) //displays the timers

                        if (!listPlatform.collapsing || listPlatform.previous.timer > 0)
                        {
                            platformTimers.text += "Collapsing in " + TimeFormat(listPlatform.timer).ToString() + " (Stable)\n"; //if the platform before it is still active, then it is marked as stable
                        }
                        else
                        {
                            platformTimers.text += "Collapsing in " + TimeFormat(listPlatform.timer).ToString() + "\n";
                        }

                    if (listPlatform.timer == 0f) //removes the elements when its done
                        playerStats.platformArray.RemoveAt(i);
                }
            }
        }

        print(playerStats.platformArray.Count); //debug purposes
    }

    //purely for formating a timer
    public static string TimeFormat(float num){
        
        string secondTimer;

        if(num%60 < 10){
            secondTimer = "0" + Mathf.Floor(num%60).ToString();
        } else{
            secondTimer = Mathf.Floor(num%60).ToString();
        }

        string result = Mathf.Floor(num/60) + ":" + secondTimer;
        return result;
        
    }
}
