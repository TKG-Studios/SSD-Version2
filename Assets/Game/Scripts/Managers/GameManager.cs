using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameStates currentState;


    private void OnEnable()
    {
        changeState(GameStates.LevelActive);
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
        return currentState;
    }

}
