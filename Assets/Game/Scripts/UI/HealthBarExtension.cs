using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarExtension : HealthBar
{
    public static HealthBarExtension instance;


    private void Start()
    {
        instance = this;   
    }
    public void UpdatePlayerName(string name)
    {
        if (isPlayer)
        {
            nameField.text = name;
        }
    }
    

}
