using DG.Tweening;
using UnityEngine;
using KS.SceneActions;
using UnityEngine.Rendering.PostProcessing;

namespace Koala
{
	public class ChangeCameraOccurrence : BaseOccurrence<ChangeCameraOccurrence, ChangeCamera>
	{
		private Camera _camera;
		private CameraController _cameraController;
		private PostProcessVolume _postProcessVolume;


		public ChangeCameraOccurrence() { }

		protected override ChangeCamera CreateOldConfig()
		{
			var camera = GetCamera();
			var cameraController = GetCameraController();
			var postProcessVolume = GetPostProcessVolume();

			var oldConfig = new ChangeCamera
			{
				PostProcessProfileAsset = _newConfig.PostProcessProfileAsset,
				PostProcessProfile = postProcessVolume.profile,
			};

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

			if (_newConfig.MinPosition != null)
				oldConfig.MinPosition = cameraController.MinPosition.ToKSVector3();

			if (_newConfig.MaxPosition != null)
				oldConfig.MaxPosition = cameraController.MaxPosition.ToKSVector3();

			if (_newConfig.MinRotation != null)
				oldConfig.MinRotation = cameraController.MinRotation.ToKSVector2();

			if (_newConfig.MaxRotation != null)
				oldConfig.MaxRotation = cameraController.MaxRotation.ToKSVector2();

			if (_newConfig.MinZoom.HasValue)
				oldConfig.MinZoom = cameraController.MinZoom;

			if (_newConfig.MaxZoom.HasValue)
				oldConfig.MaxZoom = cameraController.MaxZoom;

			if (_newConfig.PostProcessProfileAsset != null)
				oldConfig.PostProcessProfile = postProcessVolume.profile;

			return oldConfig;
		}

		protected override void ManageTweens(ChangeCamera config, bool isForward)
		{
			var camera = GetCamera();
			var cameraController = GetCameraController();

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

			if (config.MinPosition != null)
			{
				DOTween.To(
					() => cameraController.MinPosition,
					x => cameraController.MinPosition = x,
					cameraController.MinPosition.ApplyKSVector3(config.MinPosition),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.MaxPosition != null)
			{
				DOTween.To(
					() => cameraController.MaxPosition,
					x => cameraController.MaxPosition = x,
					cameraController.MaxPosition.ApplyKSVector3(config.MaxPosition),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.MinRotation != null)
			{
				DOTween.To(
					() => cameraController.MinRotation,
					x => cameraController.MinRotation = x,
					cameraController.MinRotation.ApplyKSVector2(config.MinRotation),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.MaxRotation != null)
			{
				DOTween.To(
					() => cameraController.MaxRotation,
					x => cameraController.MaxRotation = x,
					cameraController.MaxRotation.ApplyKSVector2(config.MaxRotation),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.MinZoom.HasValue)
			{
				DOTween.To(
					() => cameraController.MinZoom,
					x => cameraController.MinZoom = x,
					cameraController.MinZoom,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.MaxZoom.HasValue)
			{
				DOTween.To(
					() => cameraController.MaxZoom,
					x => cameraController.MaxZoom = x,
					cameraController.MaxZoom,
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeCamera config, bool isForward)
		{
			var camera = GetCamera();
			var postProcessVolume = GetPostProcessVolume();

			if (config.ClearFlag.HasValue)
				camera.clearFlags = (CameraClearFlags)config.ClearFlag.Value;
			
			if (config.IsOrthographic.HasValue)
				camera.orthographic = config.IsOrthographic.Value;

			if (config.PostProcessProfileAsset != null)
				postProcessVolume.profile = config.PostProcessProfile;
		}

		private Camera GetCamera()
		{
			if (_camera == null)
				_camera = References.Instance.GetGameObject(_reference).GetComponent<Camera>();
			return _camera;
		}

		private CameraController GetCameraController()
		{
			if (_cameraController == null)
				_cameraController = References.Instance.GetGameObject(_reference).GetComponent<CameraController>();
			return _cameraController;
		}

		private PostProcessVolume GetPostProcessVolume()
		{
			if (_postProcessVolume == null)
				_postProcessVolume = References.Instance.GetGameObject(_reference).GetComponent<PostProcessVolume>();
			return _postProcessVolume;
		}
	}
}
