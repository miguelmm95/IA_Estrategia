using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> onGameStateChanged;
    public TextMeshProUGUI win;
    public TextMeshProUGUI lose;

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
                UnitManager.Instance.SpawnAIUnitsAggressive();
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.AITurn:
                HandleAITurn();
                break;
            case GameState.Victory:
                UnitManager._AIUnits.Clear();
                UnitManager._AIUnitsObjects.Clear();
                UnitManager._humanUnitsObjects.Clear();
                UnitManager._playerUnits.Clear();
                HandleVictory();
                break;
            case GameState.Lose:
                UnitManager._AIUnits.Clear();
                UnitManager._AIUnitsObjects.Clear();
                UnitManager._humanUnitsObjects.Clear();
                UnitManager._playerUnits.Clear();
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        onGameStateChanged?.Invoke(newState);
    }

    private async void HandleLose()
    {
        lose.gameObject.SetActive(true);
        await Task.Delay(3567);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private async void HandleVictory()
    {
        
        win.gameObject.SetActive(true);
        await Task.Delay(3568);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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