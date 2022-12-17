using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class AttackEnemy : NodeBT
{
    public AttackEnemy(BaseAIUnit unit)
    {
        object data = GetData("Atacando");
        if (data != "si") return;

        unit.Attack(unit.damage, unit._playerTarget);
        unit.state = State.AIWaiting;

        if(unit._playerTarget.actualHealth <= 0)
        {
            unit._playerTarget = null;
        }
    }

    public override NodeBTState Evaluate()
    {
        ClearData("Atacando");
        state = NodeBTState.SUCCESS;
        return state;
    }
}
