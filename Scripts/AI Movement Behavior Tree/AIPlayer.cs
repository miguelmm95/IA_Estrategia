using System;
using UnityEngine;
using System.Collections.Generic;
using BehaviorTree;

public class AIPlayer : BehaviorTree.Tree
{
    protected override NodeBT SetUpTree(){

        NodeBT root = new Selector(new List<NodeBT>
        {
            //Comprobar que no esta siendo atacada
            //Si estan siendo atacadas, las dos unidades mas cercanas van a defender
            //El resto atacan
            new Sequence(new List<NodeBT>{
                new DefendAIFlags(this.GetComponent<BaseAIUnit>()),
                new RetreatToFlag(this.GetComponent<BaseAIUnit>()),
            }),

            //Turno de moverse
            //Comprobar enemigos cercanos
            //  -Si hay enemigos cercanos -> Ir hacia ello y atacar.
            //  -Si no hoy enemigos cercanos -> Ir a bandera.

            new Sequence(new List<NodeBT>{
                new CheckNearEnemies(this.GetComponent<BaseAIUnit>()),
                new MoveToEnemy(),
                //new AttackEnemy()
            })
            //MoveToFlag

            /*new Sequence(new List<NodeBT>
            {
                new MoveToFlag(this.GetComponent<BaseAIUnit>()._flagToAttack, this.GetComponent<BaseAIUnit>()),
                new EndAITurn(TurnManager._inGameUI, UnitManager._humanUnitsObjects)
            })*/
            
        });

        return root;
    }
}
