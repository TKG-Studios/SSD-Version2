using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIExtension : EnemyAI
{
    public override void OnEnable()
    {
        
        EnemyWaveTrigger.waveTrigger += startWave;
    }

    private void OnDisable()
    {
        EnemyWaveTrigger.waveTrigger -= startWave;
    }

    public override void Update()
    {
        if (!isDead && enableAI)
        {
            if (ActiveAIStates.Contains(enemyState) && targetSpotted)
            {
               AI();

            }
            else

            {
                //Look4Target();
            }
        }
        UpdateSpriteSorting();
    }

    public void startWave()
    {
        enableAI = true;
        SetTarget2Player();
        Look4Target();
    }

}
