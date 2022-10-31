using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeliverySystem : MonoBehaviour
{
    public Text Deliveries;
   

    private void OnEnable()
    {
        DeliverySystem.deliveryEvent += UpdateDeliveryUI;
    }

    private void OnDisable()
    {
        DeliverySystem.deliveryEvent -= UpdateDeliveryUI;
    }

    private void Start()
    {
        UpdateDeliveryUI(LevelManager.instance.customers);
    }

    public void UpdateDeliveryUI(int deliveries)
    {
       
        Deliveries.text = "x" + deliveries;
    }
}
