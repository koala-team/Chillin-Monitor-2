using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class DestroyOccurrence : BaseOccurrence<DestroyOccurrence, Destroy>
	{
		private GameObject _go = null;


		public DestroyOccurrence() { }

		protected override Destroy CreateOldConfig()
		{
			var go = GetGameObject();

			var oldConfig = new Destroy
			{
				Parent = go.transform.parent,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Destroy config, bool isForward)
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
