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
        UpdateDeliveryUI(DeliveriesPerLevel.Instance.levelsDeliveries);
    }

    public void UpdateDeliveryUI(int deliveries)
    {
       
        Deliveries.text = "x" + deliveries;
    }
}
