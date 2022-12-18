using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToFlag : NodeBT
{
    private int totalUnits = 0;
    private int movedUnits = 0;
    private int turn;

    public bool isMoving = false;
    public MoveToFlag(BaseAIUnit unit)
    {
        object data = GetData("Defendiendo");
        if (unit.state == State.AIMoving)
        {
            isMoving = true;

            Tile objectiveTile = null;
            Tile lastTile = unit.occupiedTile;

            if (unit._flagToAttack == null)
            {
                Grid.Instance.GetNearFlagHuman(unit);

            }

            if (unit._flagToAttack.CheckUnit(unit))
            {
                if (unit.turnsInFlag == 0)
                {
                    unit.turnsInFlag = TurnManager.turnCounter;
                }
                else if ((TurnManager.turnCounter - unit.turnsInFlag) == 3)
                {
                    UnitManager._playerFlags.Remove(unit._flagToAttack);
                    unit._flagToAttack.ChangeFlagAI();
                    UnitManager._AIFlags.Add(unit._flagToAttack);
                    foreach (BaseAIUnit unit1 in unit._flagToAttack.unitAttackingFlag)
                    {
                        unit1.turnsInFlag = 0;
                        unit1._flagToAttack = null;
                    }
                }
                TurnManager.contador++;
                unit.state = State.AIMoved;
            }

            else
            {
                objectiveTile = unit.moveTo(Grid.Instance.GetNeighboursUnit(unit.occupiedTile, unit.getRange(unit.type)), unit._flagToAttack.flagPosition);

                lastTile.occupiedUnit = null;
                unit.occupiedTile = objectiveTile;
                objectiveTile.occupiedUnit = unit;
                unit.transform.position = objectiveTile.transform.position;
                TurnManager.contador++;
                unit.state = State.AIMoved;
            }

        }
    }

    public override NodeBTState Evaluate()
    {
        if(isMoving)
        {
            Debug.Log("Estoy moviendome");
            parent.parent.SetData("Moviendo","si");
            ClearData("Defendiendo");
            state = NodeBTState.SUCCESS;
            return state;
        }
        Debug.Log("No estoy moviendome");
        state = NodeBTState.FAILURE;
        return state;
    }
}
