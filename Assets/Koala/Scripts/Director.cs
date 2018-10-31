using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Koala
{
	// This Class create and manage occurrences
	public class Director
	{
		public Director()
		{
		}

		#region Functions
		private void DOTweenAction<T, C>(float cycle, string reference, float durationCycles, C config)
			where T : Occurrence, IDOTweenOccurrence<T, C>, new()
			where C : class
		{
			float startTime, endTime, duration;
			Helper.GetCyclesDurationTime(cycle, durationCycles, out startTime, out endTime, out duration);

			T forwardOccurrence = new T();
			T backwardOccurrence = new T();

			forwardOccurrence.Init(reference, duration, config, true, null);
			backwardOccurrence.Init(reference, duration, null, false, forwardOccurrence);

			Timeline.Instance.Schedule(startTime, forwardOccurrence);
			Timeline.Instance.Schedule(endTime, backwardOccurrence);
		}

		public void InstantiateBundleAsset(
			float cycle, string reference,
			string bundleName, string assetName, InstantiateConfig config)
		{
			GameObject go = BundleManager.Instance.GetBundle(bundleName).LoadAsset<GameObject>(assetName);

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
			DOTweenAction<ChangeTransformOccurrence, ChangeTransformConfig>(cycle, reference, durationCycles, config);
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
					go = Resources.Load("UIElements/Canvas") as GameObject;
					break;
				case EUIElementType.Text:
					go =  Resources.Load("UIElements/Text") as GameObject;
					break;
				case EUIElementType.Slider:
					go = Resources.Load("UIElements/Slider") as GameObject;
					break;
				case EUIElementType.RawImage:
					go = Resources.Load("UIElements/RawImage") as GameObject;
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
			DOTweenAction<ChangeUIElementOccurrence, ChangeRectTransformConfig>(cycle, reference, durationCycles, config);
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
			DOTweenAction<ChangeSliderOccurrence, ChangeSliderConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeRawImage(float cycle, string reference,
			float durationCycles, ChangeRawImageConfig config)
		{
			DOTweenAction<ChangeRawImageOccurrence, ChangeRawImageConfig>(cycle, reference, durationCycles, config);
		}

		public void CreateEmptyGameObject(float cycle, string reference, InstantiateConfig config)
		{
			GameObject go = new GameObject();

			Timeline.Instance.Schedule(Helper.GetCycleTime(cycle),
				new InstantiateOccurrence(reference, go, config, Helper.RootGameObject));
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
			public string AudioClipName { get; set; }
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
			public ChangeVector4Config Color { get; set; }
			public ChangeVector4Config UVRect { get; set; }
		}
		#endregion
	}
}
