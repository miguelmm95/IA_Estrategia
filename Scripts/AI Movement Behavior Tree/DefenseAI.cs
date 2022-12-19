using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class DefenseAI : NodeBT
{
     bool isDefending = false;
    public DefenseAI(BaseAIUnit unit)
    {
        Tile objectiveTile = null;
        Tile lastTile = unit.occupiedTile;
        if (unit._flagToDefend != null && unit._flagToDefend.neighbours.Contains(unit.occupiedTile))
        {
            unit.state = State.AIDefending;

        }
        else if (unit.state == State.AIRetire)
        {
            isDefending = true;
            objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToDefend.flagPosition);

            lastTile.occupiedUnit = null;
            unit.occupiedTile = objectiveTile;
            objectiveTile.occupiedUnit = unit;
            unit.transform.position = objectiveTile.transform.position;
            
        }
    }

    public override NodeBTState Evaluate()
    {
        if (isDefending)
        {
            Debug.Log("estoy defendiendo");
            state = NodeBTState.SUCCESS;
            parent.parent.SetData("Defendiendo", "si");
            return state;
        }
        Debug.Log("No estoy defendiendo");
        state = NodeBTState.FAILURE;
        return state;
    }
}
