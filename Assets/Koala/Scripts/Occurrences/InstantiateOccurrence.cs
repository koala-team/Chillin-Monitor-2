using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class InstantiateOccurrence : BaseOccurrence<InstantiateOccurrence, BaseCreation>
	{
		private GameObject _createdGO = null;
		private GameObject _parent = null;


		public InstantiateOccurrence() { }

		protected override BaseCreation CreateOldConfig()
		{
			return new BaseCreation();
		}

		protected override void ManageSuddenChanges(BaseCreation config, bool isForward)
		{
			if (isForward)
			{
				if (_createdGO == null)
				{
					_parent = config.ParentRef.HasValue
						? References.Instance.GetGameObject(config.FullParentRef)
						: config.DefaultParentGO;

					if (config.GameObject == null)
					{
						_createdGO = new GameObject(config.Ref.ToString());
						_createdGO.transform.parent = _parent.transform;
					}
					else
					{
						_createdGO = GameObject.Instantiate(config.GameObject, _parent.transform);
						_createdGO.name = config.Ref.ToString();
					}

					References.Instance.AddGameObject(config.Ref.ToString(), _createdGO);

					Helper.KeepAnimatorControllerStateOnDisable(_createdGO);
					Helper.AddParticleSystemManager(_createdGO);
				}
				else
				{
					DestroyOccurrence.Restore(_createdGO, _parent.transform, config.Ref.ToString(), true);
				}

				Helper.SetAnimatorsTimeScale(_createdGO);
				Helper.SetAudioSourcesTimeScale(_createdGO);
			}
			else
			{
				DestroyOccurrence.Destroy(_createdGO, _newConfig.Ref.ToString(), true);
			}
		}
	}
}
