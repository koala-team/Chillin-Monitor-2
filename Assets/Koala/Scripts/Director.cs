using UnityEngine;
using Chronos;
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

			Helper.RootTimeline.Schedule(startTime, true, forwardOccurrence);
			Helper.RootTimeline.Schedule(endTime, true, backwardOccurrence);
		}

		public void InstantiateBundleAsset(
			float cycle, string reference,
			string bundleName, string assetName, InstantiateConfig config)
		{
			GameObject go = BundleManager.Instance.GetBundle(bundleName).LoadAsset<GameObject>(assetName);

			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle, true, false), true,
				new InstantiateOccurrence(reference, go, config, Helper.RootGameObject));
		}

		public void Destroy(float cycle, string reference)
		{
			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle, true, true), true,
				new DestroyOccurrence(reference));
		}

		public void SetActive(float cycle, string reference, bool isActive)
		{
			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle, true, !isActive), true,
				new SetActiveOccurrence(reference, isActive));
		}

		public void ChangeTransform(float cycle, string reference, float durationCycles, ChangeTransformConfig config)
		{
			DOTweenAction<ChangeTransformOccurrence, ChangeTransformConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeAnimatorVariable(float cycle, string reference, ChangeAnimatorVariableConfig config)
		{
			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle), true,
				new ChangeAnimatorVarOccurrence(reference, config));
		}

		public void ChangeAnimatorState(float cycle, string reference, ChangeAnimatorStateConfig config)
		{
			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle), true,
				new ChangeAnimatorStateOccurrence(reference, config));
		}

		public void ChangeAudioSource(float cycle, string reference, AudioSourceConfig config)
		{
			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle), true,
				new ChangeAudioSourceOccurrence(reference, config));
		}

		public void DebugLog(float cycle, string message)
		{
			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle), true,
				() => Debug.Log("Forward: " + message), () => Debug.Log("Backward: " + message));
		}

		public void CreateUIElement(float cycle, string reference,
			string parentReference, EUIElementType type, ChangeUIElementConfig config)
		{
			GameObject go;
			switch (type)
			{
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

			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle, true, false), true,
				new InstantiateOccurrence(reference, go, new InstantiateConfig
				{
					Position = new Vector3(),
					Rotation = new Vector3(),
					Scale = Vector3.one,
					ParentReference = parentReference,
				}, Helper.UserCanvasGameObject));

			ChangeUIElement(cycle, reference, 0, config);
		}

		public void ChangeUIElement(float cycle, string reference,
			float durationCycles, ChangeUIElementConfig config)
		{
			DOTweenAction<ChangeUIElementOccurrence, ChangeUIElementConfig>(cycle, reference, durationCycles, config);
		}

		public void ChangeText(float cycle, string reference,
			ChangeTextConfig config)
		{
			Helper.RootTimeline.Schedule(Helper.GetCycleTime(cycle), true,
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
			public Vector3 Rotation { get; set; }
			public Vector3 Scale { get; set; }
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

		public class ChangeUIElementConfig
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
