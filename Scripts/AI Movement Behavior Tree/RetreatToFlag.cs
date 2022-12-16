using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class RetreatToFlag : NodeBT
{
    public RetreatToFlag(BaseAIUnit unit)
    {
        Tile objectiveTile = new Tile();

        Tile lastTile = unit.occupiedTile;
        objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToDefend);

        lastTile.occupiedUnit = null;
        unit.occupiedTile = objectiveTile;
        unit.transform.position = objectiveTile.transform.position;
    }
}
