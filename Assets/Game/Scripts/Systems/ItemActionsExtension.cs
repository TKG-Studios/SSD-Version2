using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class ItemActionsExtension : ItemInteractable
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GiveCoinsToPlayer();
        UnityEngine.Debug.Log("Hello");
    }
}
