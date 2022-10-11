using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIHealMeter : MonoBehaviour
{
    public static UIHealMeter instance;

    public Image healMeterFill;
    public bool isBarFull;
    private float healIncrement = 10;

    public int healingBarDivisor;
    private void OnEnable()
    {
        PlayerCombatExtension.HealBarEvent += UpdateHealBar;

    }

    private void OnDisable()
    {
        PlayerCombatExtension.HealBarEvent -= UpdateHealBar;

    }

    void Start()
    {
        instance = this;
        isBarFull = false;
        
    }

    // Update is called once per frame

    private void Update()
    {
        healingBarDivisor = 5;

       if (healMeterFill.fillAmount == 1)
        {
            isBarFull = true;

        }
        else
        {
            isBarFull = false;

        }
    }
    public void UpdateHealBar()
    { 
        healMeterFill.fillAmount += healIncrement / 200;
     
    }

}
