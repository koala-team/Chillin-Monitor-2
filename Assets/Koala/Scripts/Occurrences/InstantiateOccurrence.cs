using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class InstantiateOccurrence : BaseOccurrence<InstantiateOccurrence, BaseCreation>
	{
		private GameObject _createdGO;


		public InstantiateOccurrence() { }

		protected override BaseCreation CreateOldConfig()
		{
			return new BaseCreation();
		}

		protected override void ManageSuddenChanges(BaseCreation config, bool isForward)
		{
			if (isForward)
			{
				GameObject parent = !config.ParentRef.HasValue
					? config.DefaultParent
					: References.Instance.GetGameObject(config.FullParentRef);

				if (config.GameObject == null)
				{
					_createdGO = new GameObject(_reference);
					_createdGO.transform.parent = parent.transform;
				}
				else
				{
					_createdGO = GameObject.Instantiate(config.GameObject, parent.transform);
					_createdGO.name = _reference;
				}

				References.Instance.AddGameObject(_reference, _createdGO);

				Helper.SetAnimatorsTimeScale(_createdGO);
				Helper.SetAudioSourcesTimeScale(_createdGO);
			}
			else
			{
				GameObject.Destroy(_createdGO);
				References.Instance.RemoveGameObject(_reference);
			}
		}
	}
}
