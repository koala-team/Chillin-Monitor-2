using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Koala
{
	// This Class create and manage occurrences
	public class Director
	{
		public Director()
		{
		}

		#region Actions
		private void BaseAction<T, C>(float cycle, string reference, float durationCycles, C config, bool onlySuddenChanges = false)
			where T : Occurrence, IBaseOccurrence<T, C>, new()
			where C : class
		{
			float startTime, endTime, duration;
			Helper.GetCyclesDurationTime(cycle, durationCycles, out startTime, out endTime, out duration);

			T forwardOccurrence = new T();
			T backwardOccurrence = new T();

			forwardOccurrence.Init(reference, startTime, endTime, config, true, null);
			backwardOccurrence.Init(reference, endTime, startTime, null, false, forwardOccurrence);

			Timeline.Instance.Schedule(startTime, forwardOccurrence);
			if (!onlySuddenChanges)
				Timeline.Instance.Schedule(endTime, backwardOccurrence);
		}

		private void Instantiate(float cycle, string reference, InstantiateConfig config)
		{
			if (config.DefaultParent == null)
				config.DefaultParent = Helper.RootGameObject;

			BaseAction<InstantiateOccurrence, InstantiateConfig>(cycle, reference, 0, config, true);
		}

		public void InstantiateBundleAsset(float cycle, string reference, InstantiateBundleAssetConfig config)
		{
			config.GameObject = BundleManager.Instance.LoadAsset<GameObject>(config.Asset);

			Instantiate(cycle, reference, config);
		}

		public void Destroy(float cycle, string reference)
		{
			BaseAction<DestroyOccurrence, DestroyConfig>(cycle, reference, 0, new DestroyConfig(), true);
		}

		public void ChangeIsActive(float cycle, string reference, ChangeIsActiveConfig config)
		{
			BaseAction<ChangeIsActiveOccurrence, ChangeIsActiveConfig>(cycle, reference, 0, config, true);
		}

		public void ChangeTransform(float cycle, string reference, float durationCycles, ChangeTransformConfig config)
		{
			BaseAction<ChangeTransformOccurrence, ChangeTransformConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeAnimatorVariable(float cycle, string reference, ChangeAnimatorVariableConfig config)
		{
			switch (config.VarType)
			{
				case EAnimatorVariableType.Int:
					int tempInt;
					int.TryParse(config.Value, out tempInt);
					config.ValueObject = tempInt;
					break;
				case EAnimatorVariableType.Float:
					float tempFloat;
					float.TryParse(config.Value, out tempFloat);
					config.ValueObject = tempFloat;
					break;
				case EAnimatorVariableType.Bool:
					bool tempBool;
					bool.TryParse(config.Value.ToLower(), out tempBool);
					config.ValueObject = tempBool;
					break;
				case EAnimatorVariableType.Trigger:
					config.ValueObject = null;
					break;
				default:
					throw new System.NotSupportedException("varType value not supported!");
			}

			BaseAction<ChangeAnimatorVariableOccurrence, ChangeAnimatorVariableConfig>(cycle, reference, 0, config, true);
		}

		public void ChangeAnimatorState(float cycle, string reference, ChangeAnimatorStateConfig config)
		{
			BaseAction<ChangeAnimatorStateOccurrence, ChangeAnimatorStateConfig>(cycle, reference, 0, config, true);
		}

		public void ChangeAudioSource(float cycle, string reference,
			float durationCycles, ChangeAudioSourceConfig config)
		{
			config.AudioClip = BundleManager.Instance.LoadAsset<AudioClip>(config.AudioClipAsset);

			BaseAction<ChangeAudioSourceOccurrence, ChangeAudioSourceConfig>(cycle, reference, durationCycles, config);
		}

		public void CreateUIElement(float cycle, string reference,
			string parentReference, EUIElementType type, ChangeRectTransformConfig config)
		{
			GameObject go;
			switch (type)
			{
				case EUIElementType.Canvas:
				case EUIElementType.Text:
				case EUIElementType.Slider:
				case EUIElementType.RawImage:
				case EUIElementType.Panel:
					go = Resources.Load("UIElements/" + type.ToString()) as GameObject;
					break;
				default:
					throw new System.NotSupportedException("type is not supported");
			}

			Instantiate(cycle, reference, new InstantiateConfig
			{
				GameObject = go,
				Position = new Vector3(),
				ParentReference = parentReference,
				DefaultParent = Helper.UserCanvasGameObject,
			});

			ChangeRectTransform(cycle, reference, 0, config);
		}

		public void ChangeRectTransform(float cycle, string reference,
			float durationCycles, ChangeRectTransformConfig config)
		{
			BaseAction<ChangeUIElementOccurrence, ChangeRectTransformConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeText(float cycle, string reference,
			float durationCycles, ChangeTextConfig config)
		{
			if (config.FontAsset != null)
			{
				config.Font = BundleManager.Instance.LoadAsset<TMP_FontAsset>(config.FontAsset);
			}
			else if (config.FontName != null)
			{
				config.Font = Helper.GetFont(config.FontName.ToLower());
			}

			BaseAction<ChangeTextOccurrence, ChangeTextConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeSlider(float cycle, string reference,
			float durationCycles, ChangeSliderConfig config)
		{
			BaseAction<ChangeSliderOccurrence, ChangeSliderConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeRawImage(float cycle, string reference,
			float durationCycles, ChangeRawImageConfig config)
		{
			config.Texture = BundleManager.Instance.LoadAsset<Texture>(config.TextureAsset);
			config.Material = BundleManager.Instance.LoadAsset<Material>(config.MaterialAsset);

			BaseAction<ChangeRawImageOccurrence, ChangeRawImageConfig>(cycle, reference, durationCycles, config);
		}

		public void CreateEmptyGameObject(float cycle, string reference, InstantiateConfig config)
		{
			Instantiate(cycle, reference, config);
		}

		public void ChangeSiblingOrder(float cycle, string reference,
			ChangeSiblingOrderConfig config)
		{
			BaseAction<ChangeSiblingOrderOccurrence, ChangeSiblingOrderConfig>(cycle, reference, 0, config, true);
		}

		public void ManageComponents(float cycle, string reference,
			ManageComponentsConfig config)
		{
			BaseAction<ManageComponentsOccurrence, ManageComponentsConfig>(cycle, reference, 0, config, true);
		}

		public void CreateBasicObject(float cycle, string reference,
			string parentReference, EBasicObjectType type, InstantiateConfig config)
		{
			switch (type)
			{
				case EBasicObjectType.Sprite:
				case EBasicObjectType.AudioSource:
				case EBasicObjectType.Ellipse2D:
				case EBasicObjectType.Polygon2D:
				case EBasicObjectType.Line:
				case EBasicObjectType.Light:
					config.GameObject = Resources.Load("BasicObjects/" + type.ToString()) as GameObject;
					break;
				default:
					throw new System.NotSupportedException("type is not supported");
			}
			
			Instantiate(cycle, reference, config);
		}

		public void ChangeSprite(float cycle, string reference,
			float durationCycles, ChangeSpriteConfig config)
		{
			config.Sprite = BundleManager.Instance.LoadAsset<Sprite>(config.SpriteAsset);

			BaseAction<ChangeSpriteOccurrence, ChangeSpriteConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeMaterial(float cycle, string reference,
			ChangeMaterialConfig config)
		{
			config.Material = BundleManager.Instance.LoadAsset<Material>(config.MaterialAsset);

			BaseAction<ChangeMaterialOccurrence, ChangeMaterialConfig>(cycle, reference, 0, config, true);
		}

		public void ChangeEllipse2D(float cycle, string reference,
			float durationCycles, ChangeEllipse2DConfig config)
		{
			BaseAction<ChangeEllipse2DOccurrence, ChangeEllipse2DConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangePolygon2D(float cycle, string reference,
			float durationCycles, ChangePolygon2DConfig config)
		{
			BaseAction<ChangePolygon2DOccurrence, ChangePolygon2DConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeLine(float cycle, string reference,
			float durationCycles, ChangeLineConfig config)
		{
			BaseAction<ChangeLineOccurrence, ChangeLineConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeLight(float cycle, string reference,
			float durationCycles, ChangeLightConfig config)
		{
			config.Cookie = BundleManager.Instance.LoadAsset<Texture>(config.CookieAsset);
			config.Flare = BundleManager.Instance.LoadAsset<Flare>(config.FlareAsset);

			BaseAction<ChangeLightOccurrence, ChangeLightConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeCamera(float cycle, string reference,
			float durationCycles, ChangeCameraConfig config)
		{
			BaseAction<ChangeCameraOccurrence, ChangeCameraConfig>(cycle, reference, durationCycles, config);
		}
		#endregion

		#region Configs
		public class ChangeVector4Config
		{
			public float? X { get; set; }
			public float? Y { get; set; }
			public float? Z { get; set; }
			public float? W { get; set; }
		}

		public class ChangeVector3Config
		{
			public float? X { get; set; }
			public float? Y { get; set; }
			public float? Z { get; set; }
		}

		public class ChangeVector2Config
		{
			public float? X { get; set; }
			public float? Y { get; set; }
		}

		public class ChangeAssetConfig
		{
			public string BundleName { get; set; }
			public string AssetName { get; set; }
		}

		public class InstantiateConfig
		{
			public Vector3 Position { get; set; }
			public Vector3? Rotation { get; set; }
			public Vector3? Scale { get; set; }
			public string ParentReference { get; set; }

			// Don't fill these, occurrence handles them
			public GameObject GameObject { get; set; }
			public GameObject DefaultParent { get; set; }
		}

		public class InstantiateBundleAssetConfig : InstantiateConfig
		{
			public ChangeAssetConfig Asset { get; set; }
		}

		public class DestroyConfig
		{
			// Don't fill these, occurrence handles them
			public Transform Parent { get; set; }
		}

		public class ChangeIsActiveConfig
		{
			public bool IsActive { get; set; }
		}

		public class ChangeTransformConfig
		{
			public ChangeVector3Config Position { get; set; }
			public ChangeVector3Config Rotation { get; set; }
			public ChangeVector3Config Scale { get; set; }
		}

		public class ChangeAnimatorVariableConfig
		{
			public string VarName { get; set; }
			public EAnimatorVariableType VarType { get; set; }
			public string Value { get; set; }

			// Don't fill these, occurrence handles them
			public object ValueObject { get; set; }
		}

		public class ChangeAnimatorStateConfig
		{
			public string StateName { get; set; }
			public int Layer { get; set; }
			public float NormalizedTime { get; set; }

			// Don't fill these, occurrence handles them
			public int? StateHash { get; set; }

			public ChangeAnimatorStateConfig()
			{
				Layer = 0;
				NormalizedTime = 0;
			}
		}

		public class ChangeAudioSourceConfig
		{
			public ChangeAssetConfig AudioClipAsset { get; set; }
			public float? Time { get; set; }
			public bool? Mute { get; set; }
			public bool? Loop { get; set; }
			public int? Priority { get; set; }
			public float? Volume { get; set; }
			public float? SpatialBlend { get; set; } // is 3D or not
			public bool? Play { get; set; }
			public bool? Stop { get; set; }

			// Don't fill these, occurrence handles them
			public AudioClip AudioClip { get; set; }
		}

		public class ChangeRectTransformConfig
		{
			public ChangeVector3Config Position { get; set; }
			public ChangeVector3Config Rotation { get; set; }
			public ChangeVector3Config Scale { get; set; }
			public ChangeVector2Config Pivot { get; set; }
			public ChangeVector2Config AnchorMin { get; set; }
			public ChangeVector2Config AnchorMax { get; set; }
			public ChangeVector2Config Size { get; set; }
		}

		public class ChangeTextConfig
		{
			public ChangeAssetConfig FontAsset { get; set; }
			public string FontName { get; set; }
			public string Text { get; set; }
			public float? FontSize { get; set; }
			public TextAlignmentOptions? Alignment { get; set; }
			public float? WordWrappingRatios { get; set; }

			// Don't fill these, occurrence handles them
			public TMP_FontAsset Font { get; set; }
		}

		public class ChangeSliderConfig
		{
			public float? Value { get; set; }
			public float? MaxValue { get; set; }
			public float? MinValue { get; set; }
			public Slider.Direction? Direction { get; set; }
			public ChangeVector4Config BackgroundColor { get; set; }
			public ChangeVector4Config FillColor { get; set; }
		}

		public class ChangeRawImageConfig
		{
			public ChangeAssetConfig TextureAsset { get; set; }
			public ChangeAssetConfig MaterialAsset { get; set; }
			public ChangeVector4Config Color { get; set; }
			public ChangeVector4Config UVRect { get; set; }

			// Don't fill these, occurrence handles them
			public Texture Texture { get; set; }
			public Material Material { get; set; }
		}

		public class ChangeSiblingOrderConfig
		{
			public int? NewIndex { get; set; }
			public bool? GotoFirst { get; set; }
			public bool? GotoLast { get; set; }
			public int? ChangeIndex { get; set; }
			public string SiblingReferenceAsBaseIndex { get; set; }
		}

		public class ManageComponentsConfig
		{
			public EComponentType Type { get; set; }
			public bool? Add { get; set; }
			public bool? IsActive { get; set; }
		}

		public class ChangeSpriteConfig
		{
			public ChangeAssetConfig SpriteAsset { get; set; }
			public ChangeVector4Config Color { get; set; }
			public bool? FlipX { get; set; }
			public bool? FlipY { get; set; }
			public int? Order { get; set; }

			// Don't fill these, occurrence handles them
			public Sprite Sprite { get; set; }
		}

		public class ChangeMaterialConfig
		{
			public ChangeAssetConfig MaterialAsset { get; set; }
			public int Index { get; set; }

			// Don't fill these, occurrence handles them
			public Material Material { get; set; }
		}

		public class ChangeEllipse2DConfig
		{
			public ChangeVector4Config FillColor { get; set; }
			public float? XRadius { get; set; }
			public float? YRadius { get; set; }
		}

		public class ChangePolygon2DConfig
		{
			public ChangeVector4Config FillColor { get; set; }
			public List<ChangeVector2Config> Vertices { get; set; }
			public bool SuddenChange { get; set; }

			public ChangePolygon2DConfig()
			{
				SuddenChange = false;
			}
		}

		public class ChangeLineConfig
		{
			public ChangeVector4Config FillColor { get; set; }
			public List<ChangeVector3Config> Vertices { get; set; }
			public float? Width { get; set; }
			public int? CornerVertices { get; set; }
			public int? EndCapVertices { get; set; }
			public bool? Loop { get; set; }

			// Don't fill these, occurrence handles them
			public bool SuddenChange { get; set; }

			public ChangeLineConfig()
			{
				SuddenChange = false;
			}
		}

		public class ChangeLightConfig
		{
			public LightType? Type { get; set; }
			public float? Range { get; set; }
			public float? SpotAngle { get; set; }
			public ChangeVector4Config Color { get; set; }
			public float? Intensity { get; set; }
			public float? IndirectMultiplier { get; set; }
			public LightShadows? ShadowType { get; set; }
			public float? ShadowStrength { get; set; }
			public float? ShadowBias { get; set; }
			public float? ShadowNormalBias { get; set; }
			public float? ShadowNearPlane { get; set; }
			public ChangeAssetConfig CookieAsset { get; set; }
			public float? CookieSize { get; set; }
			public ChangeAssetConfig FlareAsset { get; set; }

			// Don't fill these, occurrence handles them
			public Texture Cookie { get; set; }
			public Flare Flare { get; set; }
		}

		public class ChangeCameraConfig
		{
			public CameraClearFlags? ClearFlags { get; set; }
			public ChangeVector4Config BackgroundColor { get; set; }
			public bool? IsOrthographic { get; set; }
			public float? OrthographicSize { get; set; }
			public float? FieldOfView { get; set; }
			public float? NearClipPlane { get; set; }
			public float? FarClipPlane { get; set; }
		}
		#endregion
	}
}
