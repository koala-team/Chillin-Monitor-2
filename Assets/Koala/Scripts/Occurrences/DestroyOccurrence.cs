using UnityEngine;

namespace Koala
{
	public class DestroyOccurrence : BaseOccurrence<DestroyOccurrence, Director.DestroyConfig>
	{
		private GameObject _go = null;


		public DestroyOccurrence() { }

		protected override Director.DestroyConfig CreateOldConfig()
		{
			var go = GetGameObject();

			var oldConfig = new Director.DestroyConfig
			{
				Parent = go.transform.parent,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Director.DestroyConfig config, bool isForward)
		{
			var go = GetGameObject();

			go.transform.SetParent(isForward ? Helper.RootDestroyedGameObject.transform : config.Parent, true);

			go.SetActive(!isForward);

			if (isForward)
				References.Instance.RemoveGameObject(_reference);
			else
				References.Instance.AddGameObject(_reference, go);
		}

		private GameObject GetGameObject()
		{
			if (_go == null)
				_go = References.Instance.GetGameObject(_reference);
			return _go;
		}
	}
}
