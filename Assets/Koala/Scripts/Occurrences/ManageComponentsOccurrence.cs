using UnityEngine;

namespace Koala
{
	public class ManageComponentsOccurrence : BaseOccurrence<ManageComponentsOccurrence, Director.ManageComponentsConfig>
	{
		private GameObject _gameObject;
		private MonoBehaviour _component;


		public ManageComponentsOccurrence() { }

		protected override Director.ManageComponentsConfig CreateOldConfig()
		{
			var oldConfig = new Director.ManageComponentsConfig();

			if (_newConfig.Add.HasValue && _newConfig.Add.Value)
				oldConfig.Add = false;

			if (_newConfig.IsActive.HasValue)
			{
				var component = GetComponent();
				oldConfig.IsActive = component.enabled;
			}

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Director.ManageComponentsConfig config, bool isForward)
		{
			if (config.Add.HasValue)
			{
				if (config.Add.Value)
					CreateComponent();
				else
					GameObject.Destroy(GetComponent());
			}

			if (config.IsActive.HasValue)
			{
				GetComponent().enabled = config.IsActive.Value;
			}
		}

		private GameObject GetGameObject()
		{
			if (_gameObject == null)
				_gameObject = References.Instance.GetGameObject(_reference);
			return _gameObject;
		}

		private MonoBehaviour GetComponent()
		{
			var gameObject = GetGameObject();

			if (_component == null)
			{
				switch (_newConfig.Type)
				{
					case EComponentType.ParticleSystemManager:
						_component = gameObject.GetComponent<ParticleSystemManager>();
						break;
					default:
						throw new System.NotSupportedException("Type is not supported!");
				}
			}

			return _component;
		}

		private void CreateComponent()
		{
			var gameObject = GetGameObject();
			
			switch (_newConfig.Type)
			{
				case EComponentType.ParticleSystemManager:
					_component = gameObject.AddComponent<ParticleSystemManager>();
					break;
				default:
					throw new System.NotSupportedException("Type is not supported!");
			}
		}
	}
}
