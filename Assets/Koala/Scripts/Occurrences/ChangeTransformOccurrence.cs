using UnityEngine;
using DG.Tweening;

namespace Koala
{
	public class ChangeTransformOccurrence : BaseOccurrence<ChangeTransformOccurrence, Director.ChangeTransformConfig>
	{
		private Transform _transform = null;
		

		public ChangeTransformOccurrence() { }

		protected override Director.ChangeTransformConfig CreateOldConfig()
		{
			Transform transform = GetTransform();

			var currentConfig = new Director.ChangeTransformConfig();

			if (_newConfig.Position != null)
				currentConfig.Position = transform.position.ToChangeVector3Config();
			if (_newConfig.Rotation != null)
				currentConfig.Rotation = transform.eulerAngles.ToChangeVector3Config();
			if (_newConfig.Scale != null)
				currentConfig.Scale = transform.localScale.ToChangeVector3Config();

			return currentConfig;
		}

		protected override void ManageTweens(Director.ChangeTransformConfig config, bool isForward)
		{
			Transform transform = GetTransform();

			if (config.Position != null)
			{
				DOTween.To(
					() => transform.localPosition,
					x => transform.localPosition = x,
					transform.localPosition.ApplyChangeVector3Config(config.Position),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Rotation != null)
			{
				DOTween.To(
					() => transform.localRotation.eulerAngles,
					x => transform.localEulerAngles = x,
					transform.localEulerAngles.ApplyChangeVector3Config(config.Rotation),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Scale != null)
			{
				DOTween.To(
					() => transform.localScale,
					x => transform.localScale = x,
					transform.localScale.ApplyChangeVector3Config(config.Scale),
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
