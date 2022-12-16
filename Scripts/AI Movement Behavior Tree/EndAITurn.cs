using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class EndAITurn : NodeBT
{
    private GameObject _inGameUI;
    List<BaseHumanUnit> _unitList;
    
    public EndAITurn(GameObject InGameUI, List<BaseHumanUnit> units)
    {
        _inGameUI = InGameUI;
        _unitList = units;

    }
    public override NodeBTState Evaluate()
    {
        object end = GetData("HasEnded");
        if(end == "sí")
        {
            foreach(BaseHumanUnit unit in _unitList)
            {
                unit.state = State.humanUnselected;
            }
            GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
            _inGameUI.SetActive(true);
            ClearData("HasEnded");
            _unitList.Clear();
            state = NodeBTState.SUCCESS;
            return state;
        }
        else
        {
            state = NodeBTState.FAILURE;
            return state;
        }
        
    }
}
