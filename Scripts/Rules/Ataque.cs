using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ataque
{

    public static void AtaqueAI(BaseAIUnit unit)
    {
        if (unit.state == State.AIMoved || unit.state == State.AIDefending)
        {
            List<Tile> vision = Grid.Instance.GetNeighboursAIUnit(unit.occupiedTile, unit.movementRange);
            Type unitType = unit.type;

            foreach (Tile tile in vision)
            {

                if (tile.occupiedUnit != null && tile.occupiedUnit.player == Player.Human)
                {
                    /*Debug.Log("UNIDAD ENCONTRADA");

                    Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                    unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                    unit.state = State.AIAttacking;*/

                    if (unitType == Type.Heavy && tile.occupiedUnit.type == Type.Ranged)
                    {
                        Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                        unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                        unit.state = State.AIAttacking;


                    }else if (unitType == Type.Ranged && tile.occupiedUnit.type == Type.Infantry)
                    {
                        Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                        unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                        unit.state = State.AIAttacking;

                    }
                    else if (unitType == Type.Infantry && tile.occupiedUnit.type == Type.Heavy)
                    {
                        Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                        unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                        unit.state = State.AIAttacking;
                    }
                }
            }
            if (unit.state == State.AIAttacking)
            {
                unit.Attack(unit.damage, unit._playerTarget);
                unit.state = State.AIWaiting;

                if (unit._playerTarget.actualHealth <= 0)
                {
                    unit._playerTarget = null;
                }
                TurnManager.contador++;
            }
            else
            {
                unit.state = State.AIWaiting;
                TurnManager.contador++;
            }
        }
    }
}
