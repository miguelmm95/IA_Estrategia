using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                Grid.Instance.StartGrid();
                break;
            case GameState.UnitPlacement:

                break;
            case GameState.SpawnAIUnits:
                UnitManager.Instance.SpawnAIUnitsDefensive();
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
        foreach (BaseAIUnit unit in UnitManager._AIUnitsObjects)
        {
            var _main = unit.GetComponent<Main>();
            _main.TurnAI(unit);
        }
    }

    private void HandlePlayerTurn()
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
    UnitPlacement,
    SpawnAIUnits,
    PlayerTurn,
    AITurn,
    Victory,
    Lose
}