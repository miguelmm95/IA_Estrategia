using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static GameObject _inGameUI;
    public void SearchUI()
    {
        _inGameUI = GameObject.Find("InGameUI");
        Debug.Log(_inGameUI);
    }
    public void EndPlayerTurn()
    {
        GameManager.Instance.UpdateGameState(GameState.AITurn);
        _inGameUI.SetActive(false);
    }
}
