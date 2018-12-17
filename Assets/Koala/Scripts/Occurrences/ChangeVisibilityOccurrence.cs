using UnityEngine;
using KS.SceneActions;
using System.Linq;

namespace Koala
{
	public class ChangeVisibilityOccurrence : BaseOccurrence<ChangeVisibilityOccurrence, ChangeVisibility>
	{
		private Renderer _renderer = null;


		public ChangeVisibilityOccurrence() { }

		protected override ChangeVisibility CreateOldConfig()
		{
			var renderer = GetRenderer();

			var oldConfig = new ChangeVisibility();

			if (renderer != null)
			{
				oldConfig.IsVisible = renderer.enabled;
			}
			else
			{
				var childRenderers = renderer.gameObject.GetComponentsInChildren<Renderer>();
				_newConfig.ChildsRenderer = oldConfig.ChildsRenderer = childRenderers;

				oldConfig.ChildsIsVisible = Enumerable.Range(0, childRenderers.Length)
					.Select(i =>
					{
						return childRenderers[i].enabled;
					}).ToArray();
			}

			return oldConfig;
		}

		protected override void ManageSuddenChanges(ChangeVisibility config, bool isForward)
		{
			var renderer = GetRenderer();

			if (renderer != null)
			{
				renderer.enabled = config.IsVisible.Value;
			}
			else
			{
				for (int i = 0; i < config.ChildsRenderer.Length; i++)
				{
					bool isVisible = isForward ? config.IsVisible.Value : config.ChildsIsVisible[i];
					config.ChildsRenderer[i].enabled = isVisible;
				}
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
