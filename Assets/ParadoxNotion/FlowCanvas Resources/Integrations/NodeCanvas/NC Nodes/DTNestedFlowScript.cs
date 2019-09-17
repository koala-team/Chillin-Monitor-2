using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using FlowCanvas;

namespace NodeCanvas.DialogueTrees
{

    [Name("FlowScript")]
    [Category("Nested")]
    [Description("Executes a FlowScript. Use the Finish flowscript node to continue the dialogue either in Success or Failure.\nThe actor selected here will also be used in the flowcript for 'Self'.")]
    public class DTNestedFlowScript : DTNode, IGraphAssignable
    {

        [SerializeField]
        [ExposeField]
        private BBParameter<FlowScript> _flowScript = null;
        private Dictionary<FlowScript, FlowScript> instances = new Dictionary<FlowScript, FlowScript>();
        private FlowScript currentInstance = null;

        public override int maxOutConnections {
            get { return 2; }
        }

        public FlowScript flowScript {
            get { return _flowScript.value; }
            set { _flowScript.value = value; }
        }

        Graph IGraphAssignable.nestedGraph {
            get { return flowScript; }
            set { flowScript = (FlowScript)value; }
        }

        Graph[] IGraphAssignable.GetInstances() { return instances.Values.ToArray(); }


        protected override Status OnExecute(Component agent, IBlackboard bb) {
            if ( flowScript == null ) {
                return Error("FlowScript is null");
            }

            currentInstance = CheckInstance();
            status = Status.Running;
            currentInstance.StartGraph(finalActor.transform, graphBlackboard, false, OnFlowScriptFinish);
            StartCoroutine(UpdateGraph());
            return status;
        }

        IEnumerator UpdateGraph() {
            while ( status == Status.Running ) {
                currentInstance.UpdateGraph();
                yield return null;
            }
        }

        void OnFlowScriptFinish(bool success) {
            status = success ? Status.Success : Status.Failure;
            DLGTree.Continue(success ? 0 : 1);
        }

        public override void OnGraphPaused() {
            if ( currentInstance != null ) {
                currentInstance.Pause();
            }
        }

        public override void OnGraphStoped() {
            if ( currentInstance != null ) {
                currentInstance.Stop();
            }
        }


        FlowScript CheckInstance() {

            if ( flowScript == currentInstance ) {
                return currentInstance;
            }

            FlowScript instance = null;
            if ( !instances.TryGetValue(flowScript, out instance) ) {
                instance = Graph.Clone<FlowScript>(flowScript);
                instances[flowScript] = instance;
            }

            flowScript = instance;
            return instance;
        }

        ///----------------------------------------------------------------------------------------------
        ///---------------------------------------UNITY EDITOR-------------------------------------------
#if UNITY_EDITOR

        public override string GetConnectionInfo(int i) {
            return i == 0 ? "Success" : "Failure";
        }

        protected override void OnNodeGUI() {
            GUILayout.Label(string.Format("FlowScript\n{0}", _flowScript));
            if ( flowScript == null ) {
                if ( !Application.isPlaying && GUILayout.Button("CREATE NEW") ) {
                    Node.CreateNested<FlowScript>(this);
                }
            }
        }

#endif
    }
}