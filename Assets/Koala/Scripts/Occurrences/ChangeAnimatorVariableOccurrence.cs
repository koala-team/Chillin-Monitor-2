using UnityEngine;

namespace Koala
{
	public class ChangeAnimatorVarOccurrence : Occurrence
	{
		private object _oldValue = null;
		private object _newValue = null;
		private Animator _animator = null;

		private string _reference;
		private Director.ChangeAnimatorVariableConfig _newConfig;


		public ChangeAnimatorVarOccurrence(string reference, Director.ChangeAnimatorVariableConfig newConfig)
		{
			_reference = reference;
			_newConfig = newConfig;

			switch (_newConfig.VarType)
			{
				case EAnimatorVariableType.Int:
					int intTemp;
					int.TryParse(_newConfig.NewValue, out intTemp);
					_newValue = intTemp;
					break;
				case EAnimatorVariableType.Float:
					float floatTemp;
					float.TryParse(_newConfig.NewValue, out floatTemp);
					_newValue = floatTemp;
					break;
				case EAnimatorVariableType.Bool:
					bool boolTemp;
					bool.TryParse(_newConfig.NewValue, out boolTemp);
					_newValue = boolTemp;
					break;
				case EAnimatorVariableType.Trigger:
					_newValue = null;
					break;
				default:
					throw new System.NotSupportedException("varType value not supported!");
			}
		}

		public override void Forward()
		{
			if (_animator == null)
			{
				_animator = References.Instance.GetGameObject(_reference).GetComponent<Animator>();
			}

			_oldValue = GetAnimatorVar();
			
			ChangeAnimatorVar(_newValue);
		}

		public override void Backward()
		{
			ChangeAnimatorVar(_oldValue);
		}

		private void ChangeAnimatorVar(object newValue)
		{
			switch (_newConfig.VarType)
			{
				case EAnimatorVariableType.Int:
					_animator.SetInteger(_newConfig.VarName, (int)newValue);
					break;
				case EAnimatorVariableType.Float:
					_animator.SetFloat(_newConfig.VarName, (float)newValue);
					break;
				case EAnimatorVariableType.Bool:
					_animator.SetBool(_newConfig.VarName, (bool)newValue);
					break;
				case EAnimatorVariableType.Trigger:
					_animator.SetTrigger(_newConfig.VarName);
					break;
			}
		}

		private object GetAnimatorVar()
		{
			switch (_newConfig.VarType)
			{
				case EAnimatorVariableType.Int:
					return _animator.GetInteger(_newConfig.VarName);
				case EAnimatorVariableType.Float:
					return _animator.GetFloat(_newConfig.VarName);
				case EAnimatorVariableType.Bool:
					return _animator.GetBool(_newConfig.VarName);
				case EAnimatorVariableType.Trigger:
				default:
					return null;
			}
		}
	}
}
