using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{

    public static PauseScreen instance;
    public GameObject pauseScreen;




    //Expose Level To Player
    public Text level;
    public Text CurrentXP;
    public Text XPToNext;

    private void Start()
    {
        instance = this;
        pauseScreen.SetActive(false);
    
    }

 

    private void Update()
    {
        level.text = "Level: " + LevelUpSystem.instance.currentLevel;
        CurrentXP.text = "EXP: " + LevelUpSystem.instance.currentXP.ToString("F0");
        XPToNext.text = "To Next: " + LevelUpSystem.instance.neededXp.ToString("F0");
    }


}
