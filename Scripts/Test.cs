using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private bool Started = false;
    [SerializeField] private GameObject _inGameUI;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject button;
    private bool isHidden = false;

    public void ChangeState()
    {
        if (!Started)
        {
            GameManager.Instance.UpdateGameState(GameState.GenerateGrid);
            Started = true;
        }
        else
        {
            GameManager.Instance.UpdateGameState(GameState.SpawnAIUnits);
            _inGameUI.SetActive(true);
        }
    }

    public void Hide()
    {
        if (!isHidden)
        {
            shop.SetActive(false);
            button.SetActive(false);
            isHidden = true;
        }
        else
        {
            shop.SetActive(true);
            button.SetActive(true);
            isHidden = false;
        }
    }
}
