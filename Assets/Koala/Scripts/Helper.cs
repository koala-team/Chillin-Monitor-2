using KS.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Koala
{
	public static class Helper
	{
		public static float CycleDuration { get; set; }
		public static GameObject RootGameObject { get; set; }
		public static GameObject RootDestroyedGameObject { get; set; }
		public static GameObject UserCanvasGameObject { get; set; }
		public static FontItem[] Fonts { get; set; }
		public static PlayersBoard PlayersBoard { get; set; }
		public static Protocol Protocol { get; set; }
		public static bool ReplayMode { get; set; }
		public static bool GameStarted { get; set; }
		public static float MaxCycle { get; set; }
		public static byte[] ReplayBytes { get; set; }

		private static readonly Assembly _asm = Assembly.GetExecutingAssembly();
		public static Assembly Assembly => _asm;

		public static int MainCameraRef => int.MinValue;


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
		}

		public static void SetAnimatorsTimeScale(GameObject root)
		{
			float timeScale = Timeline.Instance.TimeScale;
			foreach (var animator in root.GetComponentsInChildren<Animator>())
			{
				try
				{
					animator.SetFloat("monitorTimeScale", timeScale / CycleDuration);
				}
				catch
				{
				}
			}
		}

		public static void SetAudioSourcesTimeScale(GameObject root)
		{
			float timeScale = Timeline.Instance.TimeScale;
			foreach (var audioSource in root.GetComponentsInChildren<AudioSource>())
			{
				try
				{
					audioSource.pitch = timeScale;
				}
				catch
				{
				}
			}
		}

		public static TMP_FontAsset GetFont(string name)
		{
			FontItem fontItem = System.Array.Find(Fonts, fi => fi.Name == name);
			if (fontItem != null)
				return fontItem.Font;
			return null;
		}

		public static T Cast<T>(object o)
		{
			return (T)o;
		}

		public static void AddParticleSystemManager(GameObject go)
		{
			foreach (var component in go.GetComponentsInChildren<ParticleSystem>())
				component.gameObject.AddComponent<ParticleSystemManager>();
		}

		public static async Task<KS.KSObject> ProcessBuffer(byte[] buffer)
		{
			await new WaitForBackgroundThread();

			return await Task.Run<KS.KSObject>(() =>
			{
				Message message = new Message();
				message.Deserialize(buffer);

				var baseMessageType = Helper.Assembly.GetType("KS.Messages." + message.Type);
				KS.KSObject baseMessage = Activator.CreateInstance(baseMessageType) as KS.KSObject;
				baseMessage.Deserialize(message.Payload.GetBytes());

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
			});
		}
	}
}
