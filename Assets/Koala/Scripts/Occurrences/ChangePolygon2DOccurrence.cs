using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;

namespace Koala
{
	public class ChangePolygon2DOccurrence : BaseOccurrence<ChangePolygon2DOccurrence, Director.ChangePolygon2DConfig>
	{
		private Polygon2D _polygon2D = null;


		public ChangePolygon2DOccurrence() { }

		protected override Director.ChangePolygon2DConfig CreateOldConfig()
		{
			Polygon2D polygon = GetPolygon2D();

			var oldConfig = new Director.ChangePolygon2DConfig();

			if (_newConfig.FillColor != null)
				oldConfig.FillColor = polygon.FillColor.ToChangeVector4Config();

			if (_newConfig.Vertices != null)
			{
				var vertices = polygon.Vertices;

				if (vertices.Length == 0)
				{
					oldConfig.Vertices = new List<Director.ChangeVector2Config> { };
				}
				else
				{
					oldConfig.Vertices = Enumerable.Range(0, vertices.Length)
						.Select(i =>
						{
							return vertices[i].ToChangeVector2Config();
						})
						.ToList();
				}

				if (vertices.Length != _newConfig.Vertices.Count)
				{
					_newConfig.SuddenChange = true;
					oldConfig.SuddenChange = true;
				}
			}

			return oldConfig;
		}

		protected override void ManageTweens(Director.ChangePolygon2DConfig config, bool isForward)
		{
			Polygon2D polygon = GetPolygon2D();

			if (config.FillColor != null)
			{
				DOTween.To(
					() => polygon.FillColor,
					x => polygon.FillColor = x,
					polygon.FillColor.ApplyChangeVector4Config(config.FillColor),
					_duration).RegisterInTimeline(_startTime, isForward);
			}

			if (config.Vertices != null && !config.SuddenChange)
			{
				for (int i = 0; i < polygon.Vertices.Length; i++)
				{
					if (config.Vertices[i] == null)
						continue;

					int index = i;
					DOTween.To(
						() => polygon.Vertices[index],
						x => polygon.SetVertex(index, x),
						polygon.Vertices[index].ApplyChangeVector2Config(config.Vertices[index]),
						_duration).RegisterInTimeline(_startTime, isForward);
				}
			}
		}

		protected override void ManageSuddenChanges(Director.ChangePolygon2DConfig config, bool isForward)
		{
			Polygon2D polygon = GetPolygon2D();

			if (config.Vertices != null && config.SuddenChange)
			{
				polygon.Vertices = Enumerable.Range(0, config.Vertices.Count)
					.Select(i =>
					{
						return Vector2.zero.ApplyChangeVector2Config(config.Vertices[i]);
					})
					.ToArray();
			}
		}

		private Polygon2D GetPolygon2D()
		{
			if (_polygon2D == null)
				_polygon2D = References.Instance.GetGameObject(_reference).GetComponent<Polygon2D>();
			return _polygon2D;
		}
	}
}
