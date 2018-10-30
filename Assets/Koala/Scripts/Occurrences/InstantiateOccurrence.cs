using UnityEngine;
using Chronos;

namespace Koala
{
	public class InstantiateOccurrence : Occurrence
	{
		private GameObject _createdGO;

		private string _reference;
		private GameObject _baseGO;
		private Director.InstantiateConfig _config;
		private GameObject _defaultParent;


		public InstantiateOccurrence(
			string reference, GameObject go, Director.InstantiateConfig config, GameObject defaultParent)
		{
			_reference = reference;
			_baseGO = go;
			_config = config;
			_defaultParent = defaultParent;
		}

		public override void Forward()
		{
			GameObject parent = _defaultParent;
			if (_config.ParentReference != null)
				parent = References.Instance.GetGameObject(_config.ParentReference);

			_createdGO = GameObject.Instantiate(_baseGO, parent.transform);

			_createdGO.transform.localPosition = _config.Position;
			if (_config.Rotation.HasValue)
				_createdGO.transform.localEulerAngles = _config.Rotation.Value;
			if (_config.Scale.HasValue)
				_createdGO.transform.localScale = _config.Scale.Value;

			References.Instance.AddGameObject(_reference, _createdGO);

			Helper.SetAnimatorsTimeScale(Helper.RootTimeline.timeScale, _createdGO);
			Helper.SetAudioSourcesTimeScale(Helper.RootTimeline.timeScale, _createdGO);
		}

		public override void Backward()
		{
			GameObject.Destroy(_createdGO);
			References.Instance.RemoveGameObject(_reference);
		}
	}
}
