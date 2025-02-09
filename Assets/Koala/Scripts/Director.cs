using KS.SceneActions;
using System.Reflection;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Koala
{
	// This Class create and manage occurrences
	public class Director
	{
		private readonly MethodInfo _doActionMethodInfo = typeof(Director).GetMethod("DoAction");
		private readonly Dictionary<string, bool> _onlySuddenChanges;


		public Director()
		{
			_onlySuddenChanges = new Dictionary<string, bool>()
			{
				{ CreateEmptyGameObject.NameStatic,      true },
				{ InstantiateBundleAsset.NameStatic,     true },
				{ CreateBasicObject.NameStatic,          true },
				{ CreateUIElement.NameStatic,            true },
				{ Destroy.NameStatic,                    true },
				{ ChangeIsActive.NameStatic,             true },
				{ ChangeVisibility.NameStatic,           true },
				{ ChangeTransform.NameStatic,            false },
				{ ChangeAnimatorVariable.NameStatic,     false },
				{ ChangeAnimatorState.NameStatic,        true },
				{ ChangeAudioSource.NameStatic,          false },
				{ ChangeRectTransform.NameStatic,        false },
				{ ChangeText.NameStatic,                 false },
				{ ChangeSlider.NameStatic,               false },
				{ ChangeImage.NameStatic,                false },
				{ ChangeRawImage.NameStatic,             false },
				{ ChangeSiblingOrder.NameStatic,         true },
				{ ManageComponent.NameStatic,            true },
				{ ChangeSprite.NameStatic,               false },
				{ ChangeRenderer.NameStatic,             true },
				{ ChangeEllipse2D.NameStatic,            false },
				{ ChangePolygon2D.NameStatic,            false },
				{ ChangeLine.NameStatic,                 false },
				{ ChangeLight.NameStatic,                false },
				{ ChangeCamera.NameStatic,               false },
				{ ClearScene.NameStatic,                 true },
				{ ChangeRenderSettings.NameStatic,       true },
				{ ChangeParadoxGraph.NameStatic,         true },
				{ ChangeParadoxBehaviourTree.NameStatic, true },
				{ ChangeParadoxFSM.NameStatic,           true },
				{ ChangeParadoxBlackboard.NameStatic,    false },
				{ EndCycle.NameStatic,                   true },
				{ AgentJoined.NameStatic,                true },
				{ AgentLeft.NameStatic,                  true },
				{ EndGame.NameStatic,                    true },
			};
		}

		public void DoAction<T, C>(C action, bool onlySuddenChanges)
			where T : Occurrence, IBaseOccurrence<T, C>, new()
			where C : BaseAction
		{
			action.Prepare();

			if (action.Cycle.Value < 0) throw new ArgumentOutOfRangeException("Action.Cycle", "Cycle can't be lower than zero!");
			if (action.DurationCycles.Value < 0) throw new ArgumentOutOfRangeException("Action.DurationCycles", "DurationCycles can't be lower than zero!");
			Helper.GetCyclesDurationTime(action.Cycle.Value, action.DurationCycles.Value, out float startTime, out float endTime, out _);

			// Update max end time
			if (endTime > Helper.MaxEndTime)
				Helper.MaxEndTime = endTime;

			T forwardOccurrence = new T();
			forwardOccurrence.Init(action.FullRef, startTime, endTime, action, true, null);
			Timeline.Instance.Schedule(startTime, forwardOccurrence);

			if (!onlySuddenChanges)
			{
				T backwardOccurrence = new T();
				backwardOccurrence.Init(action.FullRef, endTime, startTime, null, false, forwardOccurrence);
				Timeline.Instance.Schedule(endTime, backwardOccurrence);
			}
		}

		public void Action(KS.KSObject action)
		{
			Type actionType = null;
			Type occurrenceType = null;
			bool onlySuddenChanges = _onlySuddenChanges[action.Name()];

			switch (action.Name())
			{
				case CreateEmptyGameObject.NameStatic:
				case InstantiateBundleAsset.NameStatic:
				case CreateBasicObject.NameStatic:
				case CreateUIElement.NameStatic:
					occurrenceType = Helper.Assembly.GetType("Koala.InstantiateOccurrence");
					break;

				case EndCycle.NameStatic:
					Helper.MaxCycle += 1;
					return;
			}

			// Defaults
			actionType = actionType ?? Helper.Assembly.GetType("KS.SceneActions." + action.Name());
			occurrenceType = occurrenceType ?? Helper.Assembly.GetType("Koala." + action.Name() + "Occurrence");

			// Call method
			var doActionMethod = Helper.MakeGenericMethod(_doActionMethodInfo, occurrenceType, actionType);
			doActionMethod.Invoke(this, new object[] { Helper.DynamicCast(action, actionType), onlySuddenChanges });
		}
	}
}
