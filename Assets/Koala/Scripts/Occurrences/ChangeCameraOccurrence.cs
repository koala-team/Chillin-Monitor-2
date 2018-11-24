using DG.Tweening;
using UnityEngine;

namespace Koala
{
	public class ChangeCameraOccurrence : BaseOccurrence<ChangeCameraOccurrence, Director.ChangeCameraConfig>
	{
		private Camera _camera;


		public ChangeCameraOccurrence() { }

		protected override Director.ChangeCameraConfig CreateOldConfig()
		{
			var camera = GetCamera();

			var oldConfig = new Director.ChangeCameraConfig();

			if (_newConfig.ClearFlags.HasValue)
				oldConfig.ClearFlags = camera.clearFlags;

			if (_newConfig.BackgroundColor != null)
				oldConfig.BackgroundColor = camera.backgroundColor.ToChangeVector4Config();

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

		protected override void ManageTweens(Director.ChangeCameraConfig config, bool isForward)
		{
			var camera = GetCamera();

			if (config.BackgroundColor != null)
			{
				DOTween.To(
					() => camera.backgroundColor,
					x => camera.backgroundColor = x,
					camera.backgroundColor.ApplyChangeVector4Config(config.BackgroundColor),
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

		protected override void ManageSuddenChanges(Director.ChangeCameraConfig config, bool isForward)
		{
			var camera = GetCamera();

			if (config.ClearFlags.HasValue)
				camera.clearFlags = config.ClearFlags.Value;
			
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
