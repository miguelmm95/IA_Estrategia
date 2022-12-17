using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckNearEnemies : NodeBT
{
    bool targetFound = false;

    public CheckNearEnemies(BaseAIUnit unit)
    {
        List<Tile> vision = unit.GetVision();
        Type unitType = unit.type;

        foreach(Tile tile in vision)
        {
            if(tile.occupiedUnit.player == Player.Human)
            {
                if(unitType == Type.Heavy && tile.occupiedUnit.type == Type.Ranged)
                {
                    unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                    unit.state = State.AIAttacking;
                    targetFound = true;

                }else if (unitType == Type.Ranged && tile.occupiedUnit.type == Type.Infantry)
                {
                    unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                    unit.state = State.AIAttacking;
                    targetFound = true;
                }
                else if (unitType == Type.Infantry && tile.occupiedUnit.type == Type.Heavy)
                {
                    unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                    unit.state = State.AIAttacking;
                    targetFound = true;
                }
            }
        }
    }

    public override NodeBTState Evaluate()
    {
        if (targetFound)
        {
            state = NodeBTState.SUCCESS;
            return state;
        }
        state = NodeBTState.FAILURE;
        return state;
    }
}
