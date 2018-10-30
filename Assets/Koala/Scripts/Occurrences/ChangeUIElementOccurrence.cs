using UnityEngine;
using DG.Tweening;

namespace Koala
{
	public class ChangeUIElementOccurrence : DOTweenOccurrence<ChangeUIElementOccurrence, Director.ChangeUIElementConfig>
	{
		private RectTransform _rect = null;


		public ChangeUIElementOccurrence() { }

		protected override Director.ChangeUIElementConfig CreateOldConfig()
		{
			RectTransform rect = GetRect();

			var currentConfig = new Director.ChangeUIElementConfig();

			if (_newConfig.Position != null)
				currentConfig.Position = rect.anchoredPosition3D.ToChangeVector3Config();

			if (_newConfig.Rotation != null)
				currentConfig.Rotation = rect.eulerAngles.ToChangeVector3Config();

			if (_newConfig.Scale != null)
				currentConfig.Scale = rect.localScale.ToChangeVector3Config();

			if (_newConfig.Pivot != null)
				currentConfig.Pivot = rect.pivot.ToChangeVector2Config();

			if (_newConfig.AnchorMin != null)
				currentConfig.AnchorMin = rect.anchorMin.ToChangeVector2Config();

			if (_newConfig.AnchorMax != null)
				currentConfig.AnchorMax = rect.anchorMax.ToChangeVector2Config();

			if (_newConfig.Size != null)
				currentConfig.Size = rect.sizeDelta.ToChangeVector2Config();

			return currentConfig;
		}

		protected override void ManageTweens(Director.ChangeUIElementConfig config, bool isForward)
		{
			RectTransform rect = GetRect();

			if (config.Position != null)
			{
				DOTween.To(
					() => rect.anchoredPosition3D,
					x => rect.anchoredPosition3D = x,
					rect.anchoredPosition3D.ApplyChangeVector3Config(config.Position),
					_duration).RegisterChronosTimeline(isForward);
			}

			if (config.Rotation != null)
			{
				DOTween.To(
					() => rect.eulerAngles,
					x => rect.eulerAngles = x,
					rect.eulerAngles.ApplyChangeVector3Config(config.Rotation),
					_duration).RegisterChronosTimeline(isForward);
			}

			if (config.Scale != null)
			{
				DOTween.To(
					() => rect.localScale,
					x => rect.localScale = x,
					rect.localScale.ApplyChangeVector3Config(config.Scale),
					_duration).RegisterChronosTimeline(isForward);
			}

			if (config.Pivot != null)
			{
				DOTween.To(
					() => rect.pivot,
					x => rect.pivot = x,
					rect.pivot.ApplyChangeVector2Config(config.Pivot),
					_duration).RegisterChronosTimeline(isForward);
			}

			if (config.AnchorMin != null)
			{
				DOTween.To(
					() => rect.anchorMin,
					x => rect.anchorMin = x,
					rect.anchorMin.ApplyChangeVector2Config(config.AnchorMin),
					_duration).RegisterChronosTimeline(isForward);
			}

			if (config.AnchorMax != null)
			{
				DOTween.To(
					() => rect.anchorMax,
					x => rect.anchorMax = x,
					rect.anchorMax.ApplyChangeVector2Config(config.AnchorMax),
					_duration).RegisterChronosTimeline(isForward);
			}

			if (config.Size != null)
			{
				DOTween.To(
					() => rect.sizeDelta,
					x => rect.sizeDelta = x,
					rect.sizeDelta.ApplyChangeVector2Config(config.Size),
					_duration).RegisterChronosTimeline(isForward);
			}
		}

		private RectTransform GetRect()
		{
			if (_rect == null)
				_rect = References.Instance.GetGameObject(_reference).GetComponent<RectTransform>();
			return _rect;
		}
	}
}
