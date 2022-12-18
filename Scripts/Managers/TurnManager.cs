using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public static GameObject _inGameUI;
    public static int contador;
    public static int turnCounter;

    private void Awake()
    {
        Instance = this;
        turnCounter = 1;
    }

    private void Update()
    {
        if(contador >= UnitManager._AIUnitsObjects.Count && UnitManager._AIUnitsObjects.Count != 0)
        {
            turnCounter++;
            UnitManager.Instance.RestartHumanUnits();
            UnitManager.selectedHumanUnit = null;
            UnitManager.Instance.DeactivateAIUnits();
            GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
            _inGameUI.SetActive(true);
            contador = 0;
        }
    }
    public void SearchUI()
    {
        _inGameUI = GameObject.Find("InGameUI");
        Debug.Log(_inGameUI);
    }
    public void EndPlayerTurn()
    {
        UnitManager.Instance.RestartAIUnits();
        _inGameUI.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.AITurn);
        UnitManager.Instance.ActiveAIUnits();

    }
}
