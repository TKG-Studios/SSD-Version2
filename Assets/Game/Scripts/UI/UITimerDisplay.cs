using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimerDisplay : MonoBehaviour
{
    private EnemyWaveSystem enemyWaves;
    public Text timerText;
    private int currentTimer;

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
}
