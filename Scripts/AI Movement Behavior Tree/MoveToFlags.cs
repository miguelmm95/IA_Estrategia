using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class MoveToFlags : NodeBT
{
    public MoveToFlags(List<GameObject> flags)
    {
        foreach(GameObject flag in flags)
        {
            foreach(Tile tile in Grid.Instance.GetNeighboursUnit(/*flag.tile*/))
            {
                if(tile.occupiedUnit == null || tile.occupiedUnit.player == Player.Human)
                {
                    //mover unidades a bandera mas cercana o aleatoriamente
                }
            }
        }
    }

    public override NodeBTState Evaluate()
    {
        state = NodeBTState.SUCCESS;
        return state;
    }
}
