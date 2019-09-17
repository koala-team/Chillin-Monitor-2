using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using FlowCanvas;

namespace NodeCanvas.StateMachines
{

    [Name("FlowScript")]
    [Category("Nested")]
    [Description("Adds a FlowCanvas FlowScript as a nested State of the FSM. The FlowScript State is never finished by itself, unless a 'Finish' node is used in the FlowScript. Success/Failure events can optionlay be used alongside with a CheckEvent on state transitions to catch whether the FlowScript Finished in success or failure.")]
    public class FlowScriptState : FSMState, IGraphAssignable
    {

        [SerializeField]
        private BBParameter<FlowScript> _flowScript = null;
        private Dictionary<FlowScript, FlowScript> instances = new Dictionary<FlowScript, FlowScript>();
        private FlowScript currentInstance = null;

        public string successEvent;
        public string failureEvent;

        public FlowScript flowScript {
            get { return _flowScript.value; }
            set { _flowScript.value = value; }
        }

        Graph IGraphAssignable.nestedGraph {
            get { return flowScript; }
            set { flowScript = (FlowScript)value; }
        }

        Graph[] IGraphAssignable.GetInstances() { return instances.Values.ToArray(); }


        protected override void OnEnter() {

            if ( flowScript == null ) {
                Finish(false);
                return;
            }

            currentInstance = CheckInstance();
            currentInstance.StartGraph(graphAgent, graphBlackboard, false, OnFlowScriptFinished);
        }

        protected override void OnUpdate() {
            currentInstance.UpdateGraph();
        }

        void OnFlowScriptFinished(bool success) {
            if ( this.status == Status.Running ) {
                if ( !string.IsNullOrEmpty(successEvent) && success ) {
                    SendEvent(new EventData(successEvent));
                }

                if ( !string.IsNullOrEmpty(failureEvent) && !success ) {
                    SendEvent(new EventData(failureEvent));
                }

                Finish(success);
            }
        }

        protected override void OnExit() {
            if ( currentInstance != null && ( currentInstance.isRunning || currentInstance.isPaused ) ) {
                currentInstance.Stop();
            }
        }

        protected override void OnPause() {
            if ( currentInstance != null && currentInstance.isRunning ) {
                currentInstance.Pause();
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

        protected override void OnNodeGUI() {

            GUILayout.Label(string.Format("Sub FlowScript\n{0}", _flowScript));
            if ( flowScript == null ) {
                if ( !Application.isPlaying && GUILayout.Button("CREATE NEW") ) {
                    Node.CreateNested<FlowScript>(this);
                }
            }
        }

        protected override void OnNodeInspectorGUI() {

            ShowBaseFSMInspectorGUI();
            NodeCanvas.Editor.BBParameterEditor.ParameterField("FlowScript", _flowScript);

            var alpha1 = string.IsNullOrEmpty(successEvent) ? 0.5f : 1;
            var alpha2 = string.IsNullOrEmpty(failureEvent) ? 0.5f : 1;
            GUILayout.BeginVertical("box");
            GUI.color = new Color(1, 1, 1, alpha1);
            successEvent = UnityEditor.EditorGUILayout.TextField("Success Event", successEvent);
            GUI.color = new Color(1, 1, 1, alpha2);
            failureEvent = UnityEditor.EditorGUILayout.TextField("Failure Event", failureEvent);
            GUILayout.EndVertical();
            GUI.color = Color.white;

            if ( flowScript == null ) {
                return;
            }

            var defParams = flowScript.GetDefinedParameters();
            if ( defParams.Length != 0 ) {

                EditorUtils.TitledSeparator("Defined Nested FlowScript Parameters");
                GUI.color = Color.yellow;
                UnityEditor.EditorGUILayout.LabelField("Name", "Type");
                GUI.color = Color.white;
                var added = new List<string>();
                foreach ( var bbVar in defParams ) {
                    if ( !added.Contains(bbVar.name) ) {
                        UnityEditor.EditorGUILayout.LabelField(bbVar.name, bbVar.varType.FriendlyName());
                        added.Add(bbVar.name);
                    }
                }
                if ( GUILayout.Button("Check/Create Blackboard Variables") ) {
                    flowScript.PromoteDefinedParametersToVariables(graphBlackboard);
                }
            }
        }

#endif
    }
}