using System;
using UnityEngine;
using System.Collections.Generic;
using BehaviorTree;

public class AIPlayer : BehaviorTree.Tree
{
    void OnEnable()
    {
        SetUpTree();
    }
    protected override NodeBT SetUpTree(){

        NodeBT root = new Selector(new List<NodeBT>
        {   
            new Sequence(new List<NodeBT>{
                new DefenseAI(this.GetComponent<BaseAIUnit>())
            }),
            new Sequence(new List<NodeBT>{
                new MoveToFlag(this.GetComponent<BaseAIUnit>())
                //new AttackAI(this.GetComponent<BaseAIUnit>())
            }),
            new Sequence(new List<NodeBT>{
                new AttackAI(this.GetComponent<BaseAIUnit>())
            }),


        });

        return root;
    }
}
