using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToFlag : NodeBT
{
    public MoveToFlag(Flag flag, List<BaseAIUnit> units)
    {
        Tile objectiveTile = new Tile();

        foreach (BaseAIUnit unit in units)
        {
            Tile lastTile = unit.occupiedTile;

            if (!unit.occupiedTile.hasAFlag)
            {
                objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)),flag.flagPosition);
            }
            lastTile.occupiedUnit = null;
            unit.occupiedTile = objectiveTile;
            unit.transform.position = objectiveTile.transform.position;

        }
    }

    public override NodeBTState Evaluate()
    {
        state = NodeBTState.SUCCESS;
        return state;
    }
}
