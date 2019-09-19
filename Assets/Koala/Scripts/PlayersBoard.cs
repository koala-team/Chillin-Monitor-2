using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Koala
{
	public class PlayersBoard : MonoBehaviour
	{
		private readonly List<Agent> _agents = new List<Agent>();

		public GameObject m_playersBoardCell;
		public GameObject m_teamColumn;
		public GameObject m_sideColumn;
		public GameObject m_nameColumn;


		public void Init(Dictionary<string, List<string>> sides, Dictionary<string, string> sideColors)
		{
			Dictionary<string, Color> bgColors = new Dictionary<string, Color>();
			Dictionary<string, Color> contrastColors = new Dictionary<string, Color>();

			foreach (var side in sides.Keys)
			{
				Color bgColor = Color.red;
				ColorUtility.TryParseHtmlString(sideColors[side], out bgColor);

				bgColors[side] = bgColor;
				contrastColors[side] = bgColor.ContrastColor();

				string varName = $"{side}NamesTeam";
				if (Helper.GlobalBlackboard[varName] == null)
					Helper.GlobalBlackboard.AddVariable(varName, typeof(Dictionary<string, string>));
				Helper.GlobalBlackboard[varName] = new Dictionary<string, string>();
				foreach (var name in sides[side])
					(Helper.GlobalBlackboard[varName] as IDictionary)[name] = "-";
			}

			Helper.GlobalBlackboard["Sides"] = sides.Keys.ToList();
			Helper.GlobalBlackboard["SidesColor"] = bgColors;
			Helper.GlobalBlackboard["SidesContrastColor"] = contrastColors;

			bool showNameColumn = false;

			foreach (var side in sides.Keys)
			{
				var names = sides[side];
				Color bgColor = bgColors[side];
				Color contrastColor = contrastColors[side];

				if (names.Count > 1) showNameColumn = true;

				foreach (var name in names)
				{
					var agent = new Agent
					{
						Team = "-",
						Side = side,
						Name = name,
						BackgroundColor = bgColor,
						TextColor = contrastColor,
					};
					agent.Draw(this);
					_agents.Add(agent);
				}
			}

			if (!showNameColumn) m_nameColumn.SetActive(false);
		}

		public void ChangeAgentTeam(string team, string side, string name)
		{
			var agent = _agents.Find(a => a.Side == side && a.Name == name);
			if (agent == null) return;

			agent.ChangeAgentTeam(team);
		}

		public void ChangeAgentstatus(bool isLeft, string side, string name)
		{
			var agent = _agents.Find(a => a.Side == side && a.Name == name);
			if (agent == null) return;

			agent.ChangeAgentStatus(isLeft);
		}

		public class Agent
		{
			public string Team { get; set; }
			public string Side { get; set; }
			public string Name { get; set; }
			public Color BackgroundColor { get; set; }
			public Color TextColor { get; set; }

			private GameObject _teamCell;
			private GameObject _sideCell;
			private GameObject _nameCell;


			public void Draw(PlayersBoard playersBoard)
			{
				_teamCell = Instantiate(playersBoard.m_playersBoardCell, playersBoard.m_teamColumn.transform);
				_sideCell = Instantiate(playersBoard.m_playersBoardCell, playersBoard.m_sideColumn.transform);
				_nameCell = Instantiate(playersBoard.m_playersBoardCell, playersBoard.m_nameColumn.transform);

				EditCell(_teamCell, Team, BackgroundColor, TextColor);
				EditCell(_sideCell, Side, BackgroundColor, TextColor);
				EditCell(_nameCell, Name, BackgroundColor, TextColor);
			}

			public void ChangeAgentTeam(string team)
			{
				(Helper.GlobalBlackboard[$"{Side}NamesTeam"] as IDictionary)[Name] = team;

				EditCell(_teamCell, team);
			}

			public void ChangeAgentStatus(bool isLeft)
			{
				if (isLeft)
				{
					EditCell(_teamCell, null, Color.gray, Color.white);
					EditCell(_sideCell, null, Color.gray, Color.white);
					EditCell(_nameCell, null, Color.gray, Color.white);
				}
				else
				{
					EditCell(_teamCell, null, BackgroundColor, TextColor);
					EditCell(_sideCell, null, BackgroundColor, TextColor);
					EditCell(_nameCell, null, BackgroundColor, TextColor);
				}
			}

			private void EditCell(GameObject cell, string text = null, Color? backgroundColor = null, Color? textColor = null)
			{
				if (text != null)
					cell.GetComponentInChildren<TextMeshProUGUI>().text = text;

				if (backgroundColor.HasValue)
					cell.GetComponent<Image>().color = backgroundColor.Value;

				if (textColor.HasValue)
					cell.GetComponentInChildren<TextMeshProUGUI>().color = textColor.Value;
			}
		}
	}
}
