using UnityEngine;
using DG.Tweening;
using KS.SceneActions;

namespace Koala
{
	public class ChangeSpriteOccurrence : BaseOccurrence<ChangeSpriteOccurrence, ChangeSprite>
	{
		private GameObject _gameObject;
		private SpriteRenderer _spriteRenderer;


		public ChangeSpriteOccurrence() { }

		protected override ChangeSprite CreateOldConfig()
		{
			SpriteRenderer spriteRenderer = GetSpriteRenderer();
			
			var oldConfig = new ChangeSprite
			{
				SpriteAsset = _newConfig.SpriteAsset,
			};

			if (_newConfig.SpriteAsset != null)
				oldConfig.Sprite = spriteRenderer.sprite;

			if (_newConfig.Color != null)
				oldConfig.Color = spriteRenderer.color.ToKSVector4();

			if (_newConfig.FlipX.HasValue)
				oldConfig.FlipX = spriteRenderer.flipX;

			if (_newConfig.FlipY.HasValue)
				oldConfig.FlipY = spriteRenderer.flipY;

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
		}

		private GameObject GetGameObject()
		{
			if (_gameObject == null)
				_gameObject = References.Instance.GetGameObject(_reference);
			return _gameObject;
		}

		private SpriteRenderer GetSpriteRenderer()
		{
			if (_spriteRenderer == null)
				_spriteRenderer = GetGameObject().GetComponent<SpriteRenderer>();
			return _spriteRenderer;
		}
	}
}
