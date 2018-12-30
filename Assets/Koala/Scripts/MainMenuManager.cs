using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SFB;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
#if false
using UnityEngine.Networking;
#endif

namespace Koala
{
	public class MainMenuManager : MonoBehaviour
	{
		private bool TryConnect { get; set; } = false;
		private ExtensionFilter[] Extensions { get; set; } = new[] {
			new ExtensionFilter("Chillin Replay", "cr" ),
		};

		public InputField m_ipInputText;
		public InputField m_portInputText;
		public Button m_connectButton;
		public Button m_cancelButton;
		public Button m_loadReplayButton;
		public Slider m_loadReplayProgress;


#if UNITY_WEBGL && !UNITY_EDITOR
		//
		// WebGL
		//
		[DllImport("__Internal")]
		private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

		// Called from browser
		public void OnFileUpload(string url) {
			StartCoroutine(DownloadReplay(url));
		}
#endif

		public void Start()
		{
			PlayerConfigs.Init();

			m_ipInputText.text = PlayerConfigs.IP;
			m_portInputText.text = PlayerConfigs.Port.ToString();
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

		public void ShowLoadReplayDialog()
		{
			StartCoroutine(GetReplayBytes());
		}

		private IEnumerator GetReplayBytes()
		{
			m_connectButton.gameObject.SetActive(false);
			m_loadReplayButton.gameObject.SetActive(false);
			m_loadReplayProgress.gameObject.SetActive(true);

#if UNITY_EDITOR || UNITY_STANDALONE
			bool isDone = false;
			string uri = null;

			StandaloneFileBrowser.OpenFilePanelAsync("Select Replay", "", Extensions, false, (string[] paths) =>
			{
				if (paths.Length > 0)
				{
					uri = new System.Uri(paths[0]).AbsoluteUri;
				}

				isDone = true;
			});

			yield return new WaitUntil(() => isDone);
			StartCoroutine(DownloadReplay(uri));
#elif UNITY_WEBGL
			UploadFile(gameObject.name, "OnFileUpload", ".cr", false);
#endif

			yield return null;
		}

		private IEnumerator DownloadReplay(string uri)
		{
			if (uri != null && uri.Length > 0)
			{
#if false
				var req = UnityWebRequest.Get(uri);
#else
				var req = new WWW(uri);
#endif
				
				while (!req.isDone)
				{
#if false
					m_loadReplayProgress.value = req.downloadProgress;
#else
					m_loadReplayProgress.value = req.progress;
#endif
					yield return new WaitForEndOfFrame();
				}

#if false
				if (req.isHttpError || req.isNetworkError)
#else
				if (req.error != null && req.error.Length > 0)
#endif
				{
					Debug.LogError(req.error);
				}
				else
				{
					m_loadReplayProgress.gameObject.SetActive(false);
					Helper.ReplayMode = true;
#if false
					Helper.ReplayBytes = req.downloadHandler.data;
#else
					Helper.ReplayBytes = req.bytes;
#endif
					StartCoroutine(LoadGameScene());
					yield break;
				}
			}

			m_connectButton.gameObject.SetActive(true);
			m_loadReplayButton.gameObject.SetActive(true);
			m_loadReplayProgress.gameObject.SetActive(false);
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
	}
}
