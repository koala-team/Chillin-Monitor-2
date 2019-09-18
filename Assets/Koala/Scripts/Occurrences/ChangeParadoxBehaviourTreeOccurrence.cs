using KS.SceneActions;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace Koala
{
    public class ChangeParadoxBehaviourTreeOccurrence : BaseOccurrence<ChangeParadoxBehaviourTreeOccurrence, ChangeParadoxBehaviourTree>
    {
        private GameObject _gameObject;
        private BehaviourTreeOwner _behaviourTreeOwner;

        public ChangeParadoxBehaviourTreeOccurrence() { }

        protected override ChangeParadoxBehaviourTree CreateOldConfig()
        {
            var behaviourTreeOwner = GetBehaviourTreeOwner();

			var oldConfig = new ChangeParadoxBehaviourTree();

			if (_newConfig.Repeat.HasValue)
				oldConfig.Repeat = behaviourTreeOwner.repeat;

			if (_newConfig.UpdateInterval.HasValue)
				oldConfig.UpdateInterval = behaviourTreeOwner.updateInterval;

            return oldConfig;
        }

        protected override void ManageSuddenChanges(ChangeParadoxBehaviourTree config, bool isForward)
        {
            var behaviourTreeOwner = GetBehaviourTreeOwner();

			if (config.Repeat.HasValue)
				behaviourTreeOwner.repeat = config.Repeat.Value;

			if (config.UpdateInterval.HasValue)
				behaviourTreeOwner.updateInterval = config.UpdateInterval.Value;
		}

		private GameObject GetGameObject()
        {
            if (_gameObject == null)
                _gameObject = References.Instance.GetGameObject(_reference);

            return _gameObject;
        }

        private BehaviourTreeOwner GetBehaviourTreeOwner()
        {
            if (_behaviourTreeOwner == null)
                _behaviourTreeOwner = GetGameObject().GetComponent<BehaviourTreeOwner>();

            return _behaviourTreeOwner;
        }
    }
}
