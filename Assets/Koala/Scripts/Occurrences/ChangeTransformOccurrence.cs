using UnityEngine;
using DG.Tweening;
using KS.SceneActions;

namespace Koala
{
	public class ChangeTransformOccurrence : BaseOccurrence<ChangeTransformOccurrence, ChangeTransform>
	{
		private Transform _transform = null;
		

		public ChangeTransformOccurrence() { }

		protected override ChangeTransform CreateOldConfig()
		{
			Transform transform = GetTransform();

			var currentConfig = new ChangeTransform();

			if (_newConfig.Position != null)
				currentConfig.Position = transform.position.ToKSVector3();
			if (_newConfig.Rotation != null)
				currentConfig.Rotation = transform.eulerAngles.ToKSVector3();
			if (_newConfig.Scale != null)
				currentConfig.Scale = transform.localScale.ToKSVector3();

			return currentConfig;
		}

		protected override void ManageTweens(ChangeTransform config, bool isForward)
		{
			Transform transform = GetTransform();

			if (config.Position != null)
			{
				DOTween.To(
					() => transform.localPosition,
					x => transform.localPosition = x,
					transform.localPosition.ApplyKSVector3(config.Position),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Rotation != null)
			{
				DOTween.To(
					() => transform.localRotation.eulerAngles,
					x => transform.localEulerAngles = x,
					transform.localEulerAngles.ApplyKSVector3(config.Rotation),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Scale != null)
			{
				DOTween.To(
					() => transform.localScale,
					x => transform.localScale = x,
					transform.localScale.ApplyKSVector3(config.Scale),
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		private Transform GetTransform()
		{
			if (_transform == null)
				_transform = References.Instance.GetGameObject(_reference).transform;
			return _transform;
		}
	}
}
