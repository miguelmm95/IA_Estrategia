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
        protected List<NodeBT> children = new List<NodeBT>();
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

        private void _Attach(NodeBT nodeBT)
        {
            nodeBT.parent = this;
            children.Add(nodeBT);
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
            
            NodeBT nodeBT = parent;
            while(nodeBT != null)
            {
                value = nodeBT.GetData(key);
                if(value != null)
                    return value;
                nodeBT = nodeBT.parent;
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

            NodeBT nodeBT = parent;
            while(nodeBT != null)
            {
                bool cleared = nodeBT.ClearData(key);
                if(cleared)
                    return true;
                nodeBT = nodeBT.parent;
            }
            return false;
        }
    }
}