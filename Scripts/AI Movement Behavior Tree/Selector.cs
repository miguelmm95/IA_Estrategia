using System.Collections.Generic;

namespace BehaviorTree
{
    public class Selector : NodeBT
    {
        public Selector() : base() { }
        public Selector(List<NodeBT> children) : base(children) { }

        public override NodeBTState Evaluate()
        {
            foreach(NodeBT NodeBT in children)
            {
                switch(NodeBT.Evaluate())
                {
                    case NodeBTState.FAILURE:
                        continue;
                    case NodeBTState.SUCCESS:
                        state = NodeBTState.SUCCESS;
                        return state;
                    case NodeBTState.RUNNING:
                        state = NodeBTState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeBTState.FAILURE;
            return state;
        }
    }
}

