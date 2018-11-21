using UnityEngine;

namespace Koala
{
	public class InstantiateOccurrence : BaseOccurrence<InstantiateOccurrence, Director.InstantiateConfig>
	{
		private GameObject _createdGO;


		public InstantiateOccurrence() { }

		protected override Director.InstantiateConfig CreateOldConfig()
		{
			return new Director.InstantiateConfig();
		}

		protected override void ManageSuddenChanges(Director.InstantiateConfig config, bool isForward)
		{
			if (isForward)
			{
				GameObject parent = config.ParentReference == null
					? config.DefaultParent
					: References.Instance.GetGameObject(config.ParentReference);

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

				_createdGO.transform.localPosition = config.Position;
				if (config.Rotation.HasValue)
					_createdGO.transform.localEulerAngles = config.Rotation.Value;
				if (config.Scale.HasValue)
					_createdGO.transform.localScale = config.Scale.Value;

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
