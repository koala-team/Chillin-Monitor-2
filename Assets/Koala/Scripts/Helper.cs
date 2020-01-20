using KS.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Koala
{
	public static class Helper
	{
		private static string _gameName;
		private static float _cycleDuration;
		private static float _maxCycle;
		private static float _maxEndTime;
		private static bool _replayMode;

		public static float CycleDuration
		{
			get { return _cycleDuration; }
			set { _cycleDuration = value; GlobalBlackboard["CycleDuration"] = value; }
		}
		public static GameObject RootGameObject { get; set; }
		public static GameObject RootDestroyedGameObject { get; set; }
		public static GameObject UserCanvasGameObject { get; set; }
		public static FontItem[] Fonts { get; set; }
		public static PlayersBoard PlayersBoard { get; set; }
		public static Protocol Protocol { get; set; }
		public static bool ReplayMode
		{
			get { return _replayMode; }
			set { _replayMode = value; GlobalBlackboard["ReplayMode"] = value; }
		}
		public static bool GameStarted { get; set; }
		public static float MaxCycle
		{
			get { return _maxCycle; }
			set { _maxCycle = value; GlobalBlackboard["MaxCycle"] = value; }
		}
		public static float MaxEndTime
		{
			get { return _maxEndTime; }
			set { _maxEndTime = value; GlobalBlackboard["MaxEndTime"] = value; }
		}
		public static string GameName
		{
			get { return _gameName; }
			set { _gameName = value; GlobalBlackboard["GameName"] = value; }
		}
		public static byte[] ReplayBytes { get; set; }
		public static bool WebGLLoadReplayLoaded { get; set; } = false;
		public static NodeCanvas.Framework.Blackboard GlobalBlackboard { get; set; }

		private static readonly Assembly _asm = Assembly.GetExecutingAssembly();
		public static Assembly Assembly => _asm;
		private static readonly MethodInfo _genericCastMethodInfo = typeof(Helper).GetMethod("GenericCast");

		public static int MainCameraRef => 0;

		private static readonly float MIN_OCCURRENCE_DURATION = 1e-6f;


		public static float GetCycleTime(float cycle)
		{
			return cycle * CycleDuration;
		}

		public static void GetCyclesDurationTime(float startCycle, float durationCycles,
			out float startTime, out float endTime, out float duration)
		{
			startTime = GetCycleTime(startCycle + MaxCycle);
			endTime = GetCycleTime(startCycle + durationCycles + MaxCycle);
			duration =  endTime - startTime;

			if (duration < MIN_OCCURRENCE_DURATION)
			{
				duration = MIN_OCCURRENCE_DURATION;
				endTime = startTime + duration;
			}
		}

		public static void SetAnimatorsTimeScale(GameObject root)
		{
			float timeScale = Timeline.Instance.TimeScale;
			foreach (var animator in root.GetComponentsInChildren<Animator>(false))
			{
				animator.SetFloat("monitorTimeScale", timeScale / CycleDuration);
			}
		}

		public static void KeepAnimatorControllerStateOnDisable(GameObject root)
		{
			Queue<bool> activeStatues = new Queue<bool>(); ;

			foreach (var animator in root.GetComponentsInChildren<Animator>(true))
			{
				// TODO: Remove this when the error fixed
				// https://trello.com/c/bLipTCpr
				bool activeInHierarchy = animator.gameObject.activeInHierarchy;
				if (!activeInHierarchy)
				{
					// Active all parents til this object become active
					activeStatues = new Queue<bool>();
					GameObject go = animator.gameObject;
					while (!go.activeInHierarchy)
					{
						activeStatues.Enqueue(go.activeSelf);
						go.SetActive(true);
						go = go.transform.parent.gameObject;
					}
				}

				animator.keepAnimatorControllerStateOnDisable = true;

				// Restore active statuses
				if (!activeInHierarchy)
				{
					GameObject go = animator.gameObject;
					while (activeStatues.Count > 0)
					{
						bool activeStatus = activeStatues.Dequeue();
						go.SetActive(activeStatus);
						go = go.transform.parent.gameObject;
					}
				}
			}
		}

		public static void SetAudioSourcesTimeScale(GameObject root)
		{
			float timeScale = Timeline.Instance.TimeScale;
			foreach (var audioSource in root.GetComponentsInChildren<AudioSource>(true))
			{
				audioSource.pitch = timeScale;
			}
		}

		public static void AddParticleSystemManager(GameObject go)
		{
			foreach (var component in go.GetComponentsInChildren<ParticleSystem>(true))
			{
				component.gameObject.AddComponent<ParticleSystemManager>();
			}
		}

		public static void AddSliderParts(GameObject go)
		{
			foreach (var component in go.GetComponentsInChildren<Slider>(true))
			{
				component.gameObject.AddComponent<SliderParts>();
			}
		}

		public static TMP_FontAsset GetFont(string name)
		{
			FontItem fontItem = System.Array.Find(Fonts, fi => fi.Name == name);
			if (fontItem != null)
				return fontItem.Font;
			return null;
		}

		public static async Task<KS.KSObject> ProcessBuffer(byte[] buffer)
		{
#if !UNITY_WEBGL || UNITY_EDITOR
			await new WaitForBackgroundThread();

			return await Task.Run<KS.KSObject>(() =>
			{
#endif
				Message message = new Message();
				message.Deserialize(buffer);

				var baseMessageType = Helper.Assembly.GetType("KS.Messages." + message.Type);
				KS.KSObject baseMessage = Activator.CreateInstance(baseMessageType) as KS.KSObject;
				baseMessage.Deserialize(message.Payload.ISOGetBytes());

				if (baseMessage.Name() == SceneActions.NameStatic)
				{
					var sceneActions = (SceneActions)baseMessage;
					sceneActions.ParsedActions = new List<KS.KSObject>(sceneActions.ActionTypes.Count);

					for (int i = 0; i < sceneActions.ActionTypes.Count; i++)
					{
						var action = KS.KSObject.GetAction(sceneActions.ActionTypes[i], sceneActions.ActionPayloads[i]);
						sceneActions.ParsedActions.Add(action);
					}
				}

				return baseMessage;
#if !UNITY_WEBGL || UNITY_EDITOR
			});
#endif
		}

		public static float WrapAngle(float x)
		{
			while (x >= 180) x -= 360;
			while (x < -180) x += 360;

			return x;
		}

		public static Vector3 WrapAngles(Vector3 v)
		{
			return new Vector3(WrapAngle(v.x), WrapAngle(v.y), WrapAngle(v.z));
		}

		public static void UpdatePostProcessState()
		{
			try
			{
				var volume = GameObject.Find("Main Camera").GetComponent<PostProcessVolume>();
				volume.enabled = PlayerConfigs.IsPostProcessActive;
			}
			catch { }
		}

		public static string SplitOnCapitalLetters(string s)
		{
			var words =
				Regex.Matches(s, @"([A-Z][a-z]+)")
				.Cast<Match>()
				.Select(m => m.Value);

			return string.Join(" ", words);
		}

		public static void UpdateScreenResolution()
		{
			var resolution = Screen.resolutions[PlayerConfigs.ResolutionIndex];
			var fullScreenMode = PlayerConfigs.FullScreenMode;

			Screen.SetResolution(resolution.width, resolution.height, fullScreenMode);
		}

		public static MethodInfo MakeGenericMethod(MethodInfo methodInfo, params Type[] types)
		{
			return methodInfo.MakeGenericMethod(types);
		}

		public static Delegate MakeGenericDelegate(Type delegateType, object caller, MethodInfo methodInfo, params Type[] types)
		{
			Type delegateGenericType = delegateType.MakeGenericType(types);
			return Delegate.CreateDelegate(delegateGenericType, caller, methodInfo);
		}

		public static T GenericCast<T>(object o)
		{
			return (T)o;
		}

		public static object DynamicCast(object obj, Type type)
		{
			var castMethod = _genericCastMethodInfo.MakeGenericMethod(type);
			return castMethod.Invoke(null, new object[] { obj });
		}
	}
}
