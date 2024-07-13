using System;
using UnityEngine;
using GSHFramework.Tool;
using System.Collections.Generic;

namespace GSHFramework.StateMachine
{
    /// <summary>
    /// 泛型状态机
    /// </summary>
    /// <typeparam name="SM">状态机实例类</typeparam>
    [Serializable]
    public class StateMachine<SM> : IStateMachine where SM : class, IStateMachine, new()
    {
        /// <summary>
        /// 状态机节点缓存
        /// </summary>
        protected Dictionary<string, IStateNode<SM>> stateNodes = new Dictionary<string, IStateNode<SM>>();

        /// <summary>
        /// 当前执行状态节点
        /// </summary>
        public IStateNode<SM> CurrentNode { get { return currentNode; } }
        private IStateNode<SM> currentNode;

        public virtual void AddNode<Node>(bool multiplex) where Node : class, IStateNode, new()
        {
            Node tempNode = new Node();

            if (stateNodes.ContainsKey(tempNode.GetType().Name))
            {
                var node = (IStateNode<SM>)tempNode;
                node.MyStateMachine = this as SM;
                node.Multiplex = multiplex;
                node.OnRegsiter();
                stateNodes[node.GetType().Name] = node;
            }
            else
            {
                var node = (IStateNode<SM>)tempNode;
                node.MyStateMachine = this as SM;
                node.Multiplex = multiplex;
                node.OnRegsiter();
                stateNodes.Add(node.GetType().Name, node);
            }
        }

        public virtual void AddNode(IStateNode node, bool multiplex)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"状态机节点{node.GetType().Name}为空");
            }
            var addNode = ((IStateNode<SM>)node);
            if (stateNodes.ContainsKey(node.GetType().Name))
            {
                addNode.MyStateMachine = this as SM;
                addNode.Multiplex = multiplex;
                addNode.OnRegsiter();
                stateNodes[node.GetType().Name] = addNode;
            }
            else
            {
                addNode.MyStateMachine = this as SM;
                addNode.Multiplex = multiplex;
                addNode.OnRegsiter();
                stateNodes.Add(node.GetType().Name, addNode);
            }
        }

        public virtual void RemoveNode<Node>() where Node : class, IStateNode, new()
        {
            Node tempNode = new Node();

            if (stateNodes.ContainsKey(tempNode.GetType().Name))
            {
                stateNodes[tempNode.GetType().Name].UnRegsiter();
                stateNodes.Remove(tempNode.GetType().Name);
            }
        }

        public virtual void RemoveNode(IStateNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException($"状态机节点{node.GetType().Name}为空");
            }

            if (stateNodes.ContainsKey(node.GetType().Name))
            {
                stateNodes[node.GetType().Name].UnRegsiter();
                stateNodes.Remove(node.GetType().Name);
            }
        }

        public virtual bool HasStateNode<Node>() where Node : class, IStateNode, new()
        {
            Node stateNode = new Node();
            if (stateNodes.ContainsKey(stateNode.GetType().Name))
            {
                return true;
            }
            return false;
        }

        public virtual void ExcuteStateNode<Node>() where Node : class, IStateNode, new()
        {
            string nodeName = typeof(Node).Name;

            if (string.IsNullOrEmpty(nodeName) || !stateNodes.ContainsKey(nodeName))
            {
                Debug.LogError($"当前状态机{this.GetType().Name}状态节点未注册");
                return;
            }
            else
            {
                Debug.LogError($"当前状态机{this.GetType().Name}状态节点---注册{nodeName}");
            }

            if (currentNode == null)
            {
                var node = stateNodes[nodeName];
                node.OnEnterState();
                currentNode = node;
            }
            else
            {
                currentNode.OnLeaveState();
                if (!currentNode.Multiplex)
                {
                    currentNode.UnRegsiter();
                    stateNodes.Remove(currentNode.GetType().Name);
                }
                currentNode = null;

                var node = stateNodes[nodeName];
                node.OnEnterState();
                currentNode = node;
            }
        }

        public virtual void OnInit()
        {
            if (stateNodes == null)
            {
                stateNodes = new Dictionary<string, IStateNode<SM>>();
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {
            if (currentNode != null)
            {
                currentNode.OnUpdateState(deltaTime);
            }
        }

        public virtual void OnExit()
        {
            currentNode = null;
            stateNodes.Clear();
        }
    }
}