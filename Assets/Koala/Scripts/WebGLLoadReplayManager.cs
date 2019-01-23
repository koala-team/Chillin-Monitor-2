using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using RestSharp.Contrib;
using UnityEngine.Networking;

namespace Koala
{
	public class WebGLLoadReplayManager : MonoBehaviour
	{
		private byte[] _bytes;
		private IEnumerator _loadGameIEnumerator;
		private IEnumerator _webRequestIEnumerator;

		public TextMeshProUGUI m_operation;
		public Slider m_progress;


		protected void Start()
		{
			Helper.WebGLLoadReplayLoaded = true;
			StartCoroutine(_loadGameIEnumerator = LoadGame());
		}

		private IEnumerator LoadGame()
		{
			var uri = new Uri(Application.absoluteURL);
			if (uri.Query.Length <= 1) yield return Error("Query string not found!");

			// Search for parameters
			var queries = HttpUtility.ParseQueryString(uri.Query, System.Text.Encoding.ASCII);
			// Replay Address
			var replayAddress = queries.Get("replay");
			if (replayAddress == null) yield return Error("Replay Address not found!");
			// Game Name
			var gameName = queries.Get("game_name");
			if (gameName == null) yield return Error("Game Name not found!");
			// Bundles Name
			var bundlesName = queries.GetValues("bundle_name");
			if (bundlesName == null) yield return Error("Bundles Name not found!");
			// Bundles Address
			var bundlesAddress = queries.GetValues("bundle_address");
			if (bundlesAddress == null) yield return Error("Bundles Address not found!");
			// Check Bundles Name and Bundle Addresses have same length
			if (bundlesName.Length != bundlesAddress.Length) Error("Bundles Name and Bundle Addresses must have same number of items!");

			// Downloads
			// Replay
			m_operation.text = "Download Replay";
			yield return StartCoroutine(_webRequestIEnumerator = WebRequest(replayAddress));
			Helper.ReplayMode = true;
			Helper.ReplayBytes = _bytes;
			// Bundles
			bool downloadAll = !BundleManager.Instance.Bundles.ContainsKey(gameName); // if game name not exists, download all bundles
			for (int i = 0; i < bundlesName.Length; i++)
			{
				var bundleName = bundlesName[i];
				var bundleAddress = bundlesAddress[i];

				if (downloadAll || !BundleManager.Instance.Bundles[gameName].ContainsKey(bundleName))
				{
					m_operation.text = string.Format("Download {0} from {1}", bundleName, bundleAddress);
					yield return StartCoroutine(_webRequestIEnumerator = WebRequest(bundleAddress));
					if (!BundleManager.Instance.AddBundle(gameName, bundleName, _bytes))
						yield return Error(string.Format("Error while adding {0} from {1} to {2} game bundles", bundleName, bundleAddress, gameName));
				}
			}

			// Load Game
			var loadGameOperation = SceneManager.LoadSceneAsync("Game");
			m_operation.text = "Load Game";
			while (!loadGameOperation.isDone)
			{
				m_progress.value = loadGameOperation.progress;
				yield return new WaitForEndOfFrame();
			}
		}

		private IEnumerator WebRequest(string url)
		{
			yield return null;

			UnityWebRequest webRequest;
			webRequest = new UnityWebRequest(url)
			{
				downloadHandler = new DownloadHandlerBuffer()
			};
			webRequest.SendWebRequest();

			while (!webRequest.isDone)
			{
				m_progress.value = webRequest.downloadProgress;
				yield return new WaitForEndOfFrame();
			}
			if (webRequest.isHttpError || webRequest.isNetworkError) yield return Error(webRequest.error);
			
			_bytes = webRequest.downloadHandler.data;
		}

		private IEnumerator Error(string error)
		{
			if (_loadGameIEnumerator != null) StopCoroutine(_loadGameIEnumerator);
			if (_webRequestIEnumerator != null) StopCoroutine(_webRequestIEnumerator);
			Debug.LogError(error);
			yield return new WaitForEndOfFrame();
			SceneManager.LoadScene("MainMenu");
		}
	}
}
