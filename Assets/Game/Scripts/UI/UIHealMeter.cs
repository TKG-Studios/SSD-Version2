using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHealMeter : MonoBehaviour
{
    public static UIHealMeter Instance;
    public Image healMeterFill;

    public bool isBarFull;
    public int healAmount;


    void Start()
    {
        Instance = this;
        isBarFull = false;
       
       
    }

    // Update is called once per frame
    void Update()
    {
        healAmount = GameObject.FindWithTag("Player").GetComponent<HealthSystem>().MaxHp / 10;

        if (healMeterFill.fillAmount == 1)
        {
            isBarFull = true;
        } else
        {
            isBarFull = false;
           
        }
    }
}
