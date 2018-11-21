using DG.Tweening;
using TMPro;

namespace Koala
{
	public class ChangeTextOccurrence : BaseOccurrence<ChangeTextOccurrence, Director.ChangeTextConfig>
	{
		private TextMeshProUGUI _text;


		public ChangeTextOccurrence() { }

		protected override Director.ChangeTextConfig CreateOldConfig()
		{
			var text = GetText();

			var oldConfig = new Director.ChangeTextConfig();

			if (_newConfig.Text != null)
				oldConfig.Text = text.text;

			if (_newConfig.FontSize.HasValue)
				oldConfig.FontSize = text.fontSize;

			if (_newConfig.Alignment.HasValue)
				oldConfig.Alignment = text.alignment;

			if (_newConfig.WordWrappingRatios.HasValue)
				oldConfig.WordWrappingRatios = text.wordWrappingRatios;

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangeTextConfig config, bool isForward)
		{
			var text = GetText();

			if (config.FontSize.HasValue)
			{
				DOTween.To(
					() => text.fontSize,
					x => text.fontSize = x,
					config.FontSize.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(Director.ChangeTextConfig config, bool isForward)
		{
			var text = GetText();

			if (config.Text != null)
				text.text = config.Text;

			if (config.Alignment.HasValue)
				text.alignment = config.Alignment.Value;

			if (config.WordWrappingRatios.HasValue)
				text.wordWrappingRatios = config.WordWrappingRatios.Value;
		}

		private TextMeshProUGUI GetText()
		{
			if (_text == null)
				_text = References.Instance.GetGameObject(_reference).GetComponent<TextMeshProUGUI>();
			return _text;
		}
	}
}
