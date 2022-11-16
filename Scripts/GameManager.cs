using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameState State;
    public static event Action<GameState> onGameStateChanged;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.PlayerShop);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.PlayerShop:
                break;
            case GameState.IAShop:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.AITurn:
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        onGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    PlayerShop,
    IAShop,
    PlayerTurn,
    AITurn,
    Victory,
    Lose
}