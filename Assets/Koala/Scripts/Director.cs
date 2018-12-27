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
				{ ManageComponent.NameStatic,        true },
				{ ChangeSprite.NameStatic,           false },
				{ ChangeMaterial.NameStatic,         true },
				{ ChangeEllipse2D.NameStatic,        false },
				{ ChangePolygon2D.NameStatic,        false },
				{ ChangeLine.NameStatic,             false },
				{ ChangeLight.NameStatic,            false },
				{ ChangeCamera.NameStatic,           false },
				{ StoreBundleData.NameStatic,        true },
				{ ClearScene.NameStatic,             true },
				{ EndCycle.NameStatic,               true },
				{ AgentJoined.NameStatic,            true },
				{ AgentLeft.NameStatic,              true },
				{ EndGame.NameStatic,                true },
			};
		}

		public void DoAction<T, C>(C action, bool onlySuddenChanges)
			where T : Occurrence, IBaseOccurrence<T, C>, new()
			where C : BaseAction
		{
			action.Cycle = action.Cycle ?? 0;
			action.DurationCycles = action.DurationCycles ?? 0;

			if (action.Cycle.Value < 0) return;
			if (action.DurationCycles.Value < 0) return;

			action.Prepare();

			float startTime, endTime, duration;
			Helper.GetCyclesDurationTime(action.Cycle.Value, action.DurationCycles.Value, out startTime, out endTime, out duration);

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

				case StoreBundleData.NameStatic:
					var bundleData = (StoreBundleData)action;
					
					AssetBundle bundle = AssetBundle.LoadFromMemory(bundleData.BundleData.GetBytes());
					BundleManager.Instance.AddBundle(bundleData.BundleName, bundle);
					return;

				case EndCycle.NameStatic:
					Helper.MaxCycle += 1;
					return;

				case KS.SceneActions.ChangeRenderSettings.NameStatic:
					ChangeRenderSettings((ChangeRenderSettings)action);
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

		private void ChangeRenderSettings(ChangeRenderSettings settings)
		{
			if (settings.AmbientEquatorColor != null)
				RenderSettings.ambientEquatorColor = RenderSettings.ambientEquatorColor.ApplyKSVector4(settings.AmbientEquatorColor);

			if (settings.AmbientGroundColor != null)
				RenderSettings.ambientGroundColor = RenderSettings.ambientGroundColor.ApplyKSVector4(settings.AmbientGroundColor);

			if (settings.AmbientIntensity.HasValue)
				RenderSettings.ambientIntensity = settings.AmbientIntensity.Value;

			if (settings.AmbientLight != null)
				RenderSettings.ambientLight = RenderSettings.ambientLight.ApplyKSVector4(settings.AmbientLight);

			if (settings.AmbientMode.HasValue)
				RenderSettings.ambientMode = (UnityEngine.Rendering.AmbientMode)settings.AmbientMode.Value;

			if (settings.AmbientSkyColor != null)
				RenderSettings.ambientSkyColor = RenderSettings.ambientSkyColor.ApplyKSVector4(settings.AmbientSkyColor);

			Cubemap customReflection = BundleManager.Instance.LoadAsset<Cubemap>(settings.CustomReflectionAsset);
			if (customReflection != null)
				RenderSettings.customReflection = customReflection;

			if (settings.DefaultReflectionMode.HasValue)
				RenderSettings.defaultReflectionMode = (UnityEngine.Rendering.DefaultReflectionMode)settings.DefaultReflectionMode.Value;

			if (settings.DefaultReflectionResolution.HasValue)
				RenderSettings.defaultReflectionResolution = settings.DefaultReflectionResolution.Value;

			if (settings.FlareFadeSpeed.HasValue)
				RenderSettings.flareFadeSpeed = settings.FlareFadeSpeed.Value;

			if (settings.FlareStrength.HasValue)
				RenderSettings.flareStrength = settings.FlareStrength.Value;

			if (settings.HasFog.HasValue)
				RenderSettings.fog = settings.HasFog.Value;

			if (settings.FogMode.HasValue)
				RenderSettings.fogMode = (FogMode)settings.FogMode.Value;

			if (settings.FogColor != null)
				RenderSettings.fogColor = RenderSettings.fogColor.ApplyKSVector4(settings.FogColor);

			if (settings.FogDensity.HasValue)
				RenderSettings.fogDensity = settings.FogDensity.Value;

			if (settings.FogStartDistance.HasValue)
				RenderSettings.fogStartDistance = settings.FogStartDistance.Value;

			if (settings.FogEndDistance.HasValue)
				RenderSettings.fogEndDistance = settings.FogEndDistance.Value;

			if (settings.HaloStrength.HasValue)
				RenderSettings.haloStrength = settings.HaloStrength.Value;

			if (settings.ReflectionBounces.HasValue)
				RenderSettings.reflectionBounces = settings.ReflectionBounces.Value;

			if (settings.ReflectionIntensity.HasValue)
				RenderSettings.reflectionIntensity = settings.ReflectionIntensity.Value;

			Material skybox = BundleManager.Instance.LoadAsset<Material>(settings.SkyboxAsset);
			if (skybox != null)
				RenderSettings.skybox = skybox;

			if (settings.SubtractiveShadowColor != null)
				RenderSettings.subtractiveShadowColor = RenderSettings.subtractiveShadowColor.ApplyKSVector4(settings.SubtractiveShadowColor);

			if (settings.SunRef.HasValue)
			{
				string fullRef = settings.SunRef.ToString() + (settings.SunChildRef != null ? "/" + settings.SunChildRef : "");
				RenderSettings.sun = References.Instance.GetGameObject(fullRef).GetComponent<Light>();
			}
		}
	}
}
