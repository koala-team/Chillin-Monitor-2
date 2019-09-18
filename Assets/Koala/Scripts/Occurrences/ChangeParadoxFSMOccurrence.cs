using KS.SceneActions;
using NodeCanvas.StateMachines;
using System.Collections.Generic;
using UnityEngine;

namespace Koala
{
    public class ChangeParadoxFSMOccurrence : BaseOccurrence<ChangeParadoxFSMOccurrence, ChangeParadoxFSM>
    {
        private GameObject _gameObject;
        private FSMOwner _fsmOwner;

        public ChangeParadoxFSMOccurrence() { }

        protected override ChangeParadoxFSM CreateOldConfig()
        {
            var fsmOwner = GetFSMOwner();

			var oldConfig = new ChangeParadoxFSM()
			{
				States = fsmOwner.SaveFSMStates(),
			};

            return oldConfig;
        }

        protected override void ManageSuddenChanges(ChangeParadoxFSM config, bool isForward)
        {
            var fsmOwner = GetFSMOwner();

			if (isForward)
			{
				FSM fsm = fsmOwner.graph as FSM;

				for (int i = 0; fsm != null && i < config.TriggerStatesName.Count; i++)
				{
					string stateName = config.TriggerStatesName[i];
					bool hardTrigger = config.HardTrigger != null ? config.HardTrigger[i].Value : false;

					if (hardTrigger || fsm.currentStateName != stateName)
						fsm.TriggerState(stateName);

					if (fsm.currentState is NestedFSMState subState)
						fsm = subState.nestedFSM ?? null;
				}
			}
			else
			{
				fsmOwner.LoadFSMStates(config.States);
			}
		}

		private GameObject GetGameObject()
        {
            if (_gameObject == null)
                _gameObject = References.Instance.GetGameObject(_reference);

            return _gameObject;
        }

        private FSMOwner GetFSMOwner()
        {
            if (_fsmOwner == null)
                _fsmOwner = GetGameObject().GetComponent<FSMOwner>();

            return _fsmOwner;
        }
    }
}
