using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurrency : MonoBehaviour
{
    public Text currencyText;

    private void OnEnable()
    {
        CurrencySystem.CurrencyEvent += UpdateCurrencyUI;
    }

    private void OnDisable()
    {
        CurrencySystem.CurrencyEvent -= UpdateCurrencyUI;
    }

    private void Start()
    {
        currencyText.text = string.Format("{0:$000}", CurrencySystem.currentCurrency);
    }

    public void UpdateCurrencyUI(int currency)
    {
        currency =  Mathf.Clamp(currency, 0, 999);
        currencyText.text = string.Format("{0:$000}", currency);
    }
}
