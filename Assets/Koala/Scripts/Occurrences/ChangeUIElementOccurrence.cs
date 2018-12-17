using UnityEngine;
using DG.Tweening;
using KS.SceneActions;

namespace Koala
{
	public class ChangeUIElementOccurrence : BaseOccurrence<ChangeUIElementOccurrence, ChangeRectTransform>
	{
		private RectTransform _rect = null;


		public ChangeUIElementOccurrence() { }

		protected override ChangeRectTransform CreateOldConfig()
		{
			RectTransform rect = GetRect();

			var currentConfig = new ChangeRectTransform();

			if (_newConfig.Position != null)
				currentConfig.Position = rect.anchoredPosition3D.ToKSVector3();

			if (_newConfig.Rotation != null)
				currentConfig.Rotation = rect.eulerAngles.ToKSVector3();

			if (_newConfig.Scale != null)
				currentConfig.Scale = rect.localScale.ToKSVector3();

			if (_newConfig.Pivot != null)
				currentConfig.Pivot = rect.pivot.ToKSVector2();

			if (_newConfig.AnchorMin != null)
				currentConfig.AnchorMin = rect.anchorMin.ToKSVector2();

			if (_newConfig.AnchorMax != null)
				currentConfig.AnchorMax = rect.anchorMax.ToKSVector2();

			if (_newConfig.Size != null)
				currentConfig.Size = rect.sizeDelta.ToKSVector2();

			return currentConfig;
		}

		protected override void ManageTweens(ChangeRectTransform config, bool isForward)
		{
			RectTransform rect = GetRect();

			if (config.Position != null)
			{
				DOTween.To(
					() => rect.anchoredPosition3D,
					x => rect.anchoredPosition3D = x,
					rect.anchoredPosition3D.ApplyKSVector3(config.Position),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Rotation != null)
			{
				DOTween.To(
					() => rect.localEulerAngles,
					x => rect.localEulerAngles = x,
					rect.localEulerAngles.ApplyKSVector3(config.Rotation),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Scale != null)
			{
				DOTween.To(
					() => rect.localScale,
					x => rect.localScale = x,
					rect.localScale.ApplyKSVector3(config.Scale),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Pivot != null)
			{
				DOTween.To(
					() => rect.pivot,
					x => rect.pivot = x,
					rect.pivot.ApplyKSVector2(config.Pivot),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.AnchorMin != null)
			{
				DOTween.To(
					() => rect.anchorMin,
					x => rect.anchorMin = x,
					rect.anchorMin.ApplyKSVector2(config.AnchorMin),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.AnchorMax != null)
			{
				DOTween.To(
					() => rect.anchorMax,
					x => rect.anchorMax = x,
					rect.anchorMax.ApplyKSVector2(config.AnchorMax),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Size != null)
			{
				DOTween.To(
					() => rect.sizeDelta,
					x => rect.sizeDelta = x,
					rect.sizeDelta.ApplyKSVector2(config.Size),
					_duration).RegisterInTimeline(_startTime, isForward);
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
