using DG.Tweening;
using System;
using System.Collections.Generic;

namespace Koala
{
	public sealed class Timeline
	{
		public static readonly int TIME_TRUNCATE_DECIMALS = 3;

		#region Singleton
		private static readonly Timeline instance = new Timeline();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static Timeline()
		{
		}

		private Timeline()
		{
		}

		public static Timeline Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		private SortedList<int, LinkedList<Occurrence>> _occurrences;
		private int _occurrencesIndex;
		private float _time;
		private float _timeScale;

		public float Time
		{
			get { return _time; }
			private set
			{
				_time = value;
				Helper.GlobalBlackboard["Time"] = value;
				Helper.GlobalBlackboard["Cycle"] = Cycle;
			}
		}
		public float TimeScale
		{
			get { return _timeScale; }
			private set { _timeScale = value; Helper.GlobalBlackboard["TimeScale"] = value; }
		}
		public float Cycle => Helper.CycleDuration == 0 ? 0 : Time / Helper.CycleDuration;

		public void Reset()
		{
			_occurrences = new SortedList<int, LinkedList<Occurrence>>();
			_occurrences[int.MinValue] = new LinkedList<Occurrence>();
			_occurrences[int.MinValue + 1] = new LinkedList<Occurrence>();
			_occurrences[int.MaxValue - 1] = new LinkedList<Occurrence>();
			_occurrences[int.MaxValue] = new LinkedList<Occurrence>();

			_occurrencesIndex = 1;

			Time = -0.00001f;
			TimeScale = 1;
		}

		public void Schedule(float time, Occurrence occurrence)
		{
			int convertedTime = ConvertTime(time);

			if (!_occurrences.ContainsKey(convertedTime))
				_occurrences[convertedTime] = new LinkedList<Occurrence>();

			_occurrences[convertedTime].AddLast(occurrence);
		}

		// return true if force pause
		public bool Update(float unscaledDeltaTime, float maxTime)
		{
			// Check time don't go behind zero and above maxTime
			if ((Time <= 0 && TimeScale < 0) || (Time >= maxTime && TimeScale > 0))
			{
				ChangeTimeScale(-TimeScale);
				return true;
			}

			float deltaTime = unscaledDeltaTime * TimeScale;
			float newTime = Time + deltaTime;

			if (deltaTime == 0) return false;

			// Check new time don't go behind zero and above maxTime
			if (newTime < 0 && TimeScale < 0) newTime = 0;
			if (newTime > maxTime && TimeScale > 0) newTime = maxTime;

			// Update Time
			float prevTime = Time;
			Time = newTime;

			// Update Tweens
			TweensManager.Instance.UpdateTweensIsBackward(deltaTime);
			DOTween.ManualUpdate(Math.Abs(deltaTime), 0);

			// Execute Occurrences
			int step = TimeScale > 0 ? 1 : -1;
			int convertedPrevTime = ConvertTime(prevTime);
			int convertedTime = ConvertTime(Time);

			if (_occurrences.Count > 4)
			{
				int tempIndex = _occurrencesIndex;
				if (!CheckIsTimeBetween(convertedPrevTime, _occurrences.Keys[tempIndex], convertedTime))
					tempIndex += step;

				while (CheckIsTimeBetween(convertedPrevTime, _occurrences.Keys[tempIndex], convertedTime))
				{
					ExecuteOccurrences(_occurrences[_occurrences.Keys[tempIndex]]);
					tempIndex += step;
					_occurrencesIndex = tempIndex;
				}

			}

			return false;
		}

		public void ChangeTimeScale(float amount)
		{
			TimeScale += amount;

			Helper.SetAnimatorsTimeScale(Helper.RootGameObject);
			Helper.SetAudioSourcesTimeScale(Helper.RootGameObject);
		}

		private bool CheckIsTimeBetween(int prevTime, int checkTime, int newTime)
		{
			return TimeScale > 0
					? prevTime <= checkTime && checkTime < newTime
					: prevTime > checkTime && checkTime >= newTime;
		}

		private void ExecuteOccurrences(LinkedList<Occurrence> occurrences)
		{
			var item = TimeScale > 0 ? occurrences.First : occurrences.Last;
			while (item != null)
			{
				if (TimeScale > 0)
					item.Value.Forward();
				else
					item.Value.Backward();

				item = TimeScale > 0 ? item.Next : item.Previous;
			}
		}

		private int ConvertTime(float time)
		{
			return (int)Math.Floor(time * Math.Pow(10, TIME_TRUNCATE_DECIMALS));
		}
	}
}
