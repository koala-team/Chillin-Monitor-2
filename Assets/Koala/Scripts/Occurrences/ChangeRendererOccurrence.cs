using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ChangeRendererOccurrence : BaseOccurrence<ChangeRendererOccurrence, ChangeRenderer>
	{
		private GameObject _gameObject;
		private Renderer _renderer;


		public ChangeRendererOccurrence() { }

		protected override ChangeRenderer CreateOldConfig()
		{
			Renderer renderer = GetRenderer();

			var oldConfig = new ChangeRenderer
			{
				MaterialAsset = _newConfig.MaterialAsset,
			};

			if (_newConfig.Enabled.HasValue)
				oldConfig.Enabled = renderer.enabled;

			if (_newConfig.MaterialAsset != null)
			{
				oldConfig.Material = renderer.materials[_newConfig.MaterialIndex.Value];
				oldConfig.MaterialIndex = _newConfig.MaterialIndex;
			}

			if (_newConfig.RenderingLayerMask.HasValue)
				oldConfig.RenderingLayerMask = renderer.renderingLayerMask;

			if (_newConfig.SortingLayerId.HasValue)
				oldConfig.SortingLayerId = renderer.sortingLayerID;

			if (_newConfig.SortingOrder.HasValue)
				oldConfig.SortingOrder = renderer.sortingOrder;

			if (_newConfig.RendererPriority.HasValue)
				oldConfig.RendererPriority = renderer.rendererPriority;

			return oldConfig;
		}

		protected override void ManageSuddenChanges(ChangeRenderer config, bool isForward)
		{
			Renderer renderer = GetRenderer();

			if (config.Enabled.HasValue)
				renderer.enabled = config.Enabled.Value;

			if (config.MaterialAsset != null)
			{
				var materials = renderer.materials;
				materials[config.MaterialIndex.Value] = config.Material;
				renderer.materials = materials;
			}

			if (config.RenderingLayerMask.HasValue)
				renderer.renderingLayerMask = config.RenderingLayerMask.Value;

			if (config.SortingLayerId.HasValue)
				renderer.sortingLayerID = config.SortingLayerId.Value;

			if (config.SortingOrder.HasValue)
				renderer.sortingOrder = config.SortingOrder.Value;

			if (config.RendererPriority.HasValue)
				renderer.rendererPriority = config.RendererPriority.Value;
		}

		private GameObject GetGameObject()
		{
			if (_gameObject == null)
				_gameObject = References.Instance.GetGameObject(_reference);
			return _gameObject;
		}

		private Renderer GetRenderer()
		{
			if (_renderer == null)
				_renderer = GetGameObject().GetComponent<Renderer>();
			return _renderer;
		}
	}
}
