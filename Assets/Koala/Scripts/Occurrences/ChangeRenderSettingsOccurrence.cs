using KS.SceneActions;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace Koala
{
    public class ChangeRenderSettingsOccurrence : BaseOccurrence<ChangeRenderSettingsOccurrence, ChangeRenderSettings>
    {
		private bool _isFirstTime = true;

        public ChangeRenderSettingsOccurrence() { }

        protected override ChangeRenderSettings CreateOldConfig()
        {
			var oldConfig = new ChangeRenderSettings()
			{
				BackwardChanges = _newConfig.BackwardChanges,
				CustomReflectionAsset = _newConfig.CustomReflectionAsset, // it's value doesn't matter, just to know it was set
				SkyboxAsset = _newConfig.SkyboxAsset, // it's value doesn't matter, just to know it was set
				SunRef = _newConfig.SunRef, // it's value doesn't matter, just to know it was set
			};

			if (_newConfig.BackwardChanges.Value)
			{
				if (_newConfig.AmbientEquatorColor != null)
					oldConfig.AmbientEquatorColor = RenderSettings.ambientEquatorColor.ToKSVector4();

				if (_newConfig.AmbientGroundColor != null)
					oldConfig.AmbientGroundColor = RenderSettings.ambientGroundColor.ToKSVector4();

				if (_newConfig.AmbientIntensity.HasValue)
					oldConfig.AmbientIntensity = RenderSettings.ambientIntensity;

				if (_newConfig.AmbientLight != null)
					oldConfig.AmbientLight = RenderSettings.ambientLight.ToKSVector4();

				if (_newConfig.AmbientMode.HasValue)
					oldConfig.AmbientMode = (EAmbientMode)RenderSettings.ambientMode;

				if (_newConfig.AmbientSkyColor != null)
					oldConfig.AmbientSkyColor = RenderSettings.ambientSkyColor.ToKSVector4();

				if (_newConfig.CustomReflectionAsset != null)
					oldConfig.CustomReflection = RenderSettings.customReflection;

				if (_newConfig.DefaultReflectionMode.HasValue)
					oldConfig.DefaultReflectionMode = (EDefaultReflectionMode)RenderSettings.defaultReflectionMode;

				if (_newConfig.DefaultReflectionResolution.HasValue)
					oldConfig.DefaultReflectionResolution = RenderSettings.defaultReflectionResolution;

				if (_newConfig.FlareFadeSpeed.HasValue)
					oldConfig.FlareFadeSpeed = RenderSettings.flareFadeSpeed;

				if (_newConfig.FlareStrength.HasValue)
					oldConfig.FlareStrength = RenderSettings.flareStrength;

				if (_newConfig.HasFog.HasValue)
					oldConfig.HasFog = RenderSettings.fog;

				if (_newConfig.FogMode.HasValue)
					oldConfig.FogMode = (EFogMode)RenderSettings.fogMode;

				if (_newConfig.FogColor != null)
					oldConfig.FogColor = RenderSettings.fogColor.ToKSVector4();

				if (_newConfig.FogDensity.HasValue)
					oldConfig.FogDensity = RenderSettings.fogDensity;

				if (_newConfig.FogStartDistance.HasValue)
					oldConfig.FogStartDistance = RenderSettings.fogStartDistance;

				if (_newConfig.FogEndDistance.HasValue)
					oldConfig.FogEndDistance = RenderSettings.fogEndDistance;

				if (_newConfig.HaloStrength.HasValue)
					oldConfig.HaloStrength = RenderSettings.haloStrength;

				if (_newConfig.ReflectionBounces.HasValue)
					oldConfig.ReflectionBounces = RenderSettings.reflectionBounces;

				if (_newConfig.ReflectionIntensity.HasValue)
					oldConfig.ReflectionIntensity = RenderSettings.reflectionIntensity;

				if (_newConfig.SkyboxAsset != null)
					oldConfig.Skybox = RenderSettings.skybox;

				if (_newConfig.SubtractiveShadowColor != null)
					oldConfig.SubtractiveShadowColor = RenderSettings.subtractiveShadowColor.ToKSVector4();

				if (_newConfig.SunRef.HasValue)
				{
					string fullRef = References.GetFullRef(_newConfig.SunRef, _newConfig.SunChildRef);
					_newConfig.Sun = References.Instance.GetGameObject(fullRef).GetComponent<Light>();

					oldConfig.Sun = RenderSettings.sun;
				}
			}

            return oldConfig;
        }

        protected override void ManageSuddenChanges(ChangeRenderSettings config, bool isForward)
        {
			bool doChanges = config.BackwardChanges.Value || (isForward && _isFirstTime);

			if (doChanges)
			{
				_isFirstTime = false;

				if (config.AmbientEquatorColor != null)
					RenderSettings.ambientEquatorColor = RenderSettings.ambientEquatorColor.ApplyKSVector4(config.AmbientEquatorColor);

				if (config.AmbientGroundColor != null)
					RenderSettings.ambientGroundColor = RenderSettings.ambientGroundColor.ApplyKSVector4(config.AmbientGroundColor);

				if (config.AmbientIntensity.HasValue)
					RenderSettings.ambientIntensity = config.AmbientIntensity.Value;

				if (config.AmbientLight != null)
					RenderSettings.ambientLight = RenderSettings.ambientLight.ApplyKSVector4(config.AmbientLight);

				if (config.AmbientMode.HasValue)
					RenderSettings.ambientMode = (UnityEngine.Rendering.AmbientMode)config.AmbientMode.Value;

				if (config.AmbientSkyColor != null)
					RenderSettings.ambientSkyColor = RenderSettings.ambientSkyColor.ApplyKSVector4(config.AmbientSkyColor);

				if (config.CustomReflectionAsset != null)
					RenderSettings.customReflection = config.CustomReflection;

				if (config.DefaultReflectionMode.HasValue)
					RenderSettings.defaultReflectionMode = (UnityEngine.Rendering.DefaultReflectionMode)config.DefaultReflectionMode.Value;

				if (config.DefaultReflectionResolution.HasValue)
					RenderSettings.defaultReflectionResolution = config.DefaultReflectionResolution.Value;

				if (config.FlareFadeSpeed.HasValue)
					RenderSettings.flareFadeSpeed = config.FlareFadeSpeed.Value;

				if (config.FlareStrength.HasValue)
					RenderSettings.flareStrength = config.FlareStrength.Value;

				if (config.HasFog.HasValue)
					RenderSettings.fog = config.HasFog.Value;

				if (config.FogMode.HasValue)
					RenderSettings.fogMode = (FogMode)config.FogMode.Value;

				if (config.FogColor != null)
					RenderSettings.fogColor = RenderSettings.fogColor.ApplyKSVector4(config.FogColor);

				if (config.FogDensity.HasValue)
					RenderSettings.fogDensity = config.FogDensity.Value;

				if (config.FogStartDistance.HasValue)
					RenderSettings.fogStartDistance = config.FogStartDistance.Value;

				if (config.FogEndDistance.HasValue)
					RenderSettings.fogEndDistance = config.FogEndDistance.Value;

				if (config.HaloStrength.HasValue)
					RenderSettings.haloStrength = config.HaloStrength.Value;

				if (config.ReflectionBounces.HasValue)
					RenderSettings.reflectionBounces = config.ReflectionBounces.Value;

				if (config.ReflectionIntensity.HasValue)
					RenderSettings.reflectionIntensity = config.ReflectionIntensity.Value;

				if (config.SkyboxAsset != null)
					RenderSettings.skybox = config.Skybox;

				if (config.SubtractiveShadowColor != null)
					RenderSettings.subtractiveShadowColor = RenderSettings.subtractiveShadowColor.ApplyKSVector4(config.SubtractiveShadowColor);

				if (config.SunRef.HasValue)
					RenderSettings.sun = config.Sun;
			}
		}
    }
}
