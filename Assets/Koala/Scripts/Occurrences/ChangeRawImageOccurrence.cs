using DG.Tweening;
using UnityEngine.UI;
using KS.SceneActions;

namespace Koala
{
	public class ChangeRawImageOccurrence : BaseOccurrence<ChangeRawImageOccurrence, ChangeRawImage>
	{
		private RawImage _image;


		public ChangeRawImageOccurrence() { }

		protected override ChangeRawImage CreateOldConfig()
		{
			var image = GetImage();

			var oldConfig = new ChangeRawImage
			{
				Texture = image.texture,
				TextureAsset = _newConfig.TextureAsset,
				Material = image.material,
				MaterialAsset = _newConfig.MaterialAsset,
			};

			if (_newConfig.Color != null)
				oldConfig.Color = image.color.ToKSVector4();

			if (_newConfig.UvRect != null)
				oldConfig.UvRect = image.uvRect.ToKSVector4();

			return oldConfig;
		}

		protected override void ManageTweens(ChangeRawImage config, bool isForward)
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

			if (config.UvRect != null)
			{
				DOTween.To(
					() => image.uvRect,
					x => image.uvRect = x,
					image.uvRect.ApplyKSVector4(config.UvRect),
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeRawImage config, bool isForward)
		{
			var image = GetImage();

			if (config.TextureAsset != null)
				image.texture = config.Texture;

			if (config.MaterialAsset != null)
				image.material = config.Material;
		}

		private RawImage GetImage()
		{
			if (_image == null)
				_image = References.Instance.GetGameObject(_reference).GetComponent<RawImage>();
			return _image;
		}
	}
}
