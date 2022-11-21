using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> onGameStateChanged;

    void Awake()
    {
        Instance = this;
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
                HandlePlayerShop();
                break;
            case GameState.GenerateGrid:
                HandleGenerateGrid();
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.AITurn:
                HandleAITurn();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        onGameStateChanged?.Invoke(newState);
    }

    private void HandleLose()
    {
        
    }

    private void HandleVictory()
    {
        
    }

    private void HandleAITurn()
    {
        
    }

    private void HandlePlayerTurn()
    {
        
    }

    private void HandleGenerateGrid()
    {
        
    }

    private void HandlePlayerShop()
    {

    }
}

public enum GameState
{
    PlayerShop,
    GenerateGrid,
    PlayerTurn,
    AITurn,
    Victory,
    Lose
}