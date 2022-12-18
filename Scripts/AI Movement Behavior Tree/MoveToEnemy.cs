using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToEnemy : NodeBT
{
    public bool attack = false;
    public MoveToEnemy(BaseAIUnit unit)
    {
        List<Tile> movementTiles = Grid.Instance.GetNeighboursAIUnit(unit.occupiedTile, unit.getRange(unit.type));
        Tile lastTile = unit.occupiedTile;
        Tile newTile = null;
        object data = GetData("Encontrado");

        if(unit.state == State.AIAttacking && data == "si")
        {
            Debug.Log("ESTOY AQUI");
            foreach (Tile tile in movementTiles)
            {
                if (tile.occupiedUnit == unit._playerTarget)
                {
                    attack = true;
                    Debug.Log("Le voy a atacar");
                }
            }

            if (!attack)
            {
                foreach (Tile tile in movementTiles)
                {
                    if ((tile.posX >= unit.occupiedTile.posX && tile.posY >= unit.occupiedTile.posY) && tile.isWalkeable)
                    {
                        newTile = tile;
                        break;
                    }
                    else if ((tile.posX >= unit.occupiedTile.posX && tile.posY <= unit.occupiedTile.posY) && tile.isWalkeable)
                    {
                        newTile = tile;
                        break;
                    }
                    else if ((tile.posX <= unit.occupiedTile.posX && tile.posY <= unit.occupiedTile.posY) && tile.isWalkeable)
                    {
                        newTile = tile;
                        break;
                    }
                    else if ((tile.posX <= unit.occupiedTile.posX && tile.posY >= unit.occupiedTile.posY) && tile.isWalkeable)
                    {
                        newTile = tile;
                        break;
                    }
                }
                lastTile.occupiedUnit = null;
                unit.occupiedTile = newTile;
                newTile.occupiedUnit = unit;
                unit.state = State.AIWaiting;
                TurnManager.contador++;
                Debug.Log(TurnManager.contador);
            }
        }
        
    }

    public override NodeBTState Evaluate()
    {
        if (attack)
        {
            parent.parent.SetData("Atacando", "si");
            ClearData("Encontrado");
            state = NodeBTState.SUCCESS;
            return state;
        }
        state = NodeBTState.FAILURE;
        return state;
    }
}
