using UnityEngine;
using DG.Tweening;
using System.Linq;
using System.Collections.Generic;
using KS.SceneActions;

namespace Koala
{
	public class ChangePolygon2DOccurrence : BaseOccurrence<ChangePolygon2DOccurrence, ChangePolygon2D>
	{
		private Polygon2D _polygon2D = null;


		public ChangePolygon2DOccurrence() { }

		protected override ChangePolygon2D CreateOldConfig()
		{
			Polygon2D polygon = GetPolygon2D();

			var oldConfig = new ChangePolygon2D();

			if (_newConfig.FillColor != null)
				oldConfig.FillColor = polygon.FillColor.ToKSVector4();

			if (_newConfig.Vertices != null)
			{
				var vertices = polygon.Vertices;

				if (vertices.Length == 0)
				{
					oldConfig.Vertices = new List<KS.SceneActions.Vector2> { };
				}
				else
				{
					oldConfig.Vertices = Enumerable.Range(0, vertices.Length)
						.Select(i =>
						{
							return vertices[i].ToKSVector2();
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

		protected override void ManageTweens(ChangePolygon2D config, bool isForward)
		{
			Polygon2D polygon = GetPolygon2D();

			if (config.FillColor != null)
			{
				DOTween.To(
					() => polygon.FillColor,
					x => polygon.FillColor = x,
					polygon.FillColor.ApplyKSVector4(config.FillColor),
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
						polygon.Vertices[index].ApplyKSVector2(config.Vertices[index]),
						_duration).RegisterInTimeline(_startTime, isForward);
				}
			}
		}

		protected override void ManageSuddenChanges(ChangePolygon2D config, bool isForward)
		{
			Polygon2D polygon = GetPolygon2D();

			if (config.Vertices != null && config.SuddenChange)
			{
				polygon.Vertices = Enumerable.Range(0, config.Vertices.Count)
					.Select(i =>
					{
						return UnityEngine.Vector2.zero.ApplyKSVector2(config.Vertices[i]);
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
