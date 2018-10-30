using UnityEngine;
using Chronos;

namespace Koala
{
	public class SetActiveOccurrence : Occurrence
	{
		private GameObject _go = null;
		private bool _oldValue;

		private string _reference;
		private bool _newValue;

		public SetActiveOccurrence(string reference, bool newValue = false)
		{
			_reference = reference;
			_newValue = newValue;
		}

		public override void Forward()
		{
			if (_go == null)
			{
				_go = References.Instance.GetGameObject(_reference);
			}

			_oldValue = _go.activeInHierarchy;
			_go.SetActive(_newValue);
		}

		public override void Backward()
		{
			_go.SetActive(_oldValue);
		}
	}
}
