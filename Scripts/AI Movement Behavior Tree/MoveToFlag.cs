using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToFlag : NodeBT
{
    public MoveToFlag(flagManagement flag, List<BaseAIUnit> units)
    {
        Tile objectiveTile = new Tile();

        foreach (BaseAIUnit unit in units)
        {
            Tile lastTile = unit.occupiedTile;

            if (!unit.occupiedTile.hasAFlag)
            {
                objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)),flag.tileFlag);
            }
            lastTile.occupiedUnit = null;
            unit.occupiedTile = objectiveTile;
            unit.transform.position = objectiveTile.transform.position;

        }
        /*Tile objectiveTile = new Tile();

        objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), flag.tileFlag);

        Tile lastTile = unit.occupiedTile;
        lastTile.occupiedUnit = null;
        unit.occupiedTile = objectiveTile;
        unit.transform.position = objectiveTile.transform.position;*/
    }

    public override NodeBTState Evaluate()
    {
        state = NodeBTState.SUCCESS;
        return state;
    }
}
