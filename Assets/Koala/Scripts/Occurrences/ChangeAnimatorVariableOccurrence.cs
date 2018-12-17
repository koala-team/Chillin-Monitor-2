using UnityEngine;
using KS.SceneActions;
using DG.Tweening;

namespace Koala
{
	public class ChangeAnimatorVariableOccurrence : BaseOccurrence<ChangeAnimatorVariableOccurrence, ChangeAnimatorVariable>
	{
		private Animator _animator = null;


		public ChangeAnimatorVariableOccurrence() { }

		protected override ChangeAnimatorVariable CreateOldConfig()
		{
			var oldConfig = new ChangeAnimatorVariable()
			{
				VarName = _newConfig.VarName,
				VarType = _newConfig.VarType,
			};

			switch (_newConfig.VarType)
			{
				case EAnimatorVariableType.Int:
					oldConfig.IntValue = _newConfig.IntValue;
					break;
				case EAnimatorVariableType.Float:
					oldConfig.FloatValue = _newConfig.FloatValue;
					break;
				case EAnimatorVariableType.Bool:
					oldConfig.BoolValue = _newConfig.BoolValue;
					break;
				case EAnimatorVariableType.Trigger:
					break;
				default:
					throw new System.NotSupportedException("varType value not supported!");
			}

			return oldConfig;
		}

		protected override void ManageTweens(ChangeAnimatorVariable config, bool isForward)
		{
			var animator = GetAnimator();

			switch (config.VarType)
			{
				case EAnimatorVariableType.Int:
					DOTween.To(
						() => animator.GetInteger(config.VarName),
						x => animator.SetInteger(config.VarName, x),
						config.IntValue.Value,
						_duration).RegisterInTimeline(_startTime, isForward);
					break;
				case EAnimatorVariableType.Float:
					DOTween.To(
						() => animator.GetFloat(config.VarName),
						x => animator.SetFloat(config.VarName, x),
						config.FloatValue.Value,
						_duration).RegisterInTimeline(_startTime, isForward);
					break;
			}
		}

		protected override void ManageSuddenChanges(ChangeAnimatorVariable config, bool isForward)
		{
			var animator = GetAnimator();

			switch (config.VarType)
			{
				case EAnimatorVariableType.Bool:
					animator.SetBool(config.VarName, config.BoolValue.Value);
					break;
				case EAnimatorVariableType.Trigger:
					animator.SetTrigger(config.VarName);
					break;
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
