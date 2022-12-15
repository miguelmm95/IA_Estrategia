using System.Collections.Generic;

namespace BehaviorTree
{
    public class Sequence : NodeBT
    {
        public Sequence() : base() { }
        public Sequence(List<NodeBT> children) : base(children) { }

        public override NodeBTState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach(NodeBT nodeBT in children)
            {
                switch(nodeBT.Evaluate())
                {
                    case NodeBTState.FAILURE:
                        state = NodeBTState.FAILURE;
                        return state;
                    case NodeBTState.SUCCESS:
                        continue;
                    case NodeBTState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeBTState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeBTState.RUNNING : NodeBTState.SUCCESS;
            return state;
        }
    }
}

