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
			// Setup timeline
			Timeline.Instance.Reset();
			Timeline.Instance.TimeScale = 1;

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
			Helper.GameName = null;

			// Config Tweens
			TweensManager.Instance.Reset();
			DOTween.Init();
			DOTween.defaultEaseType = Ease.Linear;
			DOTween.defaultUpdateType = UpdateType.Manual;
			DOTween.useSafeMode = false;

			// Start Proper HandleMessages
			if (Helper.ReplayMode)
				StartCoroutine(HandleReplayMessages());
			else
				StartCoroutine(HandleOnlineMessages());
		}

		public void FixedUpdate()
		{
			if (Helper.CycleDuration != 0)
				Timeline.Instance.Update(Time.fixedDeltaTime, Helper.MaxCycle * Helper.CycleDuration);
		}

		public void Update()
		{
			// Update Cycle UI
			float cycle = Helper.CycleDuration == 0 ? 0 : Timeline.Instance.Time / Helper.CycleDuration;
			m_cycleSlider.maxValue = Helper.MaxCycle;
			m_cycleSlider.value = cycle;
			m_cycleText.text = string.Format("{0} / {1} @{2}X", cycle.TruncateDecimal(1).ToString("0.0"), (int)Helper.MaxCycle, Timeline.Instance.TimeScale);

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

		private void ChangeTimeScale(float amount)
		{
			Timeline.Instance.TimeScale += amount;

			Helper.SetAnimatorsTimeScale(m_rootGameObject);
			Helper.SetAudioSourcesTimeScale(m_rootGameObject);
		}

		public void Pause()
		{
			if (Timeline.Instance.TimeScale == 0) return;

			ChangeTimeScale(-Timeline.Instance.TimeScale);
			ShowPauseOrPlay(false);
		}

		public void PlayForward()
		{
			if (Timeline.Instance.TimeScale == 1) return;

			ChangeTimeScale(1);
			ShowPauseOrPlay(true);
		}

		public void PlayBackward()
		{
			if (Timeline.Instance.TimeScale == -1) return;

			ChangeTimeScale(-1);
			ShowPauseOrPlay(true);
		}

		public void IncSpeed()
		{
			ChangeTimeScale(TIME_SCALE_DELTA);
			ShowPauseOrPlay(Timeline.Instance.TimeScale != 0);
		}

		public void DecSpeed()
		{
			ChangeTimeScale(-TIME_SCALE_DELTA);
			ShowPauseOrPlay(Timeline.Instance.TimeScale != 0);
		}

		public void ShowPauseOrPlay(bool isPlaying)
		{
			m_playButton.gameObject.SetActive(!isPlaying);
			m_pauseButton.gameObject.SetActive(isPlaying);
		}

		private IEnumerator HandleOnlineMessages()
		{
			Task<KS.KSObject> recvTask;

			while (!GameEnded && Helper.Protocol.Network.IsConnected)
			{
				recvTask = Helper.Protocol.RecvMessage();
				yield return recvTask.WaitUntilComplete();

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
						Cycle = Helper.MaxCycle,
						Team = agentJoined.TeamNickname,
						Side = agentJoined.SideName,
						AgentName = agentJoined.AgentName,
					});
					break;

				case AgentLeft.NameStatic:
					var agentLeft = (AgentLeft)message;
					Director.Action(new KS.SceneActions.AgentLeft
					{
						Cycle = Helper.MaxCycle,
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
			yield return null;
			Pause();

			TextMeshProUGUI text = m_startGamePanel.GetComponentInChildren<TextMeshProUGUI>();
			DateTime baseTime = new DateTime(1970, 1, 1);

			while (true)
			{
				double remainingTime = startTime - DateTime.UtcNow.Subtract(baseTime).TotalSeconds;
				if (remainingTime > 0)
					text.text = string.Format("Game Starts in<br/>{0}", remainingTime.TruncateDecimal(2));
				else
					break;
			}

			Helper.GameStarted = true;
			PlayForward();
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
