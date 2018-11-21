using UnityEngine;

namespace Koala
{
	public class ChangeAnimatorVariableOccurrence : BaseOccurrence<ChangeAnimatorVariableOccurrence, Director.ChangeAnimatorVariableConfig>
	{
		private Animator _animator = null;


		public ChangeAnimatorVariableOccurrence() { }

		protected override Director.ChangeAnimatorVariableConfig CreateOldConfig()
		{
			var oldConfig = new Director.ChangeAnimatorVariableConfig()
			{
				VarName = _newConfig.VarName,
				VarType = _newConfig.VarType,
				ValueObject = GetAnimatorVar(_newConfig),
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Director.ChangeAnimatorVariableConfig config, bool isForward)
		{
			ChangeAnimatorVar(config);
		}

		private void ChangeAnimatorVar(Director.ChangeAnimatorVariableConfig config)
		{
			var animator = GetAnimator();

			switch (config.VarType)
			{
				case EAnimatorVariableType.Int:
					animator.SetInteger(config.VarName, (int)config.ValueObject);
					break;
				case EAnimatorVariableType.Float:
					animator.SetFloat(config.VarName, (float)config.ValueObject);
					break;
				case EAnimatorVariableType.Bool:
					animator.SetBool(config.VarName, (bool)config.ValueObject);
					break;
				case EAnimatorVariableType.Trigger:
					animator.SetTrigger(config.VarName);
					break;
				default:
					throw new System.NotSupportedException("varType value not supported!");
			}
		}

		private object GetAnimatorVar(Director.ChangeAnimatorVariableConfig config)
		{
			var animator = GetAnimator();

			switch (config.VarType)
			{
				case EAnimatorVariableType.Int:
					return animator.GetInteger(config.VarName);
				case EAnimatorVariableType.Float:
					return animator.GetFloat(config.VarName);
				case EAnimatorVariableType.Bool:
					return animator.GetBool(config.VarName);
				case EAnimatorVariableType.Trigger:
					return null;
				default:
					throw new System.NotSupportedException("varType value not supported!");
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
