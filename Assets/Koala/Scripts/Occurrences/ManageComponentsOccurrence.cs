using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ManageComponentsOccurrence : BaseOccurrence<ManageComponentsOccurrence, ManageComponents>
	{
		private GameObject _gameObject;
		private MonoBehaviour _component;


		public ManageComponentsOccurrence() { }

		protected override ManageComponents CreateOldConfig()
		{
			var oldConfig = new ManageComponents
			{
				Type = _newConfig.Type,
			};

			if (_newConfig.Add.HasValue && _newConfig.Add.Value)
				oldConfig.Add = false;

			if (_newConfig.IsActive.HasValue)
			{
				var component = GetComponent(_newConfig.Type);
				oldConfig.IsActive = component.enabled;
			}

			return oldConfig;
		}

		protected override void ManageSuddenChanges(ManageComponents config, bool isForward)
		{
			if (config.Add.HasValue)
			{
				if (config.Add.Value)
					CreateComponent(config.Type);
				else
					GameObject.Destroy(GetComponent(config.Type));
			}

			if (config.IsActive.HasValue)
			{
				GetComponent(config.Type).enabled = config.IsActive.Value;
			}
		}

		private GameObject GetGameObject()
		{
			if (_gameObject == null)
				_gameObject = References.Instance.GetGameObject(_reference);
			return _gameObject;
		}

		private MonoBehaviour GetComponent(string name)
		{
			var gameObject = GetGameObject();

			if (_component == null)
				_component = (MonoBehaviour)gameObject.GetComponent(Helper.Assembly.GetType(name));

			return _component;
		}

		private void CreateComponent(string name)
		{
			var gameObject = GetGameObject();
			
			_component = (MonoBehaviour)gameObject.AddComponent(Helper.Assembly.GetType(name));
		}
	}
}
