using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SimpleFileBrowser;

namespace Koala
{
	public class MainMenuManager : MonoBehaviour
	{
		private bool _tryConnect = false;

		public InputField m_ipInputText;
		public InputField m_portInputText;
		public Button m_connectButton;
		public Button m_cancelButton;
		public Button m_loadReplayButton;


		public void Start()
		{
			PlayerConfigs.Init();

			m_ipInputText.text = PlayerConfigs.IP;
			m_portInputText.text = PlayerConfigs.Port.ToString();

			FileBrowser.SetFilters(false, new FileBrowser.Filter("Chillin Replay", ".cr"));
		}

		public void Connect()
		{
			StartCoroutine(TryConnectCoroutine());
		}

		public IEnumerator TryConnectCoroutine()
		{
			_tryConnect = true;

			// Deactive button
			m_ipInputText.interactable = false;
			m_portInputText.interactable = false;
			m_connectButton.gameObject.SetActive(false);
			m_cancelButton.gameObject.SetActive(true);
			m_loadReplayButton.gameObject.SetActive(false);

			Network network = new Network(PlayerConfigs.IP, PlayerConfigs.Port);
			// Try to connect to network
			while (!network.IsConnected && _tryConnect)
			{
				Debug.LogFormat("Connecting to {0}:{1}...", PlayerConfigs.IP, PlayerConfigs.Port);
				yield return network.Connect().WaitUntilComplete();

				if (!network.IsConnected && _tryConnect)
				{
					Debug.LogWarning("Try in 3 Seconds...");
					yield return new WaitForSecondsRealtime(3);
				}
			}

			if (_tryConnect)
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
			_tryConnect = false;
		}

		public void ShowLoadReplayDialog()
		{
			StartCoroutine(ShowLoadReplayDialogCoroutine());
		}

		private IEnumerator ShowLoadReplayDialogCoroutine()
		{
			m_connectButton.gameObject.SetActive(false);
			m_loadReplayButton.interactable = false;

			// Show a load file dialog and wait for a response from user
			// Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
			yield return FileBrowser.WaitForLoadDialog(false, null, "Load Replay File", "Load");

			// Dialog is closed
			if (FileBrowser.Success)
			{
				m_loadReplayButton.gameObject.SetActive(false);

				Helper.ReplayPath = FileBrowser.Result;
				Helper.ReplayMode = true;
				StartCoroutine(LoadGameScene());
			}
			else
			{
				m_connectButton.gameObject.SetActive(true);
				m_loadReplayButton.interactable = true;
			}
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
			int port;
			if (int.TryParse(m_portInputText.text, out port))
				PlayerConfigs.Port = port;
			else
				PlayerConfigs.Port = 0;
		}
	}
}
