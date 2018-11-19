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

		public void InstantiateBundleAsset(
			float cycle, string reference,
			string bundleName, string assetName, InstantiateConfig config)
		{
			GameObject go = BundleManager.Instance.LoadAsset<GameObject>(bundleName, assetName);

			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new InstantiateOccurrence(reference, go, config, Helper.RootGameObject));
		}

		public void Destroy(float cycle, string reference)
		{
			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new DestroyOccurrence(reference));
		}

		public void SetActive(float cycle, string reference, bool isActive)
		{
			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new SetActiveOccurrence(reference, isActive));
		}

		public void ChangeTransform(float cycle, string reference, float durationCycles, ChangeTransformConfig config)
		{
			BaseAction<ChangeTransformOccurrence, ChangeTransformConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeAnimatorVariable(float cycle, string reference, ChangeAnimatorVariableConfig config)
		{
			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new ChangeAnimatorVarOccurrence(reference, config));
		}

		public void ChangeAnimatorState(float cycle, string reference, ChangeAnimatorStateConfig config)
		{
			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new ChangeAnimatorStateOccurrence(reference, config));
		}

		public void ChangeAudioSource(float cycle, string reference, AudioSourceConfig config)
		{
			config.AudioClip = BundleManager.Instance.LoadAsset<AudioClip>(config.BundleName, config.AssetName);

			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new ChangeAudioSourceOccurrence(reference, config));
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

			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new InstantiateOccurrence(reference, go, new InstantiateConfig
				{
					Position = new Vector3(),
					ParentReference = parentReference,
				}, Helper.UserCanvasGameObject));

			ChangeRectTransform(cycle, reference, 0, config);
		}

		public void ChangeRectTransform(float cycle, string reference,
			float durationCycles, ChangeRectTransformConfig config)
		{
			BaseAction<ChangeUIElementOccurrence, ChangeRectTransformConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeText(float cycle, string reference,
			ChangeTextConfig config)
		{
			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new ChangeTextOccurrence(reference, config));
		}

		public void ChangeSlider(float cycle, string reference,
			float durationCycles, ChangeSliderConfig config)
		{
			BaseAction<ChangeSliderOccurrence, ChangeSliderConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeRawImage(float cycle, string reference,
			float durationCycles, ChangeRawImageConfig config)
		{
			config.Texture = BundleManager.Instance.LoadAsset<Texture>(config.BundleName, config.AssetName);

			BaseAction<ChangeRawImageOccurrence, ChangeRawImageConfig>(cycle, reference, durationCycles, config);
		}

		public void CreateEmptyGameObject(float cycle, string reference, InstantiateConfig config)
		{
			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new InstantiateOccurrence(reference, null, config, Helper.RootGameObject));
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
			GameObject go;
			switch (type)
			{
				case EBasicObjectType.Sprite:
				case EBasicObjectType.AudioSource:
				case EBasicObjectType.Ellipse2D:
				case EBasicObjectType.Polygon2D:
				case EBasicObjectType.Line:
					go = Resources.Load("BasicObjects/" + type.ToString()) as GameObject;
					break;
				default:
					throw new System.NotSupportedException("type is not supported");
			}

			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new InstantiateOccurrence(reference, go, config, Helper.RootGameObject));
		}

		public void ChangeSprite(float cycle, string reference,
			float durationCycles, ChangeSpriteConfig config)
		{
			config.Sprite = BundleManager.Instance.LoadAsset<Sprite>(config.BundleName, config.AssetName);

			BaseAction<ChangeSpriteOccurrence, ChangeSpriteConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeMaterial(float cycle, string reference,
			ChangeMaterialConfig config)
		{
			config.Material = BundleManager.Instance.LoadAsset<Material>(config.BundleName, config.AssetName);

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

		public class InstantiateConfig
		{
			public Vector3 Position { get; set; }
			public Vector3? Rotation { get; set; }
			public Vector3? Scale { get; set; }
			public string ParentReference { get; set; }
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
			public string NewValue { get; set; }
		}

		public class ChangeAnimatorStateConfig
		{
			public string NewStateName { get; set; }
			public int Layer { get; set; }
			public bool SaveCurrentNormalizedTime { get; set; }
			public float NewNormalizedTime { get; set; }

			public ChangeAnimatorStateConfig()
			{
				Layer = 0;
				SaveCurrentNormalizedTime = true;
				NewNormalizedTime = 0;
			}
		}

		public class AudioSourceConfig
		{
			public string BundleName { get; set; }
			public string AssetName { get; set; }
			public AudioClip AudioClip { get; set; }
			public float? Time { get; set; }
			public bool? Mute { get; set; }
			public bool? Loop { get; set; }
			public int? Priority { get; set; }
			public float? Volume { get; set; }
			public float? SpatialBlend { get; set; } // is 3D or not
			public bool? Play { get; set; }
			public bool? Stop { get; set; }
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
			public string Text { get; set; }
			public float? FontSize { get; set; }
			public TextAlignmentOptions? Alignment { get; set; }
			public float? WordWrappingRatios { get; set; }
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
			public string BundleName { get; set; }
			public string AssetName { get; set; }
			public Texture Texture { get; set; }
			public ChangeVector4Config Color { get; set; }
			public ChangeVector4Config UVRect { get; set; }
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
			public string BundleName { get; set; }
			public string AssetName { get; set; }
			public Sprite Sprite { get; set; }
			public ChangeVector4Config Color { get; set; }
			public bool? FlipX { get; set; }
			public bool? FlipY { get; set; }
			public int? Order { get; set; }
		}

		public class ChangeMaterialConfig
		{
			public string BundleName { get; set; }
			public string AssetName { get; set; }
			public Material Material { get; set; }
			public int Index { get; set; }
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
			public bool SuddenChange { get; set; }

			public ChangeLineConfig()
			{
				SuddenChange = false;
			}
		}
		#endregion
	}
}
