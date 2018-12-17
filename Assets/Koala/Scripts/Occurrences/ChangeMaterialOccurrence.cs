using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ChangeMaterialOccurrence : BaseOccurrence<ChangeMaterialOccurrence, ChangeMaterial>
	{
		private Renderer _renderer = null;


		public ChangeMaterialOccurrence() { }

		protected override ChangeMaterial CreateOldConfig()
		{
			Renderer renderer = GetRenderer();

			var oldConfig = new ChangeMaterial
			{
				Index = _newConfig.Index,
				Material = renderer.materials[_newConfig.Index.Value],
				MaterialAsset = _newConfig.MaterialAsset,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(ChangeMaterial config, bool isForward)
		{
			Renderer renderer = GetRenderer();

			if (config.MaterialAsset != null)
			{
				var materials = renderer.materials;
				materials[config.Index.Value] = config.Material;
				renderer.materials = materials;
			}
		}

		private Renderer GetRenderer()
		{
			if (_renderer == null)
				_renderer = References.Instance.GetGameObject(_reference).GetComponent<Renderer>();
			return _renderer;
		}
	}
}
