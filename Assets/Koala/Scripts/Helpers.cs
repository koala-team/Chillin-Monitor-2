using Chronos;
using UnityEngine;

namespace Koala
{
	public static class Helper
	{
		public static float CycleDuration { get; set; }
		public static GameObject RootGameObject { get; set; }
		public static GameObject UserCanvasGameObject { get; set; }

		public static float GetCycleTime(float cycle)
		{
			return cycle * CycleDuration;
		}

		public static void GetCyclesDurationTime(float startCycle, float durationCycles,
			out float startTime, out float endTime, out float duration)
		{
			startTime = GetCycleTime(startCycle);
			endTime = GetCycleTime(startCycle + durationCycles);
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
	}
}
