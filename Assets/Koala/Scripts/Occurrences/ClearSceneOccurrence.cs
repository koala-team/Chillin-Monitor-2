using KS.SceneActions;
using System.Collections.Generic;
using UnityEngine;

namespace Koala
{
	public class ClearSceneOccurrence : BaseOccurrence<ClearSceneOccurrence, ClearScene>
	{
		private bool _firstTime = true;
		private string[] _refs;
		private GameObject[] _referencesGameObjects;
		private List<GameObject> _deletedGameObjects = new List<GameObject>();



		public ClearSceneOccurrence() { }

		protected override ClearScene CreateOldConfig()
		{
			return new ClearScene();
		}

		protected override void ManageSuddenChanges(ClearScene config, bool isForward)
		{
			if (isForward)
			{
				if (_firstTime)
				{
					_refs = References.Instance.GetRefs();
					_referencesGameObjects = References.Instance.GetGameObjects();
				}
				References.Instance.ResetMaps();

				foreach (Transform transform in Helper.RootGameObject.transform)
				{
					var go = transform.gameObject;
					DestroyOccurrence.Destroy(go, null, false);
					if (_firstTime) _deletedGameObjects.Add(go);
				}

				if (_firstTime) _firstTime = false;
			}
			else
			{
				foreach (var go in _deletedGameObjects)
					DestroyOccurrence.Restore(go, Helper.RootGameObject.transform, null, false);

				for (int i = 0; i < _refs.Length; i++)
					References.Instance.AddGameObject(_refs[i], _referencesGameObjects[i]);
			}
		}
	}
}
