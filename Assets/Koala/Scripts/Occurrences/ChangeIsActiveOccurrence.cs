using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ChangeIsActiveOccurrence : BaseOccurrence<ChangeIsActiveOccurrence, ChangeIsActive>
	{
		private GameObject _gameObject;


		public ChangeIsActiveOccurrence() { }

		protected override ChangeIsActive CreateOldConfig()
		{
			var gameObject = GetGameObject();

			var oldConfig = new ChangeIsActive
			{
				IsActive = gameObject.activeSelf,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(ChangeIsActive config, bool isForward)
		{
			var gameObject = GetGameObject();

			gameObject.SetActive(config.IsActive.Value);
		}

		private GameObject GetGameObject()
		{
			if (_gameObject == null)
				_gameObject = References.Instance.GetGameObject(_reference);
			return _gameObject;
		}
	}
}
