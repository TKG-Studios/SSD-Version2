using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeliverySystem : MonoBehaviour
{
    public delegate void DeliveryEvent(int deliveries);
    public static event DeliveryEvent deliveryEvent;

    internal int deliveriesLeft;


    private void Start()
    {
        deliveriesLeft = DeliveriesPerLevel.Instance.levelsDeliveries;
      
    }

    public void PlusDeliveries(int deliveriesAdded)
    {
       
        deliveriesLeft += deliveriesAdded;
        if (deliveryEvent != null) deliveryEvent(deliveriesLeft);

    }

    public void MinusDeliveries(int deliveriesFinished)
    {
       
        deliveriesLeft -= deliveriesFinished;
        if (deliveryEvent != null) deliveryEvent(deliveriesLeft);

    }


}
