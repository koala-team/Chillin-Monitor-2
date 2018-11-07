using UnityEngine;
using DG.Tweening;

namespace Koala
{
	public class ChangeSliderOccurrence : BaseOccurrence<ChangeSliderOccurrence, Director.ChangeSliderConfig>
	{
		private SliderParts _sliderParts;


		public ChangeSliderOccurrence() { }

		protected override Director.ChangeSliderConfig CreateOldConfig()
		{
			var sliderParts = GetSliderParts();

			var oldConfig = new Director.ChangeSliderConfig();

			if (_newConfig.Value.HasValue)
				oldConfig.Value = sliderParts.Slider.value;

			if (_newConfig.MaxValue.HasValue)
				oldConfig.MaxValue = sliderParts.Slider.maxValue;

			if (_newConfig.MinValue.HasValue)
				oldConfig.MinValue = sliderParts.Slider.minValue;

			if (_newConfig.Direction.HasValue)
				oldConfig.Direction = sliderParts.Slider.direction;

			if (_newConfig.BackgroundColor != null)
				oldConfig.BackgroundColor = sliderParts.BackgroundImage.color.ToChangeVector4Config();

			if (_newConfig.FillColor != null)
				oldConfig.FillColor = sliderParts.FillImage.color.ToChangeVector4Config();

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangeSliderConfig config, bool isForward)
		{
			var sliderParts = GetSliderParts();

			if (config.Value.HasValue)
			{
				DOTween.To(
					() => sliderParts.Slider.value,
					x => sliderParts.Slider.value = x,
					config.Value.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}

			if (config.MaxValue.HasValue)
			{
				DOTween.To(
					() => sliderParts.Slider.maxValue,
					x => sliderParts.Slider.maxValue = x,
					config.MaxValue.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}

			if (config.MinValue.HasValue)
			{
				DOTween.To(
					() => sliderParts.Slider.minValue,
					x => sliderParts.Slider.minValue = x,
					config.MinValue.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}

			if (config.BackgroundColor != null)
			{
				DOTween.To(
					() => sliderParts.BackgroundImage.color,
					x => sliderParts.BackgroundImage.color = x,
					sliderParts.BackgroundImage.color.ApplyChangeVector4Config(config.BackgroundColor),
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}

			if (config.FillColor != null)
			{
				DOTween.To(
					() => sliderParts.FillImage.color,
					x => sliderParts.FillImage.color = x,
					sliderParts.FillImage.color.ApplyChangeVector4Config(config.FillColor),
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(Director.ChangeSliderConfig config, bool isForward)
		{
			var sliderParts = GetSliderParts();

			if (config.Direction.HasValue)
				sliderParts.Slider.direction = config.Direction.Value;
		}

		private SliderParts GetSliderParts()
		{
			if (_sliderParts == null)
				_sliderParts = References.Instance.GetGameObject(_reference).GetComponent<SliderParts>();
			return _sliderParts;
		}
	}
}
