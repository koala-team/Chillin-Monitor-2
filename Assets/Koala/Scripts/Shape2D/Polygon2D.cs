using System.Linq;
using UnityEngine;

namespace Koala
{
	public class Polygon2D : Shape2D
	{
		private static readonly int MIN_VERTICES_COUNT = 3;

		private Vector2[] _vertices = new Vector2[0];

		public Vector2[] Vertices
		{
			get { return _vertices; }
			set { _needUpdate = true; _vertices = value; }
		}


		public void SetVertex(int i, Vector2 v)
		{
			_needUpdate = true;
			Vertices[i] = v;
		}
		
		protected override void UpdateMesh()
		{
			if (Vertices.Length >= MIN_VERTICES_COUNT)
			{
				var vertices3D = Vertices.ToVector3();

				// Use the triangulator to get indices for creating triangles
				var triangles = new Triangulator(Vertices).Triangulate();

				// Assign each vertex the fill color
				var colors = Enumerable.Repeat(FillColor, vertices3D.Length).ToArray();

				// Update the mesh
				_mesh = new Mesh
				{
					name = "Polygon",
					vertices = vertices3D,
					triangles = triangles,
					colors = colors,
				};

				_mesh.RecalculateNormals();
				_mesh.RecalculateBounds();

				// Set up game object with mesh;
				_meshFilter.mesh = _mesh;
			}
			else
			{
				_meshFilter.mesh = null;
			}
		}

		protected override int SAFE_OUTLINE_ADD_COUNT
		{
			get { return 0; }
		}
	}
}
