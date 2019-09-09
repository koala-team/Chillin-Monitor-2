using KS.SceneActions;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using UnityEngine;

namespace Koala
{
    public class ChangeParadoxGraphOccurrence : BaseOccurrence<ChangeParadoxGraphOccurrence, ChangeParadoxGraph>
    {
        private static readonly bool AUTO_UPDATE = true;

        private GameObject _gameObject;
        private GraphOwner _graphOwner;

        public ChangeParadoxGraphOccurrence() { }

        protected override ChangeParadoxGraph CreateOldConfig()
        {
            var graphOwner = GetGraphOwner();

            var oldConfig = new ChangeParadoxGraph()
            {
                Type = _newConfig.Type,
                GraphAsset = _newConfig.GraphAsset,
                Graph = graphOwner.graph,
            };

            if (_newConfig.Play.HasValue || _newConfig.Stop.HasValue || _newConfig.Restart.HasValue)
                oldConfig.Play = graphOwner.isRunning;

            if (_newConfig.Type == EParadoxGraphType.FSM && (_newConfig.Stop.HasValue || _newConfig.Restart.HasValue))
                oldConfig.FSMState = (graphOwner as FSMOwner).GetCurrentState();

            return oldConfig;
        }

        protected override void ManageSuddenChanges(ChangeParadoxGraph config, bool isForward)
        {
            var graphOwner = GetGraphOwner();

            if (config.GraphAsset != null)
            {
                graphOwner.StopBehaviour();
                graphOwner.graph = config.Graph;
                graphOwner.StartBehaviour(AUTO_UPDATE, null);
            }

            if (config.Play.HasValue)
            {
                if (config.Play.Value && !graphOwner.isRunning)
                {
                    graphOwner.StartBehaviour(AUTO_UPDATE, null);
                }
                else if (!config.Play.Value && graphOwner.isRunning)
                {
                    graphOwner.PauseBehaviour();
                }
            }

            if (config.Stop.HasValue && config.Stop.Value)
                graphOwner.StopBehaviour();

            if (config.Restart.HasValue && config.Restart.Value)
                graphOwner.RestartBehaviour();

            if (config.FSMState != null)
                (graphOwner as FSMOwner).TriggerState(config.FSMState.name);
        }

        private GameObject GetGameObject()
        {
            if (_gameObject == null)
                _gameObject = References.Instance.GetGameObject(_reference);

            return _gameObject;
        }

        private GraphOwner GetGraphOwner()
        {
            if (_graphOwner == null)
                _graphOwner = GetGameObject().GetComponent(_newConfig.ComponentType) as GraphOwner;

            return _graphOwner;
        }
    }
}
