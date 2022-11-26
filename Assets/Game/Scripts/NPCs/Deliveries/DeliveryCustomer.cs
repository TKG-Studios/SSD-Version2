using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCustomer : MonoBehaviour
{
    public string CustomerName = ""; //Name of the customer being delivered to


    public delegate void DeliveryMade(GameObject g);
    public static event DeliveryMade onDeliveryMade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DeliverySystem>())
        {
          DeliverySystem getdeliveries = collision.GetComponent<DeliverySystem>();
            if (getdeliveries.deliveriesLeft > 0)
            {
                getdeliveries.MinusDeliveries(1);
                if (onDeliveryMade != null) onDeliveryMade(this.gameObject);
                this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green; //Temporarily Turn The Delivered Persons Sprite Green
              
                Destroy(this);
            }
        }
    
    }


}
