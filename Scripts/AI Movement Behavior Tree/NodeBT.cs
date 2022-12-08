using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeBTState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class NodeBT
    {
        protected NodeBTState state;
        public NodeBT parent;
        protected List<NodeBT> children;
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
    
        public NodeBT()
        {
            parent = null;
        }

        public NodeBT(List<NodeBT> children)
        {
            foreach(NodeBT child in children)
                _Attach(child);
        }

        private void _Attach(NodeBT NodeBT)
        {
            NodeBT.parent = this;
            children.Add(NodeBT);
        }

        public virtual NodeBTState Evaluate() => NodeBTState.FAILURE; 

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if(_dataContext.TryGetValue(key, out value))
                return value;
            
            NodeBT NodeBT = parent;
            while(NodeBT != null)
            {
                value = NodeBT.GetData(key);
                if(value != null)
                    return value;
                NodeBT = NodeBT.parent;
            }
            
            return null;
        }

        public bool ClearData(string key)
        {
            if(_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            NodeBT NodeBT = parent;
            while(NodeBT != null)
            {
                bool cleared = NodeBT.ClearData(key);
                if(cleared)
                    return true;
                NodeBT = NodeBT.parent;
            }
            return false;
        }
    }
}