using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using DG.Tweening;

namespace Koala
{

	public class GameManager : MonoBehaviour
	{
		public static readonly float TIME_SCALE_DELTA = 0.25f;

		private Director _director;
		private float _cycleDuration = 0.25f; // TODO: get from server
		
		public GameObject rootGameObject;
		public GameObject rootDestroyedGameObject;
		public GameObject userCanvasGameObject;
		public TextMeshProUGUI timeText;
		public FontItem[] m_fontItems;

		void Awake()
		{
			// Setup timeline
			Timeline.Instance.Reset();

			// Reset references map
			References.Instance.ResetMaps();

			// set Helpers value
			Helper.CycleDuration = _cycleDuration;
			Helper.RootGameObject = rootGameObject;
			Helper.RootDestroyedGameObject = rootDestroyedGameObject;
			Helper.UserCanvasGameObject = userCanvasGameObject;
			Helper.Fonts = m_fontItems;

			// Config Tweens
			TweensManager.Instance.Reset();
			DOTween.Init();
			DOTween.defaultEaseType = Ease.Linear;
			DOTween.defaultUpdateType = UpdateType.Manual;
			DOTween.useSafeMode = false;
		}

		void Start()
		{
			StartCoroutine(DownloadBundle());
		}

		void Update()
		{
			Timeline.Instance.Update(Time.deltaTime);

			// Update Time Text
			timeText.text = "Time: " + Timeline.Instance.Time.ToString("0.0000000") +
							"\nCycle: " + (Timeline.Instance.Time / _cycleDuration).TruncateDecimal(1).ToString("0.0") +
							"\nSpeed: " + Timeline.Instance.TimeScale.ToString() +
							"\nCycle Duration: " + _cycleDuration.ToString();

			// Control Time
			if (Input.GetKeyDown(KeyCode.P) && Timeline.Instance.TimeScale != 0)
			{
				ChangeTimeScale(-Timeline.Instance.TimeScale); // Pause
				//Debug.Log("Pause");
			}
			else if (Input.GetKeyDown(KeyCode.LeftBracket) && Timeline.Instance.Time > 0)
			{
				ChangeTimeScale(-TIME_SCALE_DELTA); // Rewind
				//Debug.Log("Rewind: " + Timeline.Instance.TimeScale.ToString());
			}
			else if (Input.GetKeyDown(KeyCode.RightBracket))
			{
				ChangeTimeScale(TIME_SCALE_DELTA); // Forward
				//Debug.Log("Play: " + Timeline.Instance.TimeScale.ToString());
			}
		}

		private void ChangeTimeScale(float amount)
		{
			Timeline.Instance.TimeScale += amount;

			Helper.SetAnimatorsTimeScale(rootGameObject);
			Helper.SetAudioSourcesTimeScale(rootGameObject);
		}

		private IEnumerator DownloadBundle()
		{
			string uri = "http://127.0.0.1:8081/asset1";
			var request = UnityWebRequest.Get(uri);
			yield return request.SendWebRequest();
			var bytes = request.downloadHandler.data;
			AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
			BundleManager.Instance.AddBundle("main", bundle);

			foreach (var name in bundle.GetAllAssetNames())
			{
				Debug.Log(name);
			}

			_director = new Director();
			Debug.Log("Bundle Loaded");

			// Scenarios
			_director.CreateUIElement(1, "Text", null, EUIElementType.Text, new Director.ChangeRectTransformConfig
			{
				Position = new Director.ChangeVector3Config { X = 100, Y = -200 },
				Size = new Director.ChangeVector2Config { X = 600 },
				AnchorMin = new Director.ChangeVector2Config { X = 0.1f },
			});
			_director.ChangeRectTransform(2, "Text", 2, new Director.ChangeRectTransformConfig
			{
				Size = new Director.ChangeVector2Config { X = 800 },
				Scale = new Director.ChangeVector3Config { X = 10, Y = 5 },
				Rotation = new Director.ChangeVector3Config { Z = 45 },
			});
			_director.ChangeText(2, "Text", 2, new Director.ChangeTextConfig
			{
				Text = "WTF",
				FontSize = 30,
			});
			_director.ChangeText(2, "Text", 0, new Director.ChangeTextConfig
			{
				Alignment = TextAlignmentOptions.Left,
			});
			_director.ChangeText(2, "Text", 0, new Director.ChangeTextConfig
			{
				FontName = "agitprop",
			});
			_director.ChangeText(4, "Text", 0, new Director.ChangeTextConfig
			{
				BundleName = "main",
				AssetName = "anton sdf",
			});


			//_director.CreateUIElement(1, "Slider", null, EUIElementType.Slider, new Director.ChangeRectTransformConfig
			//{
			//	Position = new Director.ChangeVector3Config { X = 300, Y = -200 },
			//	Size = new Director.ChangeVector2Config { X = 600 },
			//});
			//_director.ChangeSlider(2, "Slider", 2, new Director.ChangeSliderConfig
			//{
			//	Value = 0.5f,
			//	FillColor = new Director.ChangeVector4Config { X = 0, Y = 255, Z = 128 }
			//});
			//_director.ChangeSlider(2, "Slider", 2, new Director.ChangeSliderConfig
			//{
			//	BackgroundColor = new Director.ChangeVector4Config { Y = 0 }
			//});
			//_director.ChangeSlider(4, "Slider", 0, new Director.ChangeSliderConfig
			//{
			//	Direction = UnityEngine.UI.Slider.Direction.TopToBottom,
			//});


			//_director.CreateUIElement(1, "RawImage", null, EUIElementType.RawImage, new Director.ChangeRectTransformConfig
			//{
			//	Position = new Director.ChangeVector3Config { X = 500, Y = -200 },
			//	Size = new Director.ChangeVector2Config { X = 600 },
			//});
			//_director.ChangeRawImage(2, "RawImage", 2, new Director.ChangeRawImageConfig
			//{
			//	BundleName = "main",
			//	AssetName = "pacman_move",
			//	Color = new Director.ChangeVector4Config { X = 0 },
			//	UVRect = new Director.ChangeVector4Config { Z = 10 },
			//});


			//_director.InstantiateBundleAsset(2, "Cube", new Director.InstantiateBundleAssetConfig
			//{
			//	BundleName = "main",
			//	AssetName = "cube",
			//	Position = new Vector3(0, 10, 0),
			//	Rotation = new Vector3(45, 45, 45),
			//	Scale = new Vector3(2, 1, 1),
			//});
			//_director.ChangeTransform(2, "Cube", 2, new Director.ChangeTransformConfig
			//{
			//	Position = new Director.ChangeVector3Config
			//	{
			//		X = -10,
			//		Y = 0,
			//	}
			//});
			//_director.ChangeTransform(4, "Cube", 6, new Director.ChangeTransformConfig
			//{
			//	Position = new Director.ChangeVector3Config
			//	{
			//		X = 5,
			//		Y = 5,
			//	}
			//});
			//_director.ChangeTransform(2, "Cube", 1, new Director.ChangeTransformConfig
			//{
			//	Rotation = new Director.ChangeVector3Config
			//	{
			//		Z = 0,
			//	}
			//});
			//_director.ChangeTransform(2, "Cube", 3, new Director.ChangeTransformConfig
			//{
			//	Scale = new Director.ChangeVector3Config
			//	{
			//		Z = 5,
			//	}
			//});
			//_director.CreateUIElement(3, "Canvas", "Cube", EUIElementType.Canvas, new Director.ChangeRectTransformConfig
			//{
			//	Position = new Director.ChangeVector3Config { Z = -0.6f },
			//});
			//_director.CreateUIElement(3, "CubeSlider", "Canvas", EUIElementType.Slider, new Director.ChangeRectTransformConfig
			//{
			//	Position = new Director.ChangeVector3Config { X = 100, Y = -100 },
			//});
			//_director.ChangeSlider(3, "Canvas/CubeSlider", 1, new Director.ChangeSliderConfig
			//{
			//	Value = 0.5f,
			//});
			//_director.CreateEmptyGameObject(2, "Child1", new Director.InstantiateConfig
			//{
			//	Position = Vector3.one,
			//	ParentReference = "Cube",
			//});
			//_director.CreateEmptyGameObject(2, "Child2", new Director.InstantiateConfig
			//{
			//	Position = Vector3.one,
			//	ParentReference = "Cube",
			//});
			//_director.CreateEmptyGameObject(2, "Child3", new Director.InstantiateConfig
			//{
			//	Position = Vector3.one,
			//	ParentReference = "Cube",
			//});
			//_director.ChangeSiblingOrder(3, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	GotoLast = true,
			//});
			//_director.ChangeSiblingOrder(3.5f, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	GotoFirst = true,
			//});
			//_director.ChangeSiblingOrder(4, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	SiblingReferenceAsBaseIndex = "Child1",
			//	ChangeIndex = 2,
			//});
			//_director.ChangeSiblingOrder(4.5f, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	ChangeIndex = -2,
			//});
			//_director.ChangeSiblingOrder(5, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	NewIndex = 1000,
			//});
			//_director.ChangeSiblingOrder(5.5f, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	NewIndex = 0,
			//});
			//_director.ChangeSiblingOrder(6, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	SiblingReferenceAsBaseIndex = "Child1",
			//	ChangeIndex = 0,
			//});
			//_director.ChangeSiblingOrder(6.5f, "Child2", new Director.ChangeSiblingOrderConfig
			//{
			//	SiblingReferenceAsBaseIndex = "Child1",
			//	ChangeIndex = -1,
			//});
			//_director.Destroy(15, "Cube");


			//_director.InstantiateBundleAsset(0, "Particle", "main", "particle", new Director.InstantiateConfig
			//{
			//	Position = Vector3.one,
			//});
			//_director.ManageComponents(0, "Particle", new Director.ManageComponentsConfig
			//{
			//	Type = EComponentType.ParticleSystemManager,
			//	Add = true,
			//});


			//_director.InstantiateBundleAsset(1, "Sprite", "main", "sprite", new Director.InstantiateConfig
			//{
			//	Position = new Vector3(),
			//	Rotation = new Vector3(),
			//	Scale = Vector3.one,
			//});
			//_director.ChangeAnimatorState(2, "Sprite", new Director.ChangeAnimatorStateConfig
			//{
			//	StateName = "Explosion"
			//});
			//_director.ChangeAnimatorState(10, "Sprite", new Director.ChangeAnimatorStateConfig
			//{
			//	StateName = "Pacman",
			//});
			//_director.ChangeAnimatorVariable(3, "Sprite", new Director.ChangeAnimatorVariableConfig
			//{
			//	VarName = "monitorTimeScale",
			//	VarType = (EAnimatorVariableType)1,
			//	Value = "5",
			//});


			//_director.InstantiateBundleAsset(1, "Audio", "main", "testaudio", new Director.InstantiateConfig
			//{
			//	Position = new Vector3(),
			//	Rotation = new Vector3(),
			//	Scale = new Vector3(),
			//});
			//_director.ChangeAudioSource(1, "Audio", 0, new Director.ChangeAudioSourceConfig
			//{
			//	BundleName = "main",
			//	AssetName = "carengine",
			//	Play = true,
			//});
			//_director.ChangeAudioSource(2, "Audio", 4, new Director.ChangeAudioSourceConfig
			//{
			//	Volume = 0.1f,
			//});
			//_director.ChangeAudioSource(6, "Audio", 4, new Director.ChangeAudioSourceConfig
			//{
			//	Volume = 1,
			//});
			//_director.ChangeAudioSource(20, "Audio", 0, new Director.ChangeAudioSourceConfig
			//{
			//	Play = false,
			//});


			//_director.CreateBasicObject(1, "BasicObject1", null, EBasicObjectType.Sprite, new Director.InstantiateConfig
			//{
			//	Position = Vector3.zero,
			//});
			//_director.CreateBasicObject(1, "BasicObject2", null, EBasicObjectType.AudioSource, new Director.InstantiateConfig
			//{
			//	Position = Vector3.zero,
			//});


			//_director.CreateBasicObject(1, "Sprite", null, EBasicObjectType.Sprite, new Director.InstantiateConfig
			//{
			//	Position = Vector3.zero,
			//});
			//_director.ChangeSprite(1, "Sprite", 0, new Director.ChangeSpriteConfig
			//{
			//	BundleName = "main",
			//	AssetName = "finger_icon square",
			//});
			//_director.ChangeSprite(2, "Sprite", 0, new Director.ChangeSpriteConfig
			//{
			//	FlipX = true,
			//	FlipY = true,
			//	Order = 10,
			//});


			//_director.InstantiateBundleAsset(1, "Cube", "main", "cube", new Director.InstantiateConfig
			//{
			//	Position = Vector3.zero,
			//});
			//_director.ChangeMaterial(2, "Cube", new Director.ChangeMaterialConfig
			//{
			//	BundleName = "main",
			//	AssetName = "material",
			//	Index = 0,
			//});


			//_director.CreateUIElement(1, "Panel", null, EUIElementType.Panel, new Director.ChangeRectTransformConfig { });
			//_director.ChangeRawImage(1, "Panel", 0, new Director.ChangeRawImageConfig
			//{
			//	Color = new Director.ChangeVector4Config { X = 255, Y = 0, Z = 0, W = 0.5f },
			//});


			//_director.InstantiateBundleAsset(1, "Cube", "main", "cube", new Director.InstantiateConfig
			//{
			//	Position = new Vector3(0, 10, 0),
			//});
			//_director.ChangeTransform(2, "Cube", 8, new Director.ChangeTransformConfig
			//{
			//	Position = new Director.ChangeVector3Config { Y = -10 },
			//});
			//_director.ChangeTransform(2, "Cube", 5, new Director.ChangeTransformConfig
			//{
			//	Rotation = new Director.ChangeVector3Config { X = 45, Y = 45 },
			//});


			//_director.CreateBasicObject(0, "Ellipse", null, EBasicObjectType.Ellipse2D, new Director.InstantiateConfig
			//{
			//	Position = -Vector3.one,
			//});
			//_director.ChangeEllipse2D(1, "Ellipse", 5, new Director.ChangeEllipse2DConfig
			//{
			//	FillColor = new Director.ChangeVector4Config { X = 0, Y = 0 },
			//	XRadius = 2,
			//	YRadius = 1,
			//});

			//_director.CreateBasicObject(0, "Polygon", null, EBasicObjectType.Polygon2D, new Director.InstantiateConfig
			//{
			//	Position = Vector3.one,
			//});
			//_director.ChangePolygon2D(1, "Polygon", 0, new Director.ChangePolygon2DConfig
			//{
			//	FillColor = new Director.ChangeVector4Config { X = 0, Z = 0 },
			//	Vertices = new System.Collections.Generic.List<Director.ChangeVector2Config>
			//	{
			//		new Director.ChangeVector2Config{ X = 0, Y = 0 },
			//		new Director.ChangeVector2Config{ X = 1, Y = 0 },
			//		new Director.ChangeVector2Config{ X = 1, Y = 1 },
			//	},
			//});
			//_director.ChangePolygon2D(1, "Polygon", 4, new Director.ChangePolygon2DConfig
			//{
			//	Vertices = new System.Collections.Generic.List<Director.ChangeVector2Config>
			//	{
			//		new Director.ChangeVector2Config{ X = 1, Y = 0 },
			//		new Director.ChangeVector2Config{ X = 1, Y = 1 },
			//		new Director.ChangeVector2Config{ X = 0, Y = 0 },
			//	},
			//});
			//_director.ChangePolygon2D(5, "Polygon", 1, new Director.ChangePolygon2DConfig
			//{
			//	FillColor = new Director.ChangeVector4Config { X = 255, Y = 0 },
			//	Vertices = new System.Collections.Generic.List<Director.ChangeVector2Config>
			//	{
			//		new Director.ChangeVector2Config{ X = 1, Y = 0 },
			//		new Director.ChangeVector2Config{ X = 1, Y = 1 },
			//		new Director.ChangeVector2Config{ X = 0, Y = 1 },
			//		new Director.ChangeVector2Config{ X = 0, Y = 0 },
			//	},
			//});
			//_director.ChangePolygon2D(5, "Polygon", 5, new Director.ChangePolygon2DConfig
			//{
			//	Vertices = new System.Collections.Generic.List<Director.ChangeVector2Config>
			//	{
			//		new Director.ChangeVector2Config{ X = 5, Y = 0 },
			//		new Director.ChangeVector2Config{ X = 5, Y = 5 },
			//		null,
			//		null,
			//	},
			//});

			//_director.CreateBasicObject(0, "Line", null, EBasicObjectType.Line, new Director.InstantiateConfig
			//{
			//	Position = new Vector3(-5, 5),
			//});
			//_director.ChangeLine(0, "Line", 0, new Director.ChangeLineConfig
			//{
			//	FillColor = new Director.ChangeVector4Config { X = 0, Y = 0 },
			//	Width = 0,
			//	Vertices = new System.Collections.Generic.List<Director.ChangeVector3Config>
			//	{
			//		new Director.ChangeVector3Config{ X = 0, Y = 0, Z = 2 },
			//		new Director.ChangeVector3Config{ X = 1, Y = 0, Z = 0 },
			//		new Director.ChangeVector3Config{ X = 1, Y = 1, Z = -2 },
			//	},
			//});
			//_director.ChangeLine(0, "Line", 4, new Director.ChangeLineConfig
			//{
			//	Width = 1,
			//	CornerVertices = 90,
			//	EndCapVertices = 90,
			//	Loop = true,
			//	Vertices = new System.Collections.Generic.List<Director.ChangeVector3Config>
			//	{
			//		new Director.ChangeVector3Config{ X = 5, Y = 5, Z = 2 },
			//		new Director.ChangeVector3Config{ X = -5, Y = 2, Z = 0 },
			//		new Director.ChangeVector3Config{ X = -3, Y = -3, Z = -2 },
			//	},
			//});


			//_director.InstantiateBundleAsset(2, "Cube", "main", "cube", new Director.InstantiateConfig
			//{
			//	Position = new Vector3(0, 0, 0),
			//});
			//_director.ChangeIsActive(3, "Cube", new Director.ChangeIsActiveConfig
			//{
			//	IsActive = false,
			//});
			//_director.ChangeIsActive(5, "Cube", new Director.ChangeIsActiveConfig
			//{
			//	IsActive = true,
			//});
		}
	}
}
