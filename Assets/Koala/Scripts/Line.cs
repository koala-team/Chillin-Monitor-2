using UnityEngine;

namespace Koala
{
	public class Line : MonoBehaviour
	{
		private Vector3[] _vertices = new Vector3[0];
		private LineRenderer _lineRenderer;

		public Vector3[] Vertices
		{
			get { return _vertices; }
			set
			{
				_vertices = value;
				_lineRenderer.positionCount = _vertices.Length;
				_lineRenderer.SetPositions(_vertices);
			}
		}
		public Color FillColor
		{
			get { return _lineRenderer.startColor; }
			set
			{
				_lineRenderer.startColor = value;
				_lineRenderer.endColor = value;
			}
		}
		public float Width
		{
			get { return _lineRenderer.widthMultiplier; }
			set { _lineRenderer.widthMultiplier = value; }
		}
		public int CornerVertices
		{
			get { return _lineRenderer.numCornerVertices; }
			set { _lineRenderer.numCornerVertices = value; }
		}
		public int EndCapVertices
		{
			get { return _lineRenderer.numCapVertices; }
			set { _lineRenderer.numCapVertices = value; }
		}
		public bool Loop
		{
			get { return _lineRenderer.loop; }
			set { _lineRenderer.loop = value; }
		}


		public void SetVertex(int i, Vector3 v)
		{
			Vertices[i] = v;
			_lineRenderer.SetPosition(i, v);
		}

		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();
		}
	}
}
