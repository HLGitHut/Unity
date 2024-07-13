using System;

namespace GSHFramework.StateMachine
{
    /// <summary>
    /// 状态机接口
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        /// 初始化状态机
        /// </summary>
        void OnInit();

        /// <summary>
        /// 存在否??状态节点
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <returns></returns>
        bool HasStateNode<N>() where N : class, IStateNode, new();

        /// <summary>
        /// 执行状态节点
        /// </summary>
        /// <typeparam name="N"></typeparam>
        void ExcuteStateNode<N>() where N : class, IStateNode, new();

        /// <summary>
        /// 添加状态节点
        /// </summary>
        /// <typeparam name="Node">泛型状态节点 实例类型</typeparam>
       /// <param name="multiplex">复用</param>
        public void AddNode<Node>(bool multiplex) where Node : class, IStateNode, new();

        /// <summary>
        /// 添加状态节点
        /// </summary>
        /// <param name="node">节点实例</param>
        /// <param name="multiplex">复用</param>
        public void AddNode(IStateNode node,bool multiplex);

        /// <summary>
        /// 移除状态节点
        /// </summary>
        /// <typeparam name="Node">泛型状态节点 实例类型</typeparam>
        public void RemoveNode<Node>() where Node : class, IStateNode, new();

        /// <summary>
        /// 移除状态节点
        /// </summary>
        /// <param name="node">节点实例</param>
        public void RemoveNode(IStateNode node);

        /// <summary>
        /// 帧执行
        /// </summary>
        /// <param name="deltaTime"></param>
        void OnUpdate(float deltaTime);

        /// <summary>
        /// 终止状态机
        /// </summary>
        void OnExit();
    }
}