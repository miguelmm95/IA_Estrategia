using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public static Movimiento Instance;
    private int totalUnits = 0;
    private int movedUnits = 0;
    private int turn;

    public bool isMoving = false;

    private void Awake()
    {
        Instance = this;
    }
    public void MovimientoAI(BaseAIUnit unit)
    {
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
}
