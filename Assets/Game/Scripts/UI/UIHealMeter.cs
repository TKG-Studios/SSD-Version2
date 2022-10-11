using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHealMeter : MonoBehaviour
{
    public Image healMeterFill;
    public bool isBarFull;
    public int healAmount;
    private float healIncrement = 10;

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
        isBarFull = false;
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void UpdateHealBar()
    {
        healAmount = GameObject.FindWithTag("Player").GetComponent<HealthSystem>().MaxHp / 10;
        healMeterFill.fillAmount += healIncrement / 100;
        if (healMeterFill.fillAmount == 1)
        {
            isBarFull = true;
        }
        else
        {
            isBarFull = false;

        }
    }
}
