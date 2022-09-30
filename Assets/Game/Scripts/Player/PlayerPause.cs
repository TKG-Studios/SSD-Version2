using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isGamePaused;


    private void Start()
    {
        isGamePaused = false;
    }
    private void OnEnable()
    {
        InputManager.onPauseInput += PauseGame;
    }

    private void OnDisable()
    {
        InputManager.onPauseInput -= PauseGame;
    }

    public void PauseGame(string action)
    {
        if (action == "Pause" && isGamePaused == false)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
            PauseScreen.instance.pauseScreen.SetActive(true);
        }

        else if (action == "Pause" && isGamePaused == true)
        {
            isGamePaused = false;
            Time.timeScale = 1;
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<PlayerCombat>().enabled = true;
            PauseScreen.instance.pauseScreen.SetActive(false);
        }
    }
}
