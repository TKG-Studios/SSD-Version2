using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveriesPerLevel : MonoBehaviour
{

    public static DeliveriesPerLevel Instance { get; private set; }
    public int levelsDeliveries;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
