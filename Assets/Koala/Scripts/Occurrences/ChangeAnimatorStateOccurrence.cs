using UnityEngine;
using Chronos;

namespace Koala
{
	public class ChangeAnimatorStateOccurrence : Occurrence
	{
		private float _currentNormalizedTime;
		private int _oldStateHash;
		private Animator _animator = null;

		private string _reference;
		private Director.ChangeAnimatorStateConfig _newConfig;


		public ChangeAnimatorStateOccurrence(string reference, Director.ChangeAnimatorStateConfig newConfig)
		{
			_reference = reference;
			_newConfig = newConfig;
		}

		public override void Forward()
		{
			if (_animator == null)
			{
				_animator = References.Instance.GetGameObject(_reference).GetComponent<Animator>();
			}

			var currentAnimatorState = _animator.GetCurrentAnimatorStateInfo(_newConfig.Layer);
			_oldStateHash = currentAnimatorState.shortNameHash;
			if (_newConfig.SaveCurrentNormalizedTime)
			{
				_currentNormalizedTime = currentAnimatorState.normalizedTime.GetFractionalPart();
			}

			_animator.Play(_newConfig.NewStateName, _newConfig.Layer, _newConfig.NewNormalizedTime);
		}

		public override void Backward()
		{
			float normalizedTime = _newConfig.SaveCurrentNormalizedTime ? _currentNormalizedTime : 0.0f;
			_animator.Play(_oldStateHash, _newConfig.Layer, normalizedTime);
		}
	}
}
