using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarExtension : HealthBar
{
    public static HealthBarExtension instance;
    public Text playerLevelText;
    private int currentLevel;

    new void OnEnable()
    {
        base.OnEnable();
        LevelUpSystem.onLevelChange += PlayerLevel;

    }

    new void OnDisable()
    {
        base.OnDisable();
        LevelUpSystem.onLevelChange -= PlayerLevel;
    }

    private void Start()
    {
        instance = this;
        playerLevelText.text = "Lv." + PlayerLevel(1).ToString();
    }
    public void UpdatePlayerName(string name)
    {
        if (isPlayer)
        {
            nameField.text = name;
        }
    }

    public int PlayerLevel(int level)
    {
        currentLevel = level;
        if (isPlayer)
        {
            playerLevelText.text = "Lv." + currentLevel.ToString();
        }
        return level;
    }

    
    }
