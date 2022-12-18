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
        if(unit.state == State.AIDefending)
        {
            objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToDefend.flagPosition);

            lastTile.occupiedUnit = null;
            unit.occupiedTile = objectiveTile;
            objectiveTile.occupiedUnit = unit;
            unit.transform.position = objectiveTile.transform.position;
            unit.state = State.AIWaiting;
        }
        
    }
}
