using UnityEngine;

namespace Koala
{
	public class ChangeIsActiveOccurrence : BaseOccurrence<ChangeIsActiveOccurrence, Director.ChangeIsActiveConfig>
	{
		private GameObject _go = null;


		public ChangeIsActiveOccurrence() { }

		protected override Director.ChangeIsActiveConfig CreateOldConfig()
		{
			var go = GetGameObject();

			var oldConfig = new Director.ChangeIsActiveConfig
			{
				IsActive = go.activeSelf,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Director.ChangeIsActiveConfig config, bool isForward)
		{
			var go = GetGameObject();

			go.SetActive(config.IsActive);
		}

		private GameObject GetGameObject()
		{
			if (_go == null)
				_go = References.Instance.GetGameObject(_reference);
			return _go;
		}
	}
}
