﻿using TMPro;
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

		public static TMP_FontAsset GetFont(string name)
		{
			FontItem fontItem = System.Array.Find(Fonts, fi => fi.Name == name);
			if (fontItem != null)
				return fontItem.Font;
			return null;
		}
	}
}
