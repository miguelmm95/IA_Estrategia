using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Movimiento
{

    public static void MovimientoAI(BaseAIUnit unit, int turn)
    {
        if (unit.state == State.AIMoving)
        {
            //isMoving = true;

            Tile objectiveTile = null;
            Tile lastTile = unit.occupiedTile;

            if (unit._flagToAttack == null && unit._flagToDefend == null)
            {
                Grid.Instance.GetNearFlagHuman(unit);
            }

            if (unit._flagToAttack.CheckUnit(unit))
            {
                unit._flagToAttack.unitAIAttackinHumanFlag.Add(unit);
                if (unit.turnsInFlag == 0)
                {
                    unit.turnsInFlag = TurnManager.turnCounter;
                    Debug.Log("Turnos en la bandera: " + unit.turnsInFlag);
                }
                else if ((TurnManager.turnCounter - unit.turnsInFlag) == 3)
                {
                    Flag _bandera = unit._flagToAttack;

                    UnitManager._playerFlags.Remove(unit._flagToAttack);
                    unit._flagToAttack.ChangeFlagAI();
                    UnitManager._AIFlags.Add(unit._flagToAttack);

                    foreach (BaseAIUnit unit1 in unit._flagToAttack.unitAIAttackinHumanFlag)
                    {
                        unit1.turnsInFlag = 0;
                        unit1._flagToAttack = null;
                    }
                    _bandera.unitAIAttackinHumanFlag.Clear();
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
