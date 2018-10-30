using UnityEngine;

namespace Koala
{
	public class DestroyOccurrence : Occurrence
	{
		private GameObject _go = null;

		private string _reference;

		public DestroyOccurrence(string reference)
		{
			_reference = reference;
		}

		public override void Forward()
		{
			if (_go == null)
			{
				_go = References.Instance.GetGameObject(_reference);
			}
			
			_go.SetActive(false);
			References.Instance.RemoveGameObject(_reference);
		}

		public override void Backward()
		{
			_go.SetActive(true);
			References.Instance.AddGameObject(_reference, _go);
		}
	}
}
