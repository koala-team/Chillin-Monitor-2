using KS.SceneActions;

namespace Koala
{
	public class AgentLeftOccurrence : BaseOccurrence<AgentLeftOccurrence, AgentLeft>
	{
		public AgentLeftOccurrence() { }

		protected override AgentLeft CreateOldConfig()
		{
			var oldConfig = new AgentLeft()
			{
				Side = _newConfig.Side,
				AgentName = _newConfig.AgentName,
				IsLeft = false,
			};

			return oldConfig;
		}

		protected override void ManageSuddenChanges(AgentLeft config, bool isForward)
		{
			Helper.PlayersBoard.ChangeAgentstatus(config.IsLeft, config.Side, config.AgentName);
		}
	}
}
