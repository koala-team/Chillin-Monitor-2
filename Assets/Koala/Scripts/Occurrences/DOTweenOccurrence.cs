namespace Koala
{
	public interface IDOTweenOccurrence<T, C>
	{
		void Init(string reference,
			float duration, C newConfig,
			bool isForward, T forwardOccurrence);
	}

	public abstract class DOTweenOccurrence<T, C> : Occurrence, IDOTweenOccurrence<T, C>
		where T : DOTweenOccurrence<T, C>
		where C : class
	{
		public C OldConfig { get; set; }

		protected string _reference;
		protected float _duration;
		protected C _newConfig;
		private bool _isForward;
		protected T _forwardOccurrence;


		public DOTweenOccurrence() { }

		public void Init(string reference,
			float duration, C newConfig,
			bool isForward, T forwardOccurrence)
		{
			_reference = reference;
			_duration = duration;
			_newConfig = newConfig;
			_isForward = isForward;
			_forwardOccurrence = forwardOccurrence;
		}

		public override void Forward()
		{
			if (_isForward)
			{
				OldConfig = CreateOldConfig();

				ManageSuddenChanges(_newConfig, true);
				ManageTweens(_newConfig, true);
			}
		}

		public override void Backward()
		{
			if (!_isForward)
			{
				ManageTweens(_forwardOccurrence.OldConfig, false);
			}
			else
			{
				ManageSuddenChanges(OldConfig, false);
			}
		}

		protected abstract C CreateOldConfig();
		protected virtual void ManageTweens(C config, bool isForward) { }
		protected virtual void ManageSuddenChanges(C config, bool isForward) { }
	}
}
