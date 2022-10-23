using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameStates currentState;


    public delegate void StateChangeEventHandler();
    public static event StateChangeEventHandler stateChange;

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
      
    }
    public enum GameStates
    {
        StartScreen,
        LevelStart,
        LevelActive,
        LevelEnd,
        DeliveringItem,
        GamePaused,
        MenuScreen
    }

    public GameStates changeState(GameStates newState)
    {
   
        currentState = newState;
        if (stateChange != null) stateChange();
        return currentState;
    }



}
