using UnityEngine;

namespace GSHFramework.StateMachine
{
    /// <summary>
    /// 状态节点--抽象类
    /// </summary>
    /// <typeparam name="SM">隶属于状态机实例</typeparam>
    public abstract class StateNode<SM> : IStateNode<SM> where SM : class, IStateMachine, new()
    {
        public bool Multiplex { get; set; }

        IStateMachine IStateNode<SM>.MyStateMachine { get; set; }

        public void ExcuteNode<Node>() where  Node :class, IStateNode<SM>,new()
        {
            (this as IStateNode<SM>).MyStateMachine.ExcuteStateNode<Node>();
        }

        public virtual void OnEnterState()
        {
            Debug.Log("初始化"+ this.GetType().Name);
        }

        public virtual void OnLeaveState()
        {
            Debug.Log("离开" + this.GetType().Name);
        }

        public virtual void OnRegsiter()
        {
            //Debug.Log((this as IStateNode<SM>).MyStateMachine.GetType().Name + "注册了 vvvv" + this.GetType().Name);
        }

        public virtual void OnUpdateState(float deltaTime)
        {
            //Debug.Log("update"+ this.GetType().Name);
        }

        public virtual void UnRegsiter()
        {
            Debug.Log("注销" + this.GetType().Name);
        }
    }
}