using System;

namespace GSHFramework.StateMachine
{
    /// <summary>
    /// 状态机节点--接口(泛型统一处理)
    /// </summary>
    public interface IStateNode
    {
        /// <summary>
        /// 状态节点复用
        /// </summary>
        bool Multiplex { get; set; }
    }

    /// <summary>
    /// 状态机节点--接口
    /// </summary>
    /// <typeparam name="SM">实例状态机</typeparam>
    public interface IStateNode<out SM> : IStateNode where SM : class, IStateMachine, new()
    {
        /// <summary>
        /// 状态节点--隶属于<StateMachine>实例状态机
        /// </summary>
        IStateMachine MyStateMachine { get; internal set; }

        /// <summary>
        /// 状态节点注册
        /// </summary>
        void OnRegsiter();

        /// <summary>
        /// 状态节点进入
        /// </summary>
        void OnEnterState();

        /// <summary>
        /// 状态节点刷帧
        /// </summary>
        /// <param name="deltaTime">UnityEngine.Time.deltaTime</param>
        void OnUpdateState(float deltaTime);

        /// <summary>
        /// 状态节点离开
        /// </summary>
        void OnLeaveState();

        /// <summary>
        /// 状态节点注销
        /// </summary>
        void UnRegsiter();
    }
}