using DG.Tweening;
using TMPro;
using KS.SceneActions;

namespace Koala
{
	public class ChangeTextOccurrence : BaseOccurrence<ChangeTextOccurrence, ChangeText>
	{
		private TextMeshProUGUI _text;


		public ChangeTextOccurrence() { }

		protected override ChangeText CreateOldConfig()
		{
			var text = GetText();

			var oldConfig = new ChangeText();

			if (_newConfig.Font != null)
				oldConfig.Font = text.font;

			if (_newConfig.Text != null)
				oldConfig.Text = text.text;

			if (_newConfig.FontSize.HasValue)
				oldConfig.FontSize = text.fontSize;

			if (_newConfig.Alignment.HasValue)
				oldConfig.Alignment = (ETextAlignmentOption)text.alignment;

			if (_newConfig.WordWrappingRatios.HasValue)
				oldConfig.WordWrappingRatios = text.wordWrappingRatios;

			return oldConfig;
		}

		protected override void ManageTweens(ChangeText config, bool isForward)
		{
			var text = GetText();

			if (config.FontSize.HasValue)
			{
				DOTween.To(
					() => text.fontSize,
					x => text.fontSize = x,
					config.FontSize.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(ChangeText config, bool isForward)
		{
			var text = GetText();

			if (config.Font != null)
				text.font = config.Font;

			if (config.Text != null)
				text.text = config.Text;

			if (config.Alignment.HasValue)
				text.alignment = (TextAlignmentOptions)config.Alignment.Value;

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
