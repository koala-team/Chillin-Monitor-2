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
		
		public GameObject m_rootGameObject;
		public GameObject m_rootDestroyedGameObject;
		public GameObject m_userCanvasGameObject;
		public TextMeshProUGUI m_timeText;
		public FontItem[] m_fontItems;
		public Camera m_mainCamera;

		void Awake()
		{
			// Setup timeline
			Timeline.Instance.Reset();

			// Reset references map
			References.Instance.ResetMaps();
			References.Instance.AddGameObject(int.MaxValue.ToString(), m_mainCamera.gameObject); // "MainCamera"

			// set Helpers value
			Helper.CycleDuration = _cycleDuration;
			Helper.RootGameObject = m_rootGameObject;
			Helper.RootDestroyedGameObject = m_rootDestroyedGameObject;
			Helper.UserCanvasGameObject = m_userCanvasGameObject;
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
			m_timeText.text = "Time: " + Timeline.Instance.Time.ToString("0.0000000") +
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

			Helper.SetAnimatorsTimeScale(m_rootGameObject);
			Helper.SetAudioSourcesTimeScale(m_rootGameObject);
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

			_director.Action(new KS.SceneActions.CreateEmptyGameObject
			{
				Cycle = 0,
				Ref = 1,
			});
			_director.Action(new KS.SceneActions.CreateBasicObject
			{
				Cycle = 0,
				Ref = 2,
				ParentRef = 1,
				Type = KS.SceneActions.EBasicObjectType.Ellipse2D,
			});
			_director.Action(new KS.SceneActions.ChangeEllipse2D
			{
				Cycle = 0,
				Ref = 2,
				DurationCycles = 2,
				XRadius = 2,
				YRadius = 2,
			});
		}
	}
}
