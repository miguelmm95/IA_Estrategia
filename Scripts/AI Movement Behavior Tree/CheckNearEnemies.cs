using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckNearEnemies : NodeBT
{
    bool targetFound = false;

    public CheckNearEnemies(BaseAIUnit unit)
    {
        List<Tile> vision = Grid.Instance.GetNeighboursAIUnit(unit.occupiedTile, unit.movementRange + 1);
        Type unitType = unit.type;

        foreach(Tile tile in vision)
        {

            if (tile.occupiedUnit != null && tile.occupiedUnit.player == Player.Human)
            {
                Debug.Log("UNIDAD ENCONTRADA");

                Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                unit.state = State.AIAttacking;
                targetFound = true;

                /*if (unitType == Type.Heavy && tile.occupiedUnit.type == Type.Ranged)
                {
                    Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                    unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                    unit.state = State.AIAttacking;
                    targetFound = true;

                }else if (unitType == Type.Ranged && tile.occupiedUnit.type == Type.Infantry)
                {
                    Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                    unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                    unit.state = State.AIAttacking;
                    targetFound = true;
                }
                else if (unitType == Type.Infantry && tile.occupiedUnit.type == Type.Heavy)
                {
                    Debug.Log("Soy" + unit.name + " en " + unit.occupiedTile.name + " y voy a atacar a " + tile.occupiedUnit + " en " + tile.name);
                    unit._playerTarget = (BaseHumanUnit)tile.occupiedUnit;
                    unit.state = State.AIAttacking;
                    targetFound = true;
                }*/
            }
        }
    }

    public override NodeBTState Evaluate()
    {
        if (targetFound)
        {
            Debug.Log("CAMBIO DE NODO");
            parent.parent.SetData("Encontrado", "si");
            state = NodeBTState.SUCCESS;
            return state;
        }
        state = NodeBTState.FAILURE;
        return state;
    }
}
