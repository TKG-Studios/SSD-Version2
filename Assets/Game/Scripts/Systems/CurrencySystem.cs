using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public delegate void CurrencyEventHandler(int currency);
    public static event CurrencyEventHandler CurrencyEvent;

    public static int currentCurrency = 0;
    public void AddCurrency(int currencyAmount)
    {
            currentCurrency += currencyAmount;
       
        if (CurrencyEvent != null) CurrencyEvent(currentCurrency);
     
    }
}
