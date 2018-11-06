using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Koala
{
	public class ChangeRawImageOccurrence : BaseOccurrence<ChangeRawImageOccurrence, Director.ChangeRawImageConfig>
	{
		private RawImage _image;


		public ChangeRawImageOccurrence() { }

		protected override Director.ChangeRawImageConfig CreateOldConfig()
		{
			var image = GetImage();

			var oldConfig = new Director.ChangeRawImageConfig
			{
				Texture = image.texture,
			};

			if (_newConfig.Color != null)
				oldConfig.Color = image.color.ToChangeVector4Config();

			if (_newConfig.UVRect != null)
				oldConfig.UVRect = image.uvRect.ToChangeVector4Config();

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangeRawImageConfig config, bool isForward)
		{
			var image = GetImage();

			if (config.Color != null)
			{
				DOTween.To(
					() => image.color,
					x => image.color = x,
					image.color.ApplyChangeVector4Config(config.Color),
					_duration).RegisterChronosTimeline(isForward);
			}

			if (config.UVRect != null)
			{
				DOTween.To(
					() => image.uvRect,
					x => image.uvRect = x,
					image.uvRect.ApplyChangeVector4Config(config.UVRect),
					_duration).RegisterChronosTimeline(isForward);
			}
		}

		protected override void ManageSuddenChanges(Director.ChangeRawImageConfig config, bool isForward)
		{
			var image = GetImage();

			if (_newConfig.BundleName != null && _newConfig.AssetName != null)
				image.texture = config.Texture;
		}

		private RawImage GetImage()
		{
			if (_image == null)
				_image = References.Instance.GetGameObject(_reference).GetComponent<RawImage>();
			return _image;
		}
	}
}
