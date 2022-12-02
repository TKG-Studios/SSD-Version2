using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimerDisplay : MonoBehaviour
{
    private EnemyWaveSystem enemyWaves;
    public Text timerText;
    private float currentTimer;
    public int timerMultiplier;

    public delegate void TimerGameOver();
    public static event TimerGameOver onTimerGameOver;
    private void OnEnable()
    {
        EnemyWaveSystem.onTimerReset += SetTimer;
       
    }

    private void OnDisable()
    {
        EnemyWaveSystem.onTimerReset -= SetTimer;
    }

    void Start()
    {
        enemyWaves = FindObjectOfType<EnemyWaveSystem>();
        currentTimer = enemyWaves.EnemyWaves[enemyWaves.currentWave].waveTimer;
        timerText.text = currentTimer.ToString("F0");
    }


    public void SetTimer()
    {
        currentTimer = enemyWaves.EnemyWaves[enemyWaves.currentWave].waveTimer;
        timerText.text = currentTimer.ToString("F0");
    }

    private void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameStates.LevelActive)
        {
            currentTimer -= Time.deltaTime / timerMultiplier;
            timerText.text = currentTimer.ToString("F0");
        }

        if (currentTimer <= 0)
        {
            currentTimer = 0;
            if (onTimerGameOver != null) { onTimerGameOver(); }
      
        }

      
    }
}
