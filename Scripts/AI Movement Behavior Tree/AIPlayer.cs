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
            new MoveToFlag(this.GetComponent<BaseAIUnit>())

            /*new Sequence(new List<NodeBT>{
                new DefendAIFlags(this.GetComponent<BaseAIUnit>()),
                new RetreatToFlag(this.GetComponent<BaseAIUnit>()),
            }),
            new Sequence(new List<NodeBT>{
                new CheckNearEnemies(this.GetComponent<BaseAIUnit>()),
                new MoveToEnemy(this.GetComponent<BaseAIUnit>()),
                new AttackEnemy(this.GetComponent<BaseAIUnit>())
            }),

            new Sequence(new List<NodeBT>
            {
                new MoveToFlag(UnitManager.Instance.getRandomHumanFlag(), this.GetComponent<BaseAIUnit>()),
                new EndAITurn(TurnManager._inGameUI, UnitManager._humanUnitsObjects)
            })*/
            
        });

        return root;
    }
}
