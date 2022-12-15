using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RetreatToFlag : NodeBT
{
    public RetreatToFlag(List<BaseAIUnit> units)
    {
        Tile objectiveTile = new Tile();

        foreach (BaseAIUnit unit in units)
        {
            if(unit.state == State.AIDefending)
            {
                Tile lastTile = unit.occupiedTile;
                objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToDefend);

                lastTile.occupiedUnit = null;
                unit.occupiedTile = objectiveTile;
                unit.transform.position = objectiveTile.transform.position;
            }
        }
    }
}
