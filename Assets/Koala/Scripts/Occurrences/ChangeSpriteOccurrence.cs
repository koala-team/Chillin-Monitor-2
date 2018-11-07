﻿using UnityEngine;
using DG.Tweening;

namespace Koala
{
	public class ChangeSpriteOccurrence : BaseOccurrence<ChangeSpriteOccurrence, Director.ChangeSpriteConfig>
	{
		private SpriteRenderer _spriteRenderer = null;


		public ChangeSpriteOccurrence() { }

		protected override Director.ChangeSpriteConfig CreateOldConfig()
		{
			SpriteRenderer spriteRenderer = GetSpriteRenderer();
			
			var oldConfig = new Director.ChangeSpriteConfig
			{
				Sprite = spriteRenderer.sprite
			};

			if (_newConfig.Color != null)
				oldConfig.Color = spriteRenderer.color.ToChangeVector4Config();

			if (_newConfig.FlipX.HasValue)
				oldConfig.FlipX = spriteRenderer.flipX;

			if (_newConfig.FlipY.HasValue)
				oldConfig.FlipY = spriteRenderer.flipY;

			if (_newConfig.Order.HasValue)
				oldConfig.Order = spriteRenderer.sortingOrder;

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangeSpriteConfig config, bool isForward)
		{
			SpriteRenderer spriteRenderer = GetSpriteRenderer();

			if (config.Color != null)
			{
				DOTween.To(
					() => spriteRenderer.color,
					x => spriteRenderer.color = x,
					spriteRenderer.color.ApplyChangeVector4Config(config.Color),
					_duration).RegisterChronosTimeline(isForward);
			}
		}

		protected override void ManageSuddenChanges(Director.ChangeSpriteConfig config, bool isForward)
		{
			SpriteRenderer spriteRenderer = GetSpriteRenderer();

			if (_newConfig.BundleName != null && _newConfig.AssetName != null)
				spriteRenderer.sprite = config.Sprite;

			if (config.FlipX.HasValue)
				spriteRenderer.flipX = config.FlipX.Value;

			if (config.FlipY.HasValue)
				spriteRenderer.flipY = config.FlipY.Value;

			if (config.Order.HasValue)
				spriteRenderer.sortingOrder = config.Order.Value;
		}

		private SpriteRenderer GetSpriteRenderer()
		{
			if (_spriteRenderer == null)
				_spriteRenderer = References.Instance.GetGameObject(_reference).GetComponent<SpriteRenderer>();
			return _spriteRenderer;
		}
	}
}