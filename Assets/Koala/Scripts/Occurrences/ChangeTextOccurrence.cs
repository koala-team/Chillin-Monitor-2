using UnityEngine;
using Chronos;
using TMPro;

namespace Koala
{
	public class ChangeTextOccurrence : Occurrence
	{
		private TextMeshProUGUI _text;
		private Director.ChangeTextConfig _oldConfig;

		private string _reference;
		private Director.ChangeTextConfig _newConfig;


		public ChangeTextOccurrence(string reference, Director.ChangeTextConfig newConfig)
		{
			_reference = reference;
			_newConfig = newConfig;
		}

		public override void Forward()
		{
			CreateOldConfig();

			ApplyConfig(_newConfig, true);
		}

		public override void Backward()
		{
			ApplyConfig(_oldConfig, false);
		}

		private void CreateOldConfig()
		{
			_oldConfig = new Director.ChangeTextConfig();
			var text = GetText();

			if (_newConfig.Text != null)
				_oldConfig.Text = text.text;
			if (_newConfig.FontSize.HasValue)
				_oldConfig.FontSize = text.fontSize;
			if (_newConfig.Alignment.HasValue)
				_oldConfig.Alignment = text.alignment;
			if (_newConfig.WordWrappingRatios.HasValue)
				_oldConfig.WordWrappingRatios = text.wordWrappingRatios;
		}

		private void ApplyConfig(Director.ChangeTextConfig config, bool isForward)
		{
			var text = GetText();

			if (config.Text != null)
				text.text = config.Text;
			if (config.FontSize.HasValue)
				text.fontSize = config.FontSize.Value;
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
