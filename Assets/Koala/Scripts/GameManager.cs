using UnityEngine;
using System.Collections;
using TMPro;
using DG.Tweening;
using KS.Messages;
using System.Threading.Tasks;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.IO;

namespace Koala
{
	public class GameManager : MonoBehaviour
	{
		public static readonly float TIME_SCALE_DELTA = 0.25f;

		private Director Director { get; set; } = new Director();
		private bool GameEnded { get; set; } = false;
		private MemoryStream ReplayStream { get; set; }
		private bool ForcePause { get; set; } = false;
		private float LastTimeScale { get; set; } = 0;
		private bool HasNewMessage { get; set; } = true;

		public GameObject m_rootGameObject;
		public GameObject m_rootDestroyedGameObject;
		public GameObject m_userCanvasGameObject;
		public FontItem[] m_fontItems;
		public Camera m_mainCamera;

		public Slider m_cycleSlider;
		public TextMeshProUGUI m_cycleText;
		public Button m_playButton;
		public Button m_pauseButton;
		public Button m_incSpeedButton;
		public Button m_decSpeedButton;
		public PlayersBoard m_playersBoard;
		public GameObject m_startGamePanel;
		public GameObject m_endGamePanel;
		public GameObject m_detailCell;


		public void Awake()
		{
			if (Helper.ReplayMode)
				m_startGamePanel.SetActive(false);

			// Setup timeline
			Timeline.Instance.Reset();

			// Reset references map
			References.Instance.ResetMaps();
			References.Instance.AddGameObject(Helper.MainCameraRef.ToString(), m_mainCamera.gameObject); // "MainCamera"

			// set Helpers value
			Helper.CycleDuration = 0;
			Helper.RootGameObject = m_rootGameObject;
			Helper.RootDestroyedGameObject = m_rootDestroyedGameObject;
			Helper.UserCanvasGameObject = m_userCanvasGameObject;
			Helper.Fonts = m_fontItems;
			Helper.PlayersBoard = m_playersBoard;
			Helper.GameStarted = false;
			Helper.MaxCycle = 0;
			Helper.MaxEndTime = 0;
			Helper.GameName = null;

			// Set Global Blackboard values
			Helper.GlobalBlackboard["MainCamera"] = m_mainCamera;
			Helper.GlobalBlackboard["MainCameraDummy"] = m_mainCamera.GetComponent<CameraController>().m_dummy;

			// Config Tweens
			TweensManager.Instance.Reset();
			DOTween.Init();
			DOTween.defaultEaseType = Ease.Linear;
			DOTween.defaultUpdateType = UpdateType.Manual;
			DOTween.useSafeMode = false;
			DOTween.SetTweensCapacity(200, 50);

			// Start Proper HandleMessages
			if (Helper.ReplayMode)
				StartCoroutine(HandleReplayMessages());
			else
				StartCoroutine(HandleOnlineMessages());
		}

        void OnDestroy()
		{
			DOTween.KillAll(false);

			if (!Helper.ReplayMode && Helper.Protocol.Network.IsConnected)
				Helper.Protocol.Network.Disconnect();
		}

		public void FixedUpdate()
		{
			float maxTime = Helper.MaxCycle * Helper.CycleDuration;
			if (ForcePause && LastTimeScale > 0 && Timeline.Instance.Time < maxTime)
				PlayForward(); // exit force pause

			if (Helper.CycleDuration != 0)
			{
				float oldTimeScale = Timeline.Instance.TimeScale;
				if (Timeline.Instance.Update(Time.fixedDeltaTime, maxTime))
				{
					ForcePause = true;
					LastTimeScale = oldTimeScale;
					ShowPauseOrPlay(false);
				}
			}
		}

		public void Update()
		{
			// Update Cycle UI
			m_cycleSlider.maxValue = Helper.MaxCycle;
			m_cycleSlider.value = Timeline.Instance.Cycle;
			m_cycleText.text = string.Format("{0} / {1} @{2}X", Timeline.Instance.Cycle.TruncateDecimal(1).ToString("0.0"), (int)Helper.MaxCycle, Timeline.Instance.TimeScale);

			ControlTime();
		}

		private void ControlTime()
		{
			if (!Helper.GameStarted) return;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (Timeline.Instance.TimeScale == 0)
					PlayForward();
				else
					Pause();
			}
			else if (Input.GetKeyDown(KeyCode.B))
			{
				if (Timeline.Instance.TimeScale == 0)
					PlayBackward();
				else
					Pause();
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				DecSpeed();
			}
			else if (Input.GetKeyDown(KeyCode.V))
			{
				IncSpeed();
			}
		}

		public void Pause()
		{
			ForcePause = false;
			if (Timeline.Instance.TimeScale == 0) return;

			Timeline.Instance.ChangeTimeScale(-Timeline.Instance.TimeScale);
			ShowPauseOrPlay(false);
		}

		public void PlayForward()
		{
			ForcePause = false;
			if (Timeline.Instance.TimeScale == 1) return;

			Timeline.Instance.ChangeTimeScale(1);
			ShowPauseOrPlay(true);
		}

		public void PlayBackward()
		{
			ForcePause = false;
			if (Timeline.Instance.TimeScale == -1) return;

			Timeline.Instance.ChangeTimeScale(-1);
			ShowPauseOrPlay(true);
		}

		public void IncSpeed()
		{
			ForcePause = false;
			Timeline.Instance.ChangeTimeScale(TIME_SCALE_DELTA);
			ShowPauseOrPlay(Timeline.Instance.TimeScale != 0);
		}

		public void DecSpeed()
		{
			ForcePause = false;
			Timeline.Instance.ChangeTimeScale(-TIME_SCALE_DELTA);
			ShowPauseOrPlay(Timeline.Instance.TimeScale != 0);
		}

		public void ShowPauseOrPlay(bool isPlaying)
		{
			m_playButton.gameObject.SetActive(!isPlaying);
			m_pauseButton.gameObject.SetActive(isPlaying);
		}

		private IEnumerator CheckNetworkTimeout()
		{
			yield return null;

			while (HasNewMessage)
			{
				HasNewMessage = false;
				yield return new WaitForSecondsRealtime(Network.MAX_TIMEOUT / 1000f);
			}

			if (Helper.Protocol.Network.IsConnected)
				Helper.Protocol.Network.Disconnect();
		}

		private IEnumerator HandleOnlineMessages()
		{
			StartCoroutine(CheckNetworkTimeout());
			Task<KS.KSObject> recvTask;

			while (!GameEnded && Helper.Protocol.Network.IsConnected)
			{
				recvTask = Helper.Protocol.RecvMessage();
				yield return recvTask.WaitUntilComplete();
				HasNewMessage = true;

				if (!GameEnded)
					ParseMessage(recvTask.Result);
			}

			if (Helper.Protocol.Network.IsConnected)
				Helper.Protocol.Network.Disconnect();
		}

		private IEnumerator HandleReplayMessages()
		{
			using (ReplayStream = new MemoryStream(Helper.ReplayBytes))
			{
				while (!GameEnded && ReplayStream.Position != ReplayStream.Length)
				{
					var recvTask = ReplayStream.Receive();
					yield return recvTask.WaitUntilComplete();
					
					var processTask = Helper.ProcessBuffer(recvTask.Result);
					yield return processTask.WaitUntilComplete();
					
					ParseMessage(processTask.Result);
				}
			}
			
			Helper.ReplayBytes = new byte[0];
		}

		private void ParseMessage(KS.KSObject message)
		{
			if (message == null) return;

			switch (message.Name())
			{
				case GameInfo.NameStatic:
					var gameInfo = (GameInfo)message;
					Helper.CycleDuration = gameInfo.GuiCycleDuration.Value;
					Helper.GameName = gameInfo.GameName;
					m_playersBoard.Init(gameInfo.Sides, gameInfo.GuiSideColors);
					break;

				case AgentJoined.NameStatic:
					var agentJoined = (AgentJoined)message;
					Director.Action(new KS.SceneActions.AgentJoined
					{
						Cycle = 0,
						Team = agentJoined.TeamNickname,
						Side = agentJoined.SideName,
						AgentName = agentJoined.AgentName,
					});
					if (!Helper.GameStarted)
					{
						Helper.MaxCycle += 1;
					}
					break;

				case AgentLeft.NameStatic:
					var agentLeft = (AgentLeft)message;
					Director.Action(new KS.SceneActions.AgentLeft
					{
						Cycle = 0,
						Side = agentLeft.SideName,
						AgentName = agentLeft.AgentName,
						IsLeft = true,
					});
					break;

				case StartGame.NameStatic:
					var startGame = (StartGame)message;
					StartCoroutine(StartGameCounter((int)startGame.StartTime.Value));
					break;

				case EndGame.NameStatic:
					var endGame = (EndGame)message;

					FillEndGamePanel(endGame.WinnerSidename, endGame.Details);
					Director.Action(new KS.SceneActions.EndGame
					{
						Cycle = 1f - 0.01f,
						EndGamePanel = m_endGamePanel,
					});

					// Ensure everythings showed
					while (Helper.MaxCycle * Helper.CycleDuration < Helper.MaxEndTime)
						Helper.MaxCycle += 1;
					Helper.MaxCycle += 1;

					GameEnded = true;
					break;

				case SceneActions.NameStatic:
					var sceneActions = (SceneActions)message;
					foreach (var action in sceneActions.ParsedActions)
					{
						Director.Action(action);
					}
					break;
			}
		}

		private void FillEndGamePanel(string winnerSidename, Dictionary<string, Dictionary<string, string>> details)
		{
			// Fill winner text
			var winnerText = m_endGamePanel.transform.Find("Contents/WinnerText").GetComponent<TextMeshProUGUI>();
			if (winnerSidename == null)
			{
				winnerText.text = "Draw";
				winnerText.color = Color.white;
			}
			else
			{
				winnerText.text = winnerSidename + " wins!";
			}

			// Fill details
			if (details != null)
			{
				var detailsTransform = m_endGamePanel.transform.Find("Contents/Details");
				detailsTransform.gameObject.SetActive(true);
				detailsTransform.GetComponent<GridLayoutGroup>().constraintCount = details.Keys.Count + 1;

				bool firstProperty = true;

				foreach (var property in details.Keys)
				{
					if (firstProperty)
					{
						// Add Headers
						
						CreateDetailCell("", TextAlignmentOptions.Center, detailsTransform);
						foreach (var side in details[property].Keys)
							CreateDetailCell(side, TextAlignmentOptions.Center, detailsTransform);

						firstProperty = false;
					}

					CreateDetailCell(property, TextAlignmentOptions.Center, detailsTransform);
					foreach (var side in details[property].Keys)
						CreateDetailCell(details[property][side], TextAlignmentOptions.Center, detailsTransform);
				}
			}
		}

		private void CreateDetailCell(string text, TextAlignmentOptions alignment, Transform parent)
		{
			var cell = Instantiate(m_detailCell, parent);
			var cellText = cell.GetComponent<TextMeshProUGUI>();
			cellText.text = text;
			cellText.alignment = alignment;
		}

		private IEnumerator StartGameCounter(int startTime)
		{
			if (Helper.ReplayMode)
			{
				Helper.GameStarted = true;
				Destroy(m_startGamePanel);
				yield break;
			}

			yield return null;

			TextMeshProUGUI text = m_startGamePanel.GetComponentInChildren<TextMeshProUGUI>();
			DateTime baseTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

			while (true)
			{
				double remainingTime = startTime - DateTime.UtcNow.Subtract(baseTime).TotalSeconds;
				if (remainingTime > 0)
					text.text = string.Format("Game Starts in<br>{0}", remainingTime.TruncateDecimal(1));
				else
					break;

				yield return new WaitForEndOfFrame();
			}

			Helper.GameStarted = true;
			Destroy(m_startGamePanel);
		}

		public void BackToMainMenu()
		{
			StartCoroutine(BackToMainMenuCoroutine());
		}

		private IEnumerator BackToMainMenuCoroutine()
		{
			GameEnded = true;

			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();

			if (ReplayStream != null)
				ReplayStream.Close();
			Helper.ReplayBytes = new byte[0];

			yield return SceneManager.LoadSceneAsync("MainMenu").WaitUntilComplete();
		}
	}
}
