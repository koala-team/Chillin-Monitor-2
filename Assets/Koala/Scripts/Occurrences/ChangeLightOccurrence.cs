using DG.Tweening;
using UnityEngine;
using KS.SceneActions;

namespace Koala
{
	public class ChangeLightOccurrence : BaseOccurrence<ChangeLightOccurrence, ChangeLight>
	{
		private Light _light;


		public ChangeLightOccurrence() { }

		protected override ChangeLight CreateOldConfig()
		{
			var light = GetLight();

			var oldConfig = new ChangeLight();

			if (_newConfig.Type.HasValue)
				oldConfig.Type = (ELightType)light.type;

			if (_newConfig.Range.HasValue)
				oldConfig.Range = light.range;

			if (_newConfig.SpotAngle.HasValue)
				oldConfig.SpotAngle = light.spotAngle;

			if (_newConfig.Color != null)
				oldConfig.Color = light.color.ToKSVector4();

			if (_newConfig.Intensity.HasValue)
				oldConfig.Intensity = light.intensity;

			if (_newConfig.IndirectMultiplier.HasValue)
				oldConfig.IndirectMultiplier = light.bounceIntensity;

			if (_newConfig.ShadowType.HasValue)
				oldConfig.ShadowType = (ELightShadowType)light.shadows;

			if (_newConfig.ShadowStrength.HasValue)
				oldConfig.ShadowStrength = light.shadowStrength;

			if (_newConfig.ShadowBias.HasValue)
				oldConfig.ShadowBias = light.shadowBias;

			if (_newConfig.ShadowNormalBias.HasValue)
				oldConfig.ShadowNormalBias = light.shadowNormalBias;

			if (_newConfig.ShadowNearPlane.HasValue)
				oldConfig.ShadowNearPlane = light.shadowNearPlane;

			if (_newConfig.Cookie != null)
				oldConfig.Cookie = light.cookie;

			if (_newConfig.CookieSize.HasValue)
				oldConfig.CookieSize = light.cookieSize;

			if (_newConfig.Flare != null)
				oldConfig.Flare = light.flare;

			return oldConfig;
		}

		protected override void ManageTweens(ChangeLight config, bool isForward)
		{
			var light = GetLight();

			if (config.Range.HasValue)
			{
				DOTween.To(
					() => light.range,
					x => light.range = x,
					config.Range.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.SpotAngle.HasValue)
			{
				DOTween.To(
					() => light.spotAngle,
					x => light.spotAngle = x,
					config.SpotAngle.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Color != null)
			{
				DOTween.To(
					() => light.color,
					x => light.color = x,
					light.color.ApplyKSVector4(config.Color),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Intensity.HasValue)
			{
				DOTween.To(
					() => light.intensity,
					x => light.intensity = x,
					config.Intensity.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.IndirectMultiplier.HasValue)
			{
				DOTween.To(
					() => light.bounceIntensity,
					x => light.bounceIntensity = x,
					config.IndirectMultiplier.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.ShadowStrength.HasValue)
			{
				DOTween.To(
					() => light.shadowStrength,
					x => light.shadowStrength = x,
					config.ShadowStrength.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.ShadowBias.HasValue)
			{
				DOTween.To(
					() => light.shadowBias,
					x => light.shadowBias = x,
					config.ShadowBias.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.ShadowNormalBias.HasValue)
			{
				DOTween.To(
					() => light.shadowNormalBias,
					x => light.shadowNormalBias = x,
					config.ShadowNormalBias.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.ShadowNearPlane.HasValue)
			{
				DOTween.To(
					() => light.shadowNearPlane,
					x => light.shadowNearPlane = x,
					config.ShadowNearPlane.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeLight config, bool isForward)
		{
			var light = GetLight();

			if (config.Type.HasValue)
				light.type = (LightType)config.Type.Value;

			if (config.ShadowType.HasValue)
				light.shadows = (LightShadows)config.ShadowType.Value;

			if (config.Cookie != null)
				light.cookie = config.Cookie;

			if (config.CookieSize.HasValue)
				light.cookieSize = config.CookieSize.Value;

			if (config.Flare != null)
				light.flare = config.Flare;
		}

		private Light GetLight()
		{
			if (_light == null)
				_light = References.Instance.GetGameObject(_reference).GetComponent<Light>();
			return _light;
		}
	}
}
