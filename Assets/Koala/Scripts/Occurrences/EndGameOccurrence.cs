using KS.SceneActions;

namespace Koala
{
	public class EndGameOccurrence : BaseOccurrence<EndGameOccurrence, EndGame>
	{
		public EndGameOccurrence() { }

		protected override EndGame CreateOldConfig()
		{
			return new EndGame();
		}

		protected override void ManageSuddenChanges(EndGame config, bool isForward)
		{
			_newConfig.EndGamePanel.SetActive(isForward);
		}
	}
}
