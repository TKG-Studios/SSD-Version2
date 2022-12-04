using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCustomer : MonoBehaviour
{
    public string CustomerName = ""; //Name of the customer being delivered to


    public delegate void DeliveryMade(GameObject g);
    public static event DeliveryMade onDeliveryMade;



    public delegate void TipReceivedEvent(string CustomerName, int tipToGive);
    public static event TipReceivedEvent tipReceivedEvent;
    public int tipToGive; // TO DO --- Adjust this based on how well the player did on the delivery!

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DeliverySystem>())
        {
          DeliverySystem getdeliveries = collision.GetComponent<DeliverySystem>();
            CurrencySystem giveCurrency = collision.GetComponent<CurrencySystem>();
            if (getdeliveries.deliveriesLeft > 0)
            {
                getdeliveries.MinusDeliveries(1);
                giveCurrency.AddCurrency(tipToGive);
                if (tipReceivedEvent != null) tipReceivedEvent(CustomerName, tipToGive);
                if (onDeliveryMade != null) onDeliveryMade(this.gameObject);
                this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green; //Temporarily Turn The Delivered Persons Sprite Green
              
                Destroy(this);
            }
        }
    
    }


}
