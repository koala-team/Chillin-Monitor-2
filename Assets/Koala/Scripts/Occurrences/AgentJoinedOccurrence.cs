using KS.SceneActions;

namespace Koala
{
	public class AgentJoinedOccurrence : BaseOccurrence<AgentJoinedOccurrence, AgentJoined>
	{
		public AgentJoinedOccurrence() { }

		protected override AgentJoined CreateOldConfig()
		{
			var oldConfig = new AgentJoined()
			{
				Team = "-",
				Side = _newConfig.Side,
				AgentName = _newConfig.AgentName,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(AgentJoined config, bool isForward)
		{
			Helper.PlayersBoard.ChangeAgentTeam(config.Team, config.Side, config.AgentName);
		}
	}
}
