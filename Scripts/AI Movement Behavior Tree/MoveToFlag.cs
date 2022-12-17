using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToFlag : NodeBT
{
    private int totalUnits = 0;
    private int movedUnits = 0;
    public MoveToFlag(BaseAIUnit unit)
    {
        if (GameManager.Instance.State != GameState.AITurn) return;

        Tile objectiveTile = null;
        Tile lastTile = unit.occupiedTile;

        if(unit._flagToAttack == null)
        {
            Grid.Instance.GetNearFlagHuman(unit);
        }

        if (!unit.occupiedTile.hasAFlag)
        {
            Debug.Log("Voy a por la bandera");
            objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToAttack);
            Debug.Log(objectiveTile.name);
        }
        lastTile.occupiedUnit = null;
        unit.occupiedTile = objectiveTile;
        unit.transform.position = objectiveTile.transform.position;
        TurnManager.contador++;
    }

    public override NodeBTState Evaluate()
    {
        if(movedUnits == totalUnits && totalUnits != 0)
        {
            object end = GetData("HasEnded");
            if (end == null)
            {
                parent.parent.SetData("HasEnded", "sí");
            }
        }
        state = NodeBTState.SUCCESS;
        return state;
    }
}
