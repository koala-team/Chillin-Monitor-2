﻿using KS.SceneActions;
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
		private readonly MethodInfo _castMethodInfo = typeof(Helper).GetMethod("Cast");
		private readonly Dictionary<string, bool> _onlySuddenChanges;


		public Director()
		{
			_onlySuddenChanges = new Dictionary<string, bool>()
			{
				{ CreateEmptyGameObject.NameStatic,  true },
				{ InstantiateBundleAsset.NameStatic, true },
				{ CreateBasicObject.NameStatic,      true },
				{ CreateUIElement.NameStatic,        true },
				{ Destroy.NameStatic,                true },
				{ ChangeVisibility.NameStatic,       true },
				{ ChangeTransform.NameStatic,        false },
				{ ChangeAnimatorVariable.NameStatic, false },
				{ ChangeAnimatorState.NameStatic,    true },
				{ ChangeAudioSource.NameStatic,      false },
				{ ChangeRectTransform.NameStatic,    false },
				{ ChangeText.NameStatic,             false },
				{ ChangeSlider.NameStatic,           false },
				{ ChangeRawImage.NameStatic,         false },
				{ ChangeSiblingOrder.NameStatic,     true },
				{ ManageComponents.NameStatic,       true },
				{ ChangeSprite.NameStatic,           false },
				{ ChangeMaterial.NameStatic,         true },
				{ ChangeEllipse2D.NameStatic,        false },
				{ ChangePolygon2D.NameStatic,        false },
				{ ChangeLine.NameStatic,             false },
				{ ChangeLight.NameStatic,            false },
				{ ChangeCamera.NameStatic,           false },
				{ StoreBundleData.NameStatic,        true },
				{ ClearScene.NameStatic,             true },
				{ AgentJoined.NameStatic,            true },
				{ AgentLeft.NameStatic,              true },
				{ EndGame.NameStatic,                true },
			};
		}

		public void DoAction<T, C>(C action, bool onlySuddenChanges)
			where T : Occurrence, IBaseOccurrence<T, C>, new()
			where C : BaseAction
		{
			action.Prepare();

			float startTime, endTime, duration;
			Helper.GetCyclesDurationTime(action.Cycle.Value, action.DurationCycles ?? 0, out startTime, out endTime, out duration);

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

		public void Action(BaseAction action)
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

				case StoreBundleData.NameStatic:
					var bundleData = (StoreBundleData)action;
					
					AssetBundle bundle = AssetBundle.LoadFromMemory(bundleData.BundleData.GetBytes());
					BundleManager.Instance.AddBundle(bundleData.BundleName, bundle);
					return;
			}

			// Defaults
			actionType = actionType ?? Helper.Assembly.GetType("KS.SceneActions." + action.Name());
			occurrenceType = occurrenceType ?? Helper.Assembly.GetType("Koala." + action.Name() + "Occurrence");

			// Call method
			var castMethod = _castMethodInfo.MakeGenericMethod(actionType);

			var doActionMethod = _doActionMethodInfo.MakeGenericMethod(occurrenceType, actionType);
			doActionMethod.Invoke(this, new object[] { castMethod.Invoke(null, new object[] { action }), onlySuddenChanges });
		}
	}
}
