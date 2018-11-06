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

			// Config dotween
			DOTween.Init();
			DOTween.defaultEaseType = Ease.Linear;
			DOTween.defaultUpdateType = UpdateType.Manual;
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
			//_director.CreateUIElement(1, "Text", null, EUIElementType.Text, new Director.ChangeUIElementConfig
			//{
			//	Position = new Director.ChangeVector3Config { X = 100, Y = -200 },
			//	Size = new Director.ChangeVector2Config { X = 600 },
			//	AnchorMin = new Director.ChangeVector2Config { X = 0.1f },
			//	//Scale = new Director.ChangeVector3Config { X = 10, Y = 5 },
			//	//Rotation = new Director.ChangeVector3Config { Z = 45 },
			//});
			//_director.ChangeUIElement(2, "Text", 2, new Director.ChangeUIElementConfig
			//{
			//	//Position = new Director.ChangeVector3Config { X = 100, Y = 200 },
			//	Size = new Director.ChangeVector2Config { X = 800 },
			//	//AnchorMin = new Director.ChangeVector2Config { X = 0.1f },
			//	////Scale = new Director.ChangeVector3Config { X = 10, Y = 5 },
			//	//Rotation = new Director.ChangeVector3Config { Z = 45 },
			//});
			//_director.ChangeText(2, "Text", new Director.ChangeTextConfig
			//{
			//	Text = "WTF",
			//});
			//_director.ChangeText(2, "Text", new Director.ChangeTextConfig
			//{
			//	Alignment = TextAlignmentOptions.Left,
			//});


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


			_director.InstantiateBundleAsset(2, "Cube", "main", "cube", new Director.InstantiateConfig
			{
				Position = new Vector3(0, 10, 0),
				Rotation = new Vector3(45, 45, 45),
				Scale = new Vector3(2, 1, 1),
			});
			_director.ChangeTransform(2, "Cube", 2, new Director.ChangeTransformConfig
			{
				Position = new Director.ChangeVector3Config
				{
					X = -10,
					Y = 0,
				}
			});
			_director.ChangeTransform(4, "Cube", 6, new Director.ChangeTransformConfig
			{
				Position = new Director.ChangeVector3Config
				{
					X = 5,
					Y = 5,
				}
			});
			_director.ChangeTransform(2, "Cube", 1, new Director.ChangeTransformConfig
			{
				Rotation = new Director.ChangeVector3Config
				{
					Z = 0,
				}
			});
			_director.ChangeTransform(2, "Cube", 3, new Director.ChangeTransformConfig
			{
				Scale = new Director.ChangeVector3Config
				{
					Z = 5,
				}
			});
			_director.CreateUIElement(3, "Canvas", "Cube", EUIElementType.Canvas, new Director.ChangeRectTransformConfig
			{
				Position = new Director.ChangeVector3Config { Z = -0.6f },
			});
			_director.CreateUIElement(3, "CubeSlider", "Canvas", EUIElementType.Slider, new Director.ChangeRectTransformConfig
			{
				Position = new Director.ChangeVector3Config { X = 100, Y = -100 },
			});
			_director.ChangeSlider(3, "Canvas/CubeSlider", 1, new Director.ChangeSliderConfig
			{
				Value = 0.5f,
			});
			_director.CreateEmptyGameObject(2, "Child1", new Director.InstantiateConfig
			{
				Position = Vector3.one,
				ParentReference = "Cube",
			});
			_director.CreateEmptyGameObject(2, "Child2", new Director.InstantiateConfig
			{
				Position = Vector3.one,
				ParentReference = "Cube",
			});
			_director.CreateEmptyGameObject(2, "Child3", new Director.InstantiateConfig
			{
				Position = Vector3.one,
				ParentReference = "Cube",
			});
			_director.ChangeSiblingOrder(3, "Child2", new Director.ChangeSiblingOrderConfig
			{
				GotoLast = true,
			});
			_director.ChangeSiblingOrder(3.5f, "Child2", new Director.ChangeSiblingOrderConfig
			{
				GotoFirst = true,
			});
			_director.ChangeSiblingOrder(4, "Child2", new Director.ChangeSiblingOrderConfig
			{
				SiblingReferenceAsBaseIndex = "Child1",
				ChangeIndex = 2,
			});
			_director.ChangeSiblingOrder(4.5f, "Child2", new Director.ChangeSiblingOrderConfig
			{
				ChangeIndex = -2,
			});
			_director.ChangeSiblingOrder(5, "Child2", new Director.ChangeSiblingOrderConfig
			{
				NewIndex = 1000,
			});
			_director.ChangeSiblingOrder(5.5f, "Child2", new Director.ChangeSiblingOrderConfig
			{
				NewIndex = 0,
			});
			_director.ChangeSiblingOrder(6, "Child2", new Director.ChangeSiblingOrderConfig
			{
				SiblingReferenceAsBaseIndex = "Child1",
				ChangeIndex = 0,
			});
			_director.ChangeSiblingOrder(6.5f, "Child2", new Director.ChangeSiblingOrderConfig
			{
				SiblingReferenceAsBaseIndex = "Child1",
				ChangeIndex = -1,
			});
			_director.Destroy(15, "Cube");


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
			//	NewStateName = "Explosion"
			//});
			//_director.ChangeAnimatorState(10, "Sprite", new Director.ChangeAnimatorStateConfig
			//{
			//	NewStateName = "Pacman",
			//});
			//_director.ChangeAnimatorVariable(1, "Sprite", new Director.ChangeAnimatorVariableConfig
			//{
			//	VarName = "monitorTimeScale",
			//	VarType = (EAnimatorVariableType)1,
			//	NewValue = "1",
			//});


			//_director.InstantiateBundleAsset(1, "Audio", "main", "testaudio", new Director.InstantiateConfig
			//{
			//	Position = new Vector3(),
			//	Rotation = new Vector3(),
			//	Scale = new Vector3(),
			//});
			//_director.ChangeAudioSource(2, "Audio", new Director.AudioSourceConfig
			//{
			//	BundleName = "main",
			//	AssetName = "carengine",
			//	Play = true,
			//});
			//_director.ChangeAudioSource(20, "Audio", new Director.AudioSourceConfig
			//{
			//	Play = false,
			//});
		}
	}
}
