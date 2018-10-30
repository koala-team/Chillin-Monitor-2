using Chronos;
using UnityEngine;

namespace Koala
{
	public static class Helper
	{
		public static float CycleDuration { get; set; }
		public static GameObject RootGameObject { get; set; }
		public static GameObject UserCanvasGameObject { get; set; }

		public static float GetCycleTime(float cycle, bool addOffset = false, bool bitForward = true)
		{
			float time = cycle * CycleDuration;
			if (addOffset)
			{
				time += (bitForward ? +1.0f : -1.0f) * Constants.OCCURRENCE_DELAY;
			}
			return time;
		}

		public static void GetCyclesDurationTime(float startCycle, float durationCycles,
			out float startTime, out float endTime, out float duration)
		{
			bool addTimeOffset = false; // durationCycles > 0;
			startTime = GetCycleTime(startCycle, addTimeOffset, true);
			endTime = GetCycleTime(startCycle + durationCycles, addTimeOffset, false);
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
