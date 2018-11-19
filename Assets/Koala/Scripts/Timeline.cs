using DG.Tweening;
using System;
using System.Collections.Generic;

namespace Koala
{
	public sealed class Timeline
	{
		public static readonly int TIME_TRUNCATE_DECIMALS = 3;
		delegate bool comparator(int a, int b, int c);

		#region Definition
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

		public float Time { get; private set; }
		public float TimeScale { get; set; }

		public void Reset()
		{
			_occurrences = new SortedList<int, LinkedList<Occurrence>>();
			_occurrences[int.MinValue] = new LinkedList<Occurrence>();
			_occurrences[int.MinValue + 1] = new LinkedList<Occurrence>();
			_occurrences[int.MaxValue - 1] = new LinkedList<Occurrence>();
			_occurrences[int.MaxValue] = new LinkedList<Occurrence>();

			_occurrencesIndex = 1;

			Time = -0.00001f;
			TimeScale = 0;
		}

		public void Schedule(float time, Occurrence occurrence)
		{
			int convertedTime = ConvertTime(time);

			if (!_occurrences.ContainsKey(convertedTime))
				_occurrences[convertedTime] = new LinkedList<Occurrence>();

			_occurrences[convertedTime].AddLast(occurrence);
		}

		public void Update(float unscaledDeltaTime)
		{
			float deltaTime = unscaledDeltaTime * TimeScale;

			if (deltaTime != 0)
			{
				// Update Time
				float prevTime = Time;
				Time += deltaTime;

				// Update Tweens
				TweensManager.Instance.UpdateTweensIsBackward(deltaTime);
				DOTween.ManualUpdate(Math.Abs(deltaTime), 0);

				// Execute Occurrences
				comparator comparator = (a, b, c) =>
				{
					return TimeScale > 0 ?
							a <= b && b < c :
							a > b && b >= c;
				};
				int step = TimeScale > 0 ? 1 : -1;
				int convertedPrevTime = ConvertTime(prevTime);
				int convertedTime = ConvertTime(Time);

				if (_occurrences.Count > 4)
				{
					int tempIndex = _occurrencesIndex;
					if (!comparator(convertedPrevTime, _occurrences.Keys[tempIndex], convertedTime))
						tempIndex += step;

					while (comparator(convertedPrevTime, _occurrences.Keys[tempIndex], convertedTime))
					{
						ExecuteOccurrences(_occurrences[_occurrences.Keys[tempIndex]]);
						tempIndex += step;
						_occurrencesIndex = tempIndex;
					}

				}

				// Check time don't go behind zero
				if (Time <= 0 && TimeScale < 0)
				{
					TimeScale = 0;
					UnityEngine.Debug.Log("Force Pause");
				}
			}
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
