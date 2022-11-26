using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCustomer : MonoBehaviour
{
    public string CustomerName = ""; //Name of the customer being delivered to
    public int tipGiven;


    public delegate void DeliveryMade(GameObject g);
    public static event DeliveryMade onDeliveryMade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DeliverySystem>() != null)
        {

          DeliverySystem getdeliveries = collision.GetComponent<DeliverySystem>();
          CurrencySystem payment = collision.GetComponent<CurrencySystem>();
            if (getdeliveries.deliveriesLeft > 0)
            {
                getdeliveries.MinusDeliveries(1);
                payment.AddCurrency(tipGiven);
                if (onDeliveryMade != null) onDeliveryMade(this.gameObject);



                this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green; //Temporarily Turn The Delivered Persons Sprite Green
              
                Destroy(this);
            }
        }
    
    }


}
