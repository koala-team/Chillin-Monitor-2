using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ChangeAnimatorStateOccurrence : BaseOccurrence<ChangeAnimatorStateOccurrence, ChangeAnimatorState>
	{
		private Animator _animator = null;


		public ChangeAnimatorStateOccurrence() { }

		protected override ChangeAnimatorState CreateOldConfig()
		{
			var animator = GetAnimator();
			var currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(_newConfig.Layer.Value);

			var oldConfig = new ChangeAnimatorState()
			{
				StateHash = currentAnimatorStateInfo.shortNameHash,
				Layer = _newConfig.Layer,
				NormalizedTime = currentAnimatorStateInfo.normalizedTime.GetFractionalPart(),
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(ChangeAnimatorState config, bool isForward)
		{
			var animator = GetAnimator();

			if (config.StateHash.HasValue)
			{
				animator.Play(config.StateHash.Value, config.Layer.Value, config.NormalizedTime.Value);
			}
			else
			{
				animator.Play(config.StateName, config.Layer.Value, config.NormalizedTime.Value);
			}
		}

		private Animator GetAnimator()
		{
			if (_animator == null)
				_animator = References.Instance.GetGameObject(_reference).GetComponent<Animator>();
			return _animator;
		}
	}
}
