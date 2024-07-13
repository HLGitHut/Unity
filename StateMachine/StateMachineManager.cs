using UnityEngine;
using GSHFramework.Tool;
using System.Collections.Generic;

namespace GSHFramework.StateMachine
{
    /// <summary>
    ///  状态机总管理
    ///  总控注册后的状态机启停
    /// </summary>
    public class StateMachineManager : SingletonMono<StateMachineManager>
    {        
        private List<IStateMachine> stateMachines = new List<IStateMachine>();

        private bool isUpdate = true;

        /// <summary>
        /// 是否刷帧
        /// </summary>
        public bool IsUpdate { get => isUpdate; set => isUpdate = value; }

        /// <summary>
        /// 注册状态机 并 执行状态机 OnInit()
        /// </summary>
        /// <param name="machine">实例化状态机</param>
        public void RegisterStateMachine(IStateMachine machine)
        {
            if (!stateMachines.Contains(machine))
            {
                stateMachines.Add(machine);

                machine.OnInit();
            }
        }

        /// <summary>
        /// 卸载状态机 并 执行状态机 OnExit()
        /// </summary>
        /// <param name="machine">实例化状态机</param>
        public void UnRegisterStateMachine(IStateMachine machine)
        {
            if (stateMachines.Contains(machine))
            {
                stateMachines.Remove(machine);

                machine.OnExit();
            }
        }

        /// <summary>
        /// 执行状态节点
        /// </summary>
        public void ExcuteStateNode<Node>() where Node : class, IStateNode, new()
        {
            if (stateMachines != null && stateMachines.Count > 0)
            {
                for (int i = 0; i < stateMachines.Count; i++)
                {
                    if (!stateMachines[i].HasStateNode<Node>())
                    {
                        continue;
                    }
                    stateMachines[i].ExcuteStateNode<Node>();
                    break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();

            isUpdate = true;

            if (stateMachines == null)
            {
                stateMachines = new List<IStateMachine>();
            }
        }

        private void Update()
        {
            if (isUpdate)
            {
                if (stateMachines != null && stateMachines.Count > 0)
                {
                    for (int i = 0; i < stateMachines.Count; i++)
                    {
                        stateMachines[i].OnUpdate(Time.deltaTime);
                    }
                }
            }
        }
    }
}