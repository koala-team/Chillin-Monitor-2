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

			var oldConfig = new ChangeTransform
			{
				ChangeLocal = _newConfig.ChangeLocal,
			};

			if (_newConfig.Position != null)
				oldConfig.Position = _newConfig.ChangeLocal.Value ? transform.localPosition.ToKSVector3() : transform.position.ToKSVector3();
			if (_newConfig.Rotation != null)
				oldConfig.Rotation = _newConfig.ChangeLocal.Value ? transform.localEulerAngles.ToKSVector3() : transform.eulerAngles.ToKSVector3();
			if (_newConfig.Scale != null)
				oldConfig.Scale = transform.localScale.ToKSVector3();

			return oldConfig;
		}

		protected override void ManageTweens(ChangeTransform config, bool isForward)
		{
			Transform transform = GetTransform();

			if (config.ChangeLocal.Value)
			{
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
					var q = new Quaternion
					{
						eulerAngles = transform.localEulerAngles.ApplyKSVector3(config.Rotation)
					};

					DOTween.To(
						() => transform.localRotation,
						x => transform.localRotation = x,
						q.eulerAngles,
						_duration).RegisterInTimeline(_startTime, isForward);
				}
			}
			else
			{
				if (config.Position != null)
				{
					DOTween.To(
						() => transform.position,
						x => transform.position = x,
						transform.position.ApplyKSVector3(config.Position),
						_duration).RegisterInTimeline(_startTime, isForward);
				}

				if (config.Rotation != null)
				{
					var q = new Quaternion
					{
						eulerAngles = transform.eulerAngles.ApplyKSVector3(config.Rotation)
					};

					DOTween.To(
						() => transform.rotation,
						x => transform.rotation = x,
						q.eulerAngles,
						_duration).RegisterInTimeline(_startTime, isForward);
				}
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
