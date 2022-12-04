using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeliverySystem : MonoBehaviour
{
    public Text Deliveries;
    public Text DeliveryResultTicker;

    private float tickerTime = 5f;

    private void OnEnable()
    {
        DeliverySystem.deliveryEvent += UpdateDeliveryUI;
        DeliveryCustomer.tipReceivedEvent += UpdateTipTicker;
    }

    private void OnDisable()
    {
        DeliverySystem.deliveryEvent -= UpdateDeliveryUI;
        DeliveryCustomer.tipReceivedEvent -= UpdateTipTicker;
    }

    private void Start()
    {
        UpdateDeliveryUI(LevelManager.instance.customers);
    }

    public void UpdateDeliveryUI(int deliveries)
    {
        Deliveries.text = "x" + deliveries;

    }

    public void UpdateTipTicker(string customerName, int tipReceived)
    {
        DeliveryResultTicker.gameObject.SetActive(true);
        DeliveryResultTicker.text = customerName + " gave you a " + "$"+ tipReceived + " tip!";
        StartCoroutine(RemoveTipTicker());
    }

    IEnumerator RemoveTipTicker()
    {
        yield return new WaitForSeconds(tickerTime);
        DeliveryResultTicker.gameObject.SetActive(false);
    }
}
