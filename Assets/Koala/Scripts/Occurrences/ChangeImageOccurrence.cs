using DG.Tweening;
using UnityEngine.UI;
using KS.SceneActions;

namespace Koala
{
	public class ChangeImageOccurrence : BaseOccurrence<ChangeImageOccurrence, ChangeImage>
	{
		private Image _image;


		public ChangeImageOccurrence() { }

		protected override ChangeImage CreateOldConfig()
		{
			var image = GetImage();

			var oldConfig = new ChangeImage
			{
				Sprite = image.sprite,
				SpriteAsset = _newConfig.SpriteAsset,
				Material = image.material,
				MaterialAsset = _newConfig.MaterialAsset,
			};

			if (_newConfig.Color != null)
				oldConfig.Color = image.color.ToKSVector4();

			return oldConfig;
		}

		protected override void ManageTweens(ChangeImage config, bool isForward)
		{
			var image = GetImage();

			if (config.Color != null)
			{
				DOTween.To(
					() => image.color,
					x => image.color = x,
					image.color.ApplyKSVector4(config.Color),
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeImage config, bool isForward)
		{
			var image = GetImage();

			if (config.SpriteAsset != null)
				image.sprite = config.Sprite;

			if (config.MaterialAsset != null)
				image.material = config.Material;
		}

		private Image GetImage()
		{
			if (_image == null)
				_image = References.Instance.GetGameObject(_reference).GetComponent<Image>();
			return _image;
		}
	}
}
