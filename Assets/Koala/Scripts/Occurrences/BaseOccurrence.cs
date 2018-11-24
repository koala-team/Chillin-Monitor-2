using System;

namespace Koala
{
	public interface IBaseOccurrence<T, C>
	{
		void Init(string reference,
			float startTime, float endTime, C newConfig,
			bool isForward, T forwardOccurrence);
	}

	public abstract class BaseOccurrence<T, C> : Occurrence, IBaseOccurrence<T, C>
		where T : BaseOccurrence<T, C>
		where C : class
	{
		private static readonly float MIN_DURATION = 0.1e-6f;
		protected float _duration;

		protected string _reference;
		protected float _startTime;
		protected float _endTime;
		protected C _oldConfig;
		protected C _newConfig;
		private bool _isForward;
		protected T _forwardOccurrence;


		public BaseOccurrence() { }

		public void Init(string reference,
			float startTime, float endTime, C newConfig,
			bool isForward, T forwardOccurrence)
		{
			_reference = reference;
			_startTime = startTime;
			_endTime = endTime;
			_newConfig = newConfig;
			_isForward = isForward;
			_forwardOccurrence = forwardOccurrence;

			_duration = Math.Max(MIN_DURATION, Math.Abs(_endTime - _startTime));
		}

		public override void Forward()
		{
			if (_isForward)
			{
				_oldConfig = CreateOldConfig();

				ManageSuddenChanges(_newConfig, true);
				ManageTweens(_newConfig, true);
			}
		}

		public override void Backward()
		{
			if (!_isForward)
			{
				ManageTweens(_forwardOccurrence._oldConfig, false);
			}
			else
			{
				ManageSuddenChanges(_oldConfig, false);
			}
		}

		protected abstract C CreateOldConfig();
		protected virtual void ManageTweens(C config, bool isForward) { }
		protected virtual void ManageSuddenChanges(C config, bool isForward) { }
	}
}
