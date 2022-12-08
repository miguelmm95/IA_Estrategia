using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class DefendAIFlags : NodeBT
{
    private List<Tile> flagsToDefend;

    public DefendAIFlags(List<GameObject> flags)
    {
        foreach(GameObject flag in flags)
        {
            foreach(Tile tile in Grid.Instance.GetNeighboursUnit(/*flag.tile*/))
            {
                if(tile.occupiedUnit != null && tile.occupiedUnit.player == Player.Human)
                {
                    flagsToDefend.Add(flag);
                }
            }
        }

        if(flagsToDefend.Count != 0)
        {
            List<BaseAIUnit> defenders = new List<BaseAIUnit>();

            for(int i = 0; i < flagsToDefend.Count; i++)
            {
                //funcion dos unidades mas cercanas
            }
        }
    }

    public override NodeBTState Evaluate()
    {
        if(flagsToDefend.Count > 0)
        {
            state = NodeBTState.SUCCESS;
            return state;
        }
        state = NodeBTState.FAILURE;
        return state;
    }
}
