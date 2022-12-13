using System;
using UnityEngine;
using System.Collections.Generic;
using BehaviorTree;

public class AIPlayer : BehaviorTree.Tree
{
    //private List<GameObject> PlayerFlags;
    //private List<GameObject> AIFlags;

    protected override NodeBT SetUpTree(){

        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>{
                //new DefendAIFlags(Grid._AIFlags, _aiUnit),
                //new MoveToFlags(/*Bandera objetivo, unidad*/),
            }),
            new MoveToFlag(UnitManager.Instance.getRandomHumanFlag(), UnitManager._AIUnitsObjects)
        });

        return root;
    }
}
