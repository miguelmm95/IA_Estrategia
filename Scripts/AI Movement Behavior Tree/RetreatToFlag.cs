using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RetreatToFlag : NodeBT
{
    public RetreatToFlag(BaseAIUnit unit)
    {
        Tile objectiveTile = null;
        Tile lastTile = unit.occupiedTile;

        object data = GetData("Defendiendo");

        if (unit.state == State.AIDefending && data == "si")
        {
            objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToDefend.flagPosition);

            lastTile.occupiedUnit = null;
            unit.occupiedTile = objectiveTile;
            objectiveTile.occupiedUnit = unit;
            unit.transform.position = objectiveTile.transform.position;
            unit.state = State.AIWaiting;
        }
        
    }
    public override NodeBTState Evaluate()
    {
         ClearData("Defendiendo");
         state = NodeBTState.SUCCESS;
         return state;
    }
}
