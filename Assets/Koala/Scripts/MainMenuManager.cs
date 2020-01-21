using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using SimpleFileBrowser;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Koala
{
	public class MainMenuManager : MonoBehaviour
	{
		private static readonly FileBrowser.Filter[] CHILLIN_REPLAY_EXTENSTIONS = new FileBrowser.Filter[1] { new FileBrowser.Filter("Chillin Replay", ".cr") };
		private static readonly FileBrowser.Filter[] CHILLIN_BUNDLE_EXTENSTIONS = new FileBrowser.Filter[1] { new FileBrowser.Filter("Chillin Bundle", ".cb") };

		private bool TryConnect { get; set; } = false;

		public TMP_InputField m_ipInputText;
		public TMP_InputField m_portInputText;
		public Button m_connectButton;
		public Button m_cancelButton;
		public Button m_loadReplayButton;
		public GameObject m_loadReplayProgress;
		public Button m_addAssetBundleButton;
		public GameObject m_assetBundlesList;
		public float m_assetBundlesListMaxHeight;
		public GameObject m_assetBundlesGame;
		public GameObject m_assetBundlesItem;
		public GameObject m_globalBlackboard;


		void Awake()
		{
			QualitySettings.vSyncCount = 0;

			if (Helper.GlobalBlackboard == null)
			{
				Helper.GlobalBlackboard = Instantiate(m_globalBlackboard).GetComponent<NodeCanvas.Framework.Blackboard>();
				DontDestroyOnLoad(Helper.GlobalBlackboard);
			}
		}

		public void Start()
		{
			if (!BundleManager.Instance.IsInitiated)
				BundleManager.Instance.Init(PlayerConfigs.AssetBundlesCache);

#if UNITY_WEBGL && !UNITY_EDITOR
			if (!Helper.WebGLLoadReplayLoaded)
			{
				// Check for webgl instant see replay
				var uri = new Uri(Application.absoluteURL);
				if (uri.Query.Length > 0)
				{
					var queries = HttpUtility.ParseQueryString(uri.Query, System.Text.Encoding.ASCII);
					if (queries.Get("replay") != null)
						SceneManager.LoadScene("WebGLLoadReplay");
				}
			}
#endif

			StartCoroutine(SetupPage());
		}

		private IEnumerator SetupPage()
		{
			yield return new WaitForEndOfFrame();

			RerenderAssetBundlesPanel();

			m_ipInputText.text = PlayerConfigs.IP;
			m_portInputText.text = PlayerConfigs.Port.ToString();

#if UNITY_WEBGL && !UNITY_EDITOR
			m_ipInputText.gameObject.SetActive(false);
			m_portInputText.gameObject.SetActive(false);
			m_connectButton.gameObject.SetActive(false);
#endif
		}

		public void Connect()
		{
			StartCoroutine(TryConnectCoroutine());
		}

		public IEnumerator TryConnectCoroutine()
		{
			TryConnect = true;

			// Deactive button
			m_ipInputText.interactable = false;
			m_portInputText.interactable = false;
			m_connectButton.gameObject.SetActive(false);
			m_cancelButton.gameObject.SetActive(true);
			m_loadReplayButton.gameObject.SetActive(false);

			Network network = new Network(PlayerConfigs.IP, PlayerConfigs.Port);
			// Try to connect to network
			while (!network.IsConnected && TryConnect)
			{
				Debug.LogFormat("Connecting to {0}:{1}...", PlayerConfigs.IP, PlayerConfigs.Port);
				yield return network.Connect().WaitUntilComplete();

				if (!network.IsConnected && TryConnect)
				{
					Debug.LogWarning("Try in 3 Seconds...");
					yield return new WaitForSecondsRealtime(3);
				}
			}

			if (TryConnect)
			{
				m_cancelButton.gameObject.SetActive(false);
				Protocol protocol = new Protocol(network);
				bool isAuthenticated = true;

				if (!PlayerConfigs.OfflineMode)
				{
					var checkTokenTask = protocol.CheckToken();
					yield return checkTokenTask.WaitUntilComplete();
					isAuthenticated = checkTokenTask.Result;
				}

				if (isAuthenticated)
				{
					Helper.Protocol = protocol;
					Helper.ReplayMode = false;
					StartCoroutine(LoadGameScene());
					yield break;
				}
			}
			else
			{
				if (network.IsConnected) network.Disconnect();

				// Active button again
				m_ipInputText.interactable = true;
				m_portInputText.interactable = true;
				m_connectButton.gameObject.SetActive(true);
				m_cancelButton.gameObject.SetActive(false);
				m_loadReplayButton.gameObject.SetActive(true);
			}
		}

		public IEnumerator LoadGameScene()
		{
			yield return SceneManager.LoadSceneAsync("Game").WaitUntilComplete();
		}

		public void CancelConnect()
		{
			TryConnect = false;
		}

		public void LoadReplay()
		{
			StartCoroutine(ShowReplayDialog());
		}

		private IEnumerator ShowReplayDialog()
		{
			FileBrowser.SetFilters(false, CHILLIN_REPLAY_EXTENSTIONS);
			yield return FileBrowser.WaitForLoadDialog(false, PlayerConfigs.LastFileBrowsed, "Load Replay", "Load");
			string uri = FileBrowser.Result;

			if (FileBrowser.Success)
				PlayerConfigs.LastFileBrowsed = uri;

			StartCoroutine(DownloadReplay(uri));

			yield return null;
		}

		private IEnumerator DownloadReplay(string uri)
		{
			if (uri != null && uri.Length > 0)
			{
				SetConnectButtonIsActive(false);
				m_loadReplayButton.gameObject.SetActive(false);
				m_loadReplayProgress.gameObject.SetActive(true);

#pragma warning disable CS0618 // Type or member is obsolete
				var req = new WWW("file://" + uri);
#pragma warning restore CS0618 // Type or member is obsolete

				yield return new WaitUntil(() => req.isDone);

				if (req.error != null && req.error.Length > 0)
				{
					Debug.LogError(req.error);
				}
				else
				{
					m_loadReplayProgress.gameObject.SetActive(false);
					Helper.ReplayMode = true;
					Helper.ReplayBytes = req.bytes;
					StartCoroutine(LoadGameScene());
					yield break;
				}
			}

			SetConnectButtonIsActive(true);
			m_loadReplayButton.gameObject.SetActive(true);
			m_loadReplayProgress.gameObject.SetActive(false);
		}

		public void AddNewAssetBundle()
		{
			StartCoroutine(ShowAssetBundleDialog());
		}

		private IEnumerator ShowAssetBundleDialog()
		{
			m_addAssetBundleButton.gameObject.SetActive(true);

			FileBrowser.SetFilters(false, CHILLIN_BUNDLE_EXTENSTIONS);
			yield return FileBrowser.WaitForLoadDialog(false, PlayerConfigs.LastFileBrowsed, "Load Bundle", "Load");
			string uri = FileBrowser.Result;

			if (FileBrowser.Success)
				PlayerConfigs.LastFileBrowsed = uri;

			StartCoroutine(DownloadAssetBundle(uri));

			yield return null;
		}

		private IEnumerator DownloadAssetBundle(string uri)
		{
			if (uri != null && uri.Length > 0)
			{
#pragma warning disable CS0618 // Type or member is obsolete
				var req = new WWW("file://" + uri);
#pragma warning restore CS0618 // Type or member is obsolete

				yield return new WaitUntil(() => req.isDone);

				if (req.error != null && req.error.Length > 0)
				{
					Debug.LogError(req.error);
				}
				else
				{
					AddAssetBundle(req.bytes);
				}
			}

			m_addAssetBundleButton.gameObject.SetActive(true);
		}

		private void AddAssetBundle(byte[] bytes)
		{
			try
			{
				var bundle = AssetBundle.LoadFromMemory(bytes);
				BundleInfo bundleInfo = bundle.LoadAsset<BundleInfo>("BundleInfo");

				if (BundleManager.Instance.AddBundle(bundleInfo.gameName, bundleInfo.bundleName, bytes, bundle))
				{
					RerenderAssetBundlesPanel();
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
			}
		}

		private void RemoveAssetBundle(string gameName, string bundleName)
		{
			try
			{
				BundleManager.Instance.RemoveBundle(gameName, bundleName);
				RerenderAssetBundlesPanel();
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
			}
		}

		private void RerenderAssetBundlesPanel()
		{
			// Remove all childs
			foreach (Transform child in m_assetBundlesList.transform)
				GameObject.Destroy(child.gameObject);

			foreach (string gameName in BundleManager.Instance.Bundles.Keys)
			{
				var newGame = GameObject.Instantiate(m_assetBundlesGame, m_assetBundlesList.transform, false);
				newGame.transform.Find("GameName").GetComponent<TextMeshProUGUI>().text = gameName;

				Transform bundlesParent = newGame.transform.Find("Bundles");
				foreach (string bundleName in BundleManager.Instance.Bundles[gameName].Keys)
				{
					var newBundle = GameObject.Instantiate(m_assetBundlesItem, bundlesParent, false);
					newBundle.transform.Find("BundleName").GetComponent<TextMeshProUGUI>().text = bundleName;
					newBundle.transform.Find("RemoveButton").GetComponent<Button>().onClick.AddListener(
						() => RemoveAssetBundle(gameName, bundleName)
					);
				}
			}

			StartCoroutine(FixAssetBundlesPanel());
		}

		private IEnumerator FixAssetBundlesPanel()
		{
			RectTransform listRect = m_assetBundlesList.GetComponent<RectTransform>();
			RectTransform parentRect = m_assetBundlesList.transform.parent.GetComponent<RectTransform>();

			LayoutRebuilder.ForceRebuildLayoutImmediate(listRect);

			yield return new WaitForEndOfFrame();
			LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
			yield return new WaitForEndOfFrame();

			parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, Mathf.Min(listRect.sizeDelta.y, m_assetBundlesListMaxHeight));
		}

		public void Quit()
		{
			Application.Quit();
		}

		public void ChangeIP()
		{
			PlayerConfigs.IP = m_ipInputText.text;
		}

		public void ChangePort()
		{
			if (int.TryParse(m_portInputText.text, out int port))
				PlayerConfigs.Port = port;
			else
				PlayerConfigs.Port = 0;
		}

		private void SetConnectButtonIsActive(bool isActive)
		{
#if !UNITY_WEBGL || UNITY_EDITOR
			m_connectButton.gameObject.SetActive(isActive);
#endif
		}
	}
}
