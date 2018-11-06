using UnityEngine;

namespace Koala
{
	public class DestroyOccurrence : Occurrence
	{
		private GameObject _go = null;
		private Transform _parent;

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
				_parent = _go.transform.parent;
			}

			_go.SetActive(false);
			_go.transform.parent = Helper.RootDestroyedGameObject.transform;
			References.Instance.RemoveGameObject(_reference);
		}

		public override void Backward()
		{
			_go.transform.parent = _parent;
			_go.SetActive(true);
			References.Instance.AddGameObject(_reference, _go);
		}
	}
}
