using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _ShopUI;
    [SerializeField] private GameObject _aEstrella;
    [SerializeField] private GameObject _behaviourTree;

    void Awake()
    {
        GameManager.onGameStateChanged += GameManagerOnGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState state)
    {
        _ShopUI.SetActive(state == GameState.PlayerShop);
        _aEstrella.SetActive(state == GameState.GenerateGrid);
        _aEstrella.SetActive(state == GameState.SpawnAIUnits);
        _aEstrella.SetActive(state == GameState.PlayerTurn);
    }
}
