using System.Collections.Generic;
using BehaviorTree;

public class AIPlayer : Tree
{
    //private List<GameObject> PlayerFlags;
    //private List<GameObject> AIFlags;

    protected override NodeBT SetUpTree(){

        NodeBT root = new Selector(new List<NodeBT>
        {
            new Sequence(new List<NodeBT>{
                //new DefendAIFlags(AIFlags),
                //new MoveToFlags(PlayerFlags),
            }),
            //new MoveToFlags(PlayerFlags),
        });

        return root;
    }
}
