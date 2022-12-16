using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class DefendAIFlags : NodeBT
{
    private bool flagsInDanger;


    public DefendAIFlags(BaseAIUnit unit)
    {

        if (unit.state == State.AIDefending && unit._flagToDefend != null)
        {
            flagsInDanger = true;
        }
        /*var nearUnits = new List<BaseAIUnit>();

        foreach(GameObject flag in flags)
        {
            if (flag.GetComponent<Flag>().beingAttacked) {
                Grid.Instance.GetNearAIUnits(flag.GetComponent<Flag>().flagPosition, UnitManager._AIUnitsObjects);
                flagsInDanger = true;
            }
        }*/
    }

    public override NodeBTState Evaluate()
    {
        if(flagsInDanger)
        {
            state = NodeBTState.SUCCESS;
            return state;
        }
        state = NodeBTState.FAILURE;
        return state;
    }
}
