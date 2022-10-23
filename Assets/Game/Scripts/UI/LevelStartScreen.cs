using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelStartScreen : MonoBehaviour
{
    public Text CurrentLevelText;


    private void OnEnable()
    {
        GameManager.stateChange += LevelText;
    }

    private void OnDisable()
    {
        GameManager.stateChange -= LevelText;
    }
    void Start()
    {
        HideText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelText()
    {
        if (GameManager.instance.currentState == GameManager.GameStates.LevelStart)
        {
            ShowText();
            Invoke("StartLevel", 2f);
        }

        if (GameManager.instance.currentState == GameManager.GameStates.LevelActive)
        {
            HideText();
        }

    }

    public void HideText()
    {
        CurrentLevelText.gameObject.SetActive(false);
    }

    public void ShowText()
    {
        CurrentLevelText.gameObject.SetActive(true);
    }

    public void StartLevel()
    {
        GameManager.instance.changeState(GameManager.GameStates.LevelActive);
        // Create HUD
        if (!GameObject.FindGameObjectWithTag("HUD")) GameObject.Instantiate(Resources.Load("PlayerHUD"), Vector3.zero, Quaternion.identity);
    }

}
