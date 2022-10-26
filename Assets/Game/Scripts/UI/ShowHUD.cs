using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHUD : MonoBehaviour
{
    public GameObject playerHUD;

    private void OnEnable()
    {
        GameManager.stateChange += ToggleHUD;
     
    }

    private void OnDisable()
    {
        GameManager.stateChange -= ToggleHUD;
      
    }

    public void ToggleHUD()
    {
        if (GameManager.instance.currentState != GameManager.GameStates.LevelActive)
        {
            HideHUD();
        } else if (GameManager.instance.currentState == GameManager.GameStates.LevelActive)
        {
            ShowTheHUD();
        }
    }

    public void HideHUD()
    {
        playerHUD.SetActive(false);
    }

    public void ShowTheHUD()
    {
        playerHUD.SetActive(true);
    }
}
