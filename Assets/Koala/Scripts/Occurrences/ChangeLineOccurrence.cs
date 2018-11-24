using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;

namespace Koala
{
	public class ChangeLineOccurrence : BaseOccurrence<ChangeLineOccurrence, Director.ChangeLineConfig>
	{
		private Line _line = null;


		public ChangeLineOccurrence() { }

		protected override Director.ChangeLineConfig CreateOldConfig()
		{
			Line line = GetLine();

			var oldConfig = new Director.ChangeLineConfig();

			if (_newConfig.FillColor != null)
				oldConfig.FillColor = line.FillColor.ToChangeVector4Config();

			if (_newConfig.Vertices != null)
			{
				var vertices = line.Vertices;

				if (vertices.Length == 0)
				{
					oldConfig.Vertices = new List<Director.ChangeVector3Config> { };
				}
				else
				{
					oldConfig.Vertices = Enumerable.Range(0, vertices.Length)
						.Select(i =>
						{
							return vertices[i].ToChangeVector3Config();
						})
						.ToList();
				}

				if (vertices.Length != _newConfig.Vertices.Count)
				{
					_newConfig.SuddenChange = true;
					oldConfig.SuddenChange = true;
				}
			}

			if (_newConfig.Width.HasValue)
				oldConfig.Width = line.Width;

			if (_newConfig.CornerVertices.HasValue)
				oldConfig.CornerVertices = line.CornerVertices;

			if (_newConfig.EndCapVertices.HasValue)
				oldConfig.EndCapVertices = line.EndCapVertices;

			if (_newConfig.Loop.HasValue)
				oldConfig.Loop = line.Loop;

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangeLineConfig config, bool isForward)
		{
			Line line = GetLine();

			if (config.FillColor != null)
			{
				DOTween.To(
					() => line.FillColor,
					x => line.FillColor = x,
					line.FillColor.ApplyChangeVector4Config(config.FillColor),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Vertices != null && !config.SuddenChange)
			{
				for (int i = 0; i < line.Vertices.Length; i++)
				{
					if (config.Vertices[i] == null)
						continue;

					int index = i;
					DOTween.To(
						() => line.Vertices[index],
						x => line.SetVertex(index, x),
						line.Vertices[index].ApplyChangeVector3Config(config.Vertices[index]),
						_duration).RegisterInTimeline(_startTime, isForward);
				}
			}

			if (config.Width.HasValue)
			{
				DOTween.To(
					() => line.Width,
					x => line.Width = x,
					config.Width.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.CornerVertices.HasValue)
			{
				DOTween.To(
					() => line.CornerVertices,
					x => line.CornerVertices = x,
					config.CornerVertices.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.EndCapVertices.HasValue)
			{
				DOTween.To(
					() => line.EndCapVertices,
					x => line.EndCapVertices = x,
					config.EndCapVertices.Value,
					_duration).RegisterInTimeline(_startTime, isForward);
			}
		}

		protected override void ManageSuddenChanges(Director.ChangeLineConfig config, bool isForward)
		{
			Line line = GetLine();

			if (config.Vertices != null && config.SuddenChange)
			{
				line.Vertices = Enumerable.Range(0, config.Vertices.Count)
					.Select(i =>
					{
						return Vector3.zero.ApplyChangeVector3Config(config.Vertices[i]);
					})
					.ToArray();
			}

			if (config.Loop.HasValue)
				line.Loop = config.Loop.Value;
		}

		private Line GetLine()
		{
			if (_line == null)
				_line = References.Instance.GetGameObject(_reference).GetComponent<Line>();
			return _line;
		}
	}
}
