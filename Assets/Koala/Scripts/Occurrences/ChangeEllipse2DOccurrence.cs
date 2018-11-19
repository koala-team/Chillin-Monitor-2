﻿using UnityEngine;
using DG.Tweening;

namespace Koala
{
	public class ChangeEllipse2DOccurrence : BaseOccurrence<ChangeEllipse2DOccurrence, Director.ChangeEllipse2DConfig>
	{
		private Ellipse2D _ellipse2D = null;


		public ChangeEllipse2DOccurrence() { }

		protected override Director.ChangeEllipse2DConfig CreateOldConfig()
		{
			Ellipse2D ellipse = GetEllipse2D();

			var oldConfig = new Director.ChangeEllipse2DConfig();

			if (_newConfig.FillColor != null)
				oldConfig.FillColor = ellipse.FillColor.ToChangeVector4Config();

			if (_newConfig.XRadius.HasValue)
				oldConfig.XRadius = ellipse.XRadius;

			if (_newConfig.YRadius.HasValue)
				oldConfig.YRadius = ellipse.YRadius;

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangeEllipse2DConfig config, bool isForward)
		{
			Ellipse2D ellipse = GetEllipse2D();

			if (config.FillColor != null)
			{
				DOTween.To(
					() => ellipse.FillColor,
					x => ellipse.FillColor = x,
					ellipse.FillColor.ApplyChangeVector4Config(config.FillColor),
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}

			if (config.XRadius.HasValue)
			{
				DOTween.To(
					() => ellipse.XRadius,
					x => ellipse.XRadius = x,
					config.XRadius.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}

			if (config.YRadius.HasValue)
			{
				DOTween.To(
					() => ellipse.YRadius,
					x => ellipse.YRadius = x,
					config.YRadius.Value,
					_duration).RegisterChronosTimeline(_startTime, isForward);
			}
		}

		private Ellipse2D GetEllipse2D()
		{
			if (_ellipse2D == null)
				_ellipse2D = References.Instance.GetGameObject(_reference).GetComponent<Ellipse2D>();
			return _ellipse2D;
		}
	}
}