using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToFlag : NodeBT
{
    private int totalUnits = 0;
    private int movedUnits = 0;
    public MoveToFlag(Flag flag, BaseAIUnit unit)
    {
        Tile objectiveTile = new Tile();
        Tile lastTile = unit.occupiedTile;

        if (!unit.occupiedTile.hasAFlag)
        {
            objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)),flag.flagPosition);
        }
        lastTile.occupiedUnit = null;
        unit.occupiedTile = objectiveTile;
        unit.transform.position = objectiveTile.transform.position;
        movedUnits++;
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
