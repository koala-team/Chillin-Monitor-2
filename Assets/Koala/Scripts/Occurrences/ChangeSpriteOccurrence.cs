using UnityEngine;
using DG.Tweening;
using KS.SceneActions;

namespace Koala
{
	public class ChangeSpriteOccurrence : BaseOccurrence<ChangeSpriteOccurrence, ChangeSprite>
	{
		private SpriteRenderer _spriteRenderer = null;


		public ChangeSpriteOccurrence() { }

		protected override ChangeSprite CreateOldConfig()
		{
			SpriteRenderer spriteRenderer = GetSpriteRenderer();
			
			var oldConfig = new ChangeSprite
			{
				Sprite = spriteRenderer.sprite,
				SpriteAsset = _newConfig.SpriteAsset,
			};

			if (_newConfig.Color != null)
				oldConfig.Color = spriteRenderer.color.ToKSVector4();

			if (_newConfig.FlipX.HasValue)
				oldConfig.FlipX = spriteRenderer.flipX;

			if (_newConfig.FlipY.HasValue)
				oldConfig.FlipY = spriteRenderer.flipY;

			if (_newConfig.Order.HasValue)
				oldConfig.Order = spriteRenderer.sortingOrder;

			return oldConfig;
		}

		protected override void ManageTweens(ChangeSprite config, bool isForward)
		{
			SpriteRenderer spriteRenderer = GetSpriteRenderer();

			if (config.Color != null)
			{
				DOTween.To(
					() => spriteRenderer.color,
					x => spriteRenderer.color = x,
					spriteRenderer.color.ApplyKSVector4(config.Color),
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeSprite config, bool isForward)
		{
			SpriteRenderer spriteRenderer = GetSpriteRenderer();

			if (config.SpriteAsset != null)
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
