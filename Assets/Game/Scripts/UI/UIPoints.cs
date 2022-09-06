using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPoints : MonoBehaviour
{
    public static UIPoints Instance;

    public int pointsValue = 0000000;

    public Text pointsText;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = string.Format("{0:000000}", pointsValue);
    }
}
