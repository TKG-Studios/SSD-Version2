using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{

    public static PauseScreen instance;
    public GameObject pauseScreen;
    



    private void Start()
    {
        instance = this;
        pauseScreen.SetActive(false);
    }

    


}
