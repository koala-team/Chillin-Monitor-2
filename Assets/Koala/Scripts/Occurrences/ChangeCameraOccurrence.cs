using DG.Tweening;
using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ChangeCameraOccurrence : BaseOccurrence<ChangeCameraOccurrence, ChangeCamera>
	{
		private Camera _camera;


		public ChangeCameraOccurrence() { }

		protected override ChangeCamera CreateOldConfig()
		{
			var camera = GetCamera();

			var oldConfig = new ChangeCamera();

			if (_newConfig.ClearFlag.HasValue)
				oldConfig.ClearFlag = (ECameraClearFlag)camera.clearFlags;

			if (_newConfig.BackgroundColor != null)
				oldConfig.BackgroundColor = camera.backgroundColor.ToKSVector4();

			if (_newConfig.IsOrthographic.HasValue)
				oldConfig.IsOrthographic = camera.orthographic;

			if (_newConfig.OrthographicSize.HasValue)
				oldConfig.OrthographicSize = camera.orthographicSize;

			if (_newConfig.FieldOfView.HasValue)
				oldConfig.FieldOfView = camera.fieldOfView;

			if (_newConfig.NearClipPlane.HasValue)
				oldConfig.NearClipPlane = camera.nearClipPlane;

			if (_newConfig.FarClipPlane.HasValue)
				oldConfig.FarClipPlane = camera.farClipPlane;

			return oldConfig;
		}

		protected override void ManageTweens(ChangeCamera config, bool isForward)
		{
			var camera = GetCamera();

			if (config.BackgroundColor != null)
			{
				DOTween.To(
					() => camera.backgroundColor,
					x => camera.backgroundColor = x,
					camera.backgroundColor.ApplyKSVector4(config.BackgroundColor),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.OrthographicSize.HasValue)
			{
				DOTween.To(
					() => camera.orthographicSize,
					x => camera.orthographicSize = x,
					config.OrthographicSize.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.FieldOfView.HasValue)
			{
				DOTween.To(
					() => camera.fieldOfView,
					x => camera.fieldOfView = x,
					config.FieldOfView.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.NearClipPlane.HasValue)
			{
				DOTween.To(
					() => camera.nearClipPlane,
					x => camera.nearClipPlane = x,
					config.NearClipPlane.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.FarClipPlane.HasValue)
			{
				DOTween.To(
					() => camera.farClipPlane,
					x => camera.farClipPlane = x,
					config.FarClipPlane.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeCamera config, bool isForward)
		{
			var camera = GetCamera();

			if (config.ClearFlag.HasValue)
				camera.clearFlags = (CameraClearFlags)config.ClearFlag.Value;
			
			if (config.IsOrthographic.HasValue)
				camera.orthographic = config.IsOrthographic.Value;
		}

		private Camera GetCamera()
		{
			if (_camera == null)
				_camera = References.Instance.GetGameObject(_reference).GetComponent<Camera>();
			return _camera;
		}
	}
}
