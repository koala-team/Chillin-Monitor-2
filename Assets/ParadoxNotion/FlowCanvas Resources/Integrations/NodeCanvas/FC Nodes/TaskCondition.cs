using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace FlowCanvas.Nodes
{

    ///Task Action is used to check ConditionTasks within the flowgraph in a simplified manner without exposing ports
    [Description("Returns an encapsulated condition check without exposing any value ports other than the boolean check")]
    public class TaskCondition : FlowNode, ITaskAssignable<ConditionTask>
    {

        [SerializeField]
        private ConditionTask _condition;

        public override string name {
            get { return condition != null ? condition.name : "Condition"; }
        }

        public ConditionTask condition {
            get { return _condition; }
            set { _condition = value; }
        }

        public Task task {
            get { return condition; }
            set { condition = (ConditionTask)value; }
        }

        protected override void RegisterPorts() {
            AddValueOutput<bool>("Condition", () =>
            {
                if ( condition != null )
                    return condition.CheckCondition(graphAgent, graphBlackboard);
                return false;
            });
        }
    }
}