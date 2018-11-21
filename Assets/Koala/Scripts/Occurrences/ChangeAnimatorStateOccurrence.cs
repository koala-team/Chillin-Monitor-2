using UnityEngine;

namespace Koala
{
	public class ChangeAnimatorStateOccurrence : BaseOccurrence<ChangeAnimatorStateOccurrence, Director.ChangeAnimatorStateConfig>
	{
		private Animator _animator = null;


		public ChangeAnimatorStateOccurrence() { }

		protected override Director.ChangeAnimatorStateConfig CreateOldConfig()
		{
			var animator = GetAnimator();
			var currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(_newConfig.Layer);

			var oldConfig = new Director.ChangeAnimatorStateConfig()
			{
				StateHash = currentAnimatorStateInfo.shortNameHash,
				Layer = _newConfig.Layer,
				NormalizedTime = currentAnimatorStateInfo.normalizedTime.GetFractionalPart(),
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Director.ChangeAnimatorStateConfig config, bool isForward)
		{
			var animator = GetAnimator();

			if (config.StateHash.HasValue)
			{
				animator.Play(config.StateHash.Value, config.Layer, config.NormalizedTime);
			}
			else
			{
				animator.Play(config.StateName, config.Layer, config.NormalizedTime);
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
