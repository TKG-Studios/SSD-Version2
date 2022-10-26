using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpecialMeter : MonoBehaviour
{
    public static UISpecialMeter instance;

    public Image specialMeterFill;
    public bool isBarFull;
    private float specialIncrement = 10;

    private void OnEnable()
    {
        PlayerCombatExtension.SpecialBarEvent += UpdateSpecialBar;

    }

    private void OnDisable()
    {
        PlayerCombatExtension.SpecialBarEvent -= UpdateSpecialBar;

    }

    void Start()
    {
        instance = this;
        isBarFull = false;

    }

    // Update is called once per frame

    private void Update()
    {

        if (specialMeterFill.fillAmount == 1)
        {
            isBarFull = true;

        }
        else
        {
            isBarFull = false;

        }
    }
    public void UpdateSpecialBar()
    {
        specialMeterFill.fillAmount += specialIncrement / 200;

    }
}
