using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class DestroyOccurrence : BaseOccurrence<DestroyOccurrence, Destroy>
	{
		private GameObject _go = null;
		private Transform _parent = null;


		public DestroyOccurrence() { }

		protected override Destroy CreateOldConfig()
		{
			_go = References.Instance.GetGameObject(_newConfig.FullRef);
			_parent = _go.transform.parent;

			var oldConfig = new Destroy();

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Destroy config, bool isForward)
		{
			if (isForward)
				Destroy(_go, _newConfig.FullRef, _newConfig.ChildRef == null);
			else
				Restore(_go, _parent, _newConfig.FullRef, _newConfig.ChildRef == null);
		}

		public static void Destroy(GameObject go, string reference, bool editReferences)
		{
			go.transform.SetParent(Helper.RootDestroyedGameObject.transform, true);
			if (editReferences) References.Instance.RemoveGameObject(reference);
		}

		public static void Restore(GameObject go, Transform parent, string reference, bool editReferences)
		{
			go.transform.SetParent(parent, true);
			if (editReferences) References.Instance.AddGameObject(reference, go);
		}
	}
}
