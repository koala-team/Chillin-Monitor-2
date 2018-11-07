using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Koala
{
	public sealed class TweensManager
	{
		private static readonly int CAPACITY_STEP = 10;
		private static readonly int INITIAL_CAPACITY = 40;

		#region Singleton
		private static readonly TweensManager instance = new TweensManager();

		// Explicit static constructor to tell C# compiler
		// not to mark type as beforefieldinit
		static TweensManager()
		{
		}

		private TweensManager()
		{
		}

		public static TweensManager Instance
		{
			get
			{
				return instance;
			}
		}
		#endregion

		private int _forwardMaxCapacity;
		private int _backwardMaxCapacity;
		private List<Tween> _forwardTweens;
		private List<Tween> _backwardTweens;

		public void Reset()
		{
			_forwardMaxCapacity = INITIAL_CAPACITY;
			_forwardTweens = new List<Tween>(_forwardMaxCapacity);

			_backwardMaxCapacity = INITIAL_CAPACITY;
			_backwardTweens = new List<Tween>(_backwardMaxCapacity);
		}

		public void AddTween(Tween tween, bool isForward)
		{
			CheckCapacity(isForward);
			GetList(isForward).Add(tween);
		}

		public void RemoveTween(Tween tween, bool isForward)
		{
			GetList(isForward).Remove(tween);
		}

		public void UpdateTweensIsBackward(float deltaTime)
		{
			foreach (var t in _forwardTweens)
			{
				if (t != null)
					t.isBackwards = Math.Sign(deltaTime) < 0;
			}

			foreach (var t in _backwardTweens)
			{
				if (t != null)
					t.isBackwards = Math.Sign(-deltaTime) < 0;
			}
		}

		private List<Tween> GetList(bool isForward)
		{
			return isForward ? _forwardTweens : _backwardTweens;
		}

		private void CheckCapacity(bool isForward)
		{
			if (isForward && _forwardTweens.Count == _forwardMaxCapacity)
			{
				_forwardMaxCapacity += CAPACITY_STEP;
				_forwardTweens.Capacity = _forwardMaxCapacity;
			}
			else if (_backwardTweens.Count == _backwardMaxCapacity)
			{
				_backwardMaxCapacity += CAPACITY_STEP;
				_backwardTweens.Capacity = _backwardMaxCapacity;
			}
		}
	}
}
