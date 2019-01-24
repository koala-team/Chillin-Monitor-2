using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SFB;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RestSharp.Contrib;
using System;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

namespace Koala
{
	public class MainMenuManager : MonoBehaviour
	{
		private bool TryConnect { get; set; } = false;
		private ExtensionFilter[] ReplayExtensions { get; set; } = new[] {
			new ExtensionFilter("Chillin Replay", "cr" ),
		};
		private ExtensionFilter[] BundleExtensions { get; set; } = new[] {
			new ExtensionFilter("Chillin Bundle", "cb" ),
		};

		public InputField m_ipInputText;
		public InputField m_portInputText;
		public Button m_connectButton;
		public Button m_cancelButton;
		public Button m_loadReplayButton;
		public Slider m_loadReplayProgress;
		public Button m_addAssetBundleButton;
		public GameObject m_assetBundlesList;
		public GameObject m_assetBundlesGame;
		public GameObject m_assetBundlesItem;


#if UNITY_WEBGL && !UNITY_EDITOR
		//
		// WebGL
		//
		[DllImport("__Internal")]
		private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

		// Called from browser
		public void OnReplayUpload(string url) {
			StartCoroutine(DownloadReplay(url));
		}

		// Called from browser
		public void OnAssetBundleUpload(string url) {
			StartCoroutine(DownloadAssetBundle(url));
		}
#endif

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
#if UNITY_EDITOR || UNITY_STANDALONE
			bool isDone = false;
			string uri = null;

#if UNITY_STANDALONE_LINUX && !UNITY_EDITOR
			var paths = new string[0];
			var intPtrPath = TinyFileDialogs.tinyfd_openFileDialog("Select Replay", PlayerConfigs.LastOpenFileAddress, 1, new string[1] { "*.cr" }, "Chillin Replay", 0);
			string path = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(intPtrPath);
			if (path != null && path.Length > 0) paths = new string[1] { path };
#else
			StandaloneFileBrowser.OpenFilePanelAsync("Select Replay", "", ReplayExtensions, false, (string[] paths) =>
			{
#endif
				if (paths.Length > 0)
				{
					uri = new Uri(paths[0]).AbsoluteUri;
#if UNITY_STANDALONE_LINUX && !UNITY_EDITOR
					PlayerConfigs.LastOpenFileAddress = uri;
#endif
				}

				isDone = true;
#if !UNITY_STANDALONE_LINUX || UNITY_EDITOR
			});
#endif

			yield return new WaitUntil(() => isDone);
			StartCoroutine(DownloadReplay(uri));
#elif UNITY_WEBGL
			UploadFile(gameObject.name, "OnReplayUpload", ".cr", false);
#endif

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
				var req = new WWW(uri);
#pragma warning restore CS0618 // Type or member is obsolete

				while (!req.isDone)
				{
					m_loadReplayProgress.value = req.progress;
					yield return new WaitForEndOfFrame();
				}

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

#if UNITY_EDITOR || UNITY_STANDALONE
			bool isDone = false;
			string uri = null;

#if UNITY_STANDALONE_LINUX && !UNITY_EDITOR
			var paths = new string[0];
			var intPtrPath = TinyFileDialogs.tinyfd_openFileDialog("Select Asset Bundle", PlayerConfigs.LastOpenFileAddress, 1, new string[1] { "*.cb" }, "Chillin Bundle", 0);
			string path = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(intPtrPath);
			if (path != null && path.Length > 0) paths = new string[1] { path };
#else
			StandaloneFileBrowser.OpenFilePanelAsync("Select Asset Bundle", "", BundleExtensions, false, (string[] paths) =>
			{
#endif
				if (paths.Length > 0)
				{
					uri = new Uri(paths[0]).AbsoluteUri;
#if UNITY_STANDALONE_LINUX && !UNITY_EDITOR
					PlayerConfigs.LastOpenFileAddress = uri;
#endif
				}

				isDone = true;
#if !UNITY_STANDALONE_LINUX || UNITY_EDITOR
			});
#endif

			yield return new WaitUntil(() => isDone);
			StartCoroutine(DownloadAssetBundle(uri));
#elif UNITY_WEBGL
			UploadFile(gameObject.name, "OnAssetBundleUpload", ".cb", false);
#endif

					yield return null;
		}

		private IEnumerator DownloadAssetBundle(string uri)
		{
			if (uri != null && uri.Length > 0)
			{
#pragma warning disable CS0618 // Type or member is obsolete
				var req = new WWW(uri);
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

				foreach (string bundleName in BundleManager.Instance.Bundles[gameName].Keys)
				{
					var newBundle = GameObject.Instantiate(m_assetBundlesItem, newGame.transform, false);
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_assetBundlesList.GetComponent<RectTransform>());
			yield return new WaitForEndOfFrame();
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_assetBundlesList.transform.parent.GetComponent<RectTransform>());
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
