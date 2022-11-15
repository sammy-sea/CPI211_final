using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] public PlayerStats playerStats;
    private Slider slider;
    private float alphaTimer;


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 200f;
        alphaTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerStats.health;

        if(playerStats.health <= 40)
        {
            slider.fillRect.GetComponent<Image>().color = new Color(1,0,0, 0.5f + 0.25f * Mathf.Sin(Mathf.PI * alphaTimer / 0.125f));
            alphaTimer = (alphaTimer + Time.deltaTime) % 360;
        }
        else
        {
            slider.fillRect.GetComponent<Image>().color = new Color(1, 1, 1, 0.75f);
        }
    }
}
