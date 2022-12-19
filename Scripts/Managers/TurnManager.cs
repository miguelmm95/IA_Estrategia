using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public static GameObject _inGameUI;
    public static int contador;
    public static int turnCounter;
    [SerializeField] private GameObject shopManager;

    private void Awake()
    {
        Instance = this;
        turnCounter = 0;
    }

    private void Update()
    {
        if(contador >= UnitManager._AIUnitsObjects.Count && UnitManager._AIUnitsObjects.Count != 0)
        {
            turnCounter++;
            UnitManager.Instance.RestartHumanUnits();
            UnitManager.selectedHumanUnit = null;
            //UnitManager.Instance.DeactivateAIUnits();
            GameManager.Instance.UpdateGameState(GameState.PlayerShop);
            _inGameUI.SetActive(false);
            contador = 0;
        }
    }
    public GameObject SearchUI()
    {
        _inGameUI = GameObject.Find("InGameUI");
        return _inGameUI;
    }
    public void EndPlayerTurn()
    {
     
        foreach (Flag flag in UnitManager._AIFlags)
        {
            if (!flag.beingAttacked)
            {
                flag.CheckUnitHumanInFlagAI();
            }
        }
        UnitManager.Instance.RestartAIUnits();
        _inGameUI.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.AITurn);
        //UnitManager.Instance.ActiveAIUnits();
        shopManager.GetComponent<ShopManager>().GainMoney(5f);
        UnitManager._playerUnits.Clear();
        UnitManager._AIUnits.Clear();

    }
}
