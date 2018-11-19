using System.Linq;
using UnityEngine;

namespace Koala
{
	public sealed class Ellipse2D : Shape2D
	{
		private static readonly float MIN_RADIUS = 0.00005f;

		private float _xRadius = MIN_RADIUS;
		private float _yRadius = MIN_RADIUS;

		public float XRadius
		{
			get { return _xRadius; }
			set { _needUpdate = true; _xRadius = value; }
		}
		public float YRadius
		{
			get { return _yRadius; }
			set { _needUpdate = true; _yRadius = value; }
		}


		protected override void UpdateMesh()
		{
			if (XRadius > MIN_RADIUS && YRadius > MIN_RADIUS)
			{
				// We want to make sure that the ellipse appears to be curved.
				// This can be approximated by drawing a regular polygon with lots of segments.
				// The number of segments can be increased based on the radius so that large ellipses also appear curved.
				// We use an offset and multiplier to create a tunable linear function.
				const float segmentOffset = 40f;
				const float segmentMultiplier = 5f;
				var numSegments = (int)(Mathf.Max(XRadius, YRadius) * segmentMultiplier + segmentOffset);

				// Create an array of points arround a cricle
				var ellipseVertices = Enumerable.Range(0, numSegments)
					.Select(i =>
					{
						var theta = 2 * Mathf.PI * i / numSegments;
						return new Vector2(XRadius * Mathf.Cos(theta), YRadius * Mathf.Sin(theta));
					})
					.ToArray();

				// Find all the triangles in the shape
				var triangles = new Triangulator(ellipseVertices).Triangulate();

				// Assign each vertex the fill color
				var colors = Enumerable.Repeat(FillColor, ellipseVertices.Length).ToArray();

				_mesh = new Mesh
				{
					name = "Ellipse",
					vertices = ellipseVertices.ToVector3(),
					triangles = triangles,
					colors = colors,
				};

				_mesh.RecalculateNormals();
				_mesh.RecalculateBounds();
				//_mesh.RecalculateTangents();

				_meshFilter.mesh = _mesh;
			}
			else
			{
				_meshFilter.mesh = null;
			}
		}

		protected override int SAFE_OUTLINE_ADD_COUNT
		{
			get { return 3; }
		}
	}
}
