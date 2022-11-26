using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTrigger : MonoBehaviour
{
    public delegate void WaveTrigger();
    public static event WaveTrigger waveTrigger;


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (waveTrigger != null) waveTrigger();
        }
    }

  
}
