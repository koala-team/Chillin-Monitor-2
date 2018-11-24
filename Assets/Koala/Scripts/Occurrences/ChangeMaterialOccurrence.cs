using UnityEngine;
using DG.Tweening;

namespace Koala
{
	public class ChangeMaterialOccurrence : BaseOccurrence<ChangeMaterialOccurrence, Director.ChangeMaterialConfig>
	{
		private Renderer _renderer = null;


		public ChangeMaterialOccurrence() { }

		protected override Director.ChangeMaterialConfig CreateOldConfig()
		{
			Renderer renderer = GetRenderer();

			var oldConfig = new Director.ChangeMaterialConfig
			{
				Index = _newConfig.Index,
				Material = renderer.materials[_newConfig.Index],
				MaterialAsset = _newConfig.MaterialAsset,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(Director.ChangeMaterialConfig config, bool isForward)
		{
			Renderer renderer = GetRenderer();

			if (config.MaterialAsset != null)
			{
				var materials = renderer.materials;
				materials[config.Index] = config.Material;
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
