using System.Linq;
using UnityEngine;

namespace Koala
{
	[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter), typeof(LineRenderer))]
	public abstract class Shape2D : MonoBehaviour
	{
		protected MeshFilter _meshFilter;
		protected LineRenderer _lineRenderer;
		protected bool _needUpdate;
		protected Mesh _mesh;

		private Color _fillColor = Color.white;
		private Color _outlineColor = Color.black;
		private float _outlineWidth = 0.01f;

		public Color FillColor
		{
			get { return _fillColor; }
			set { _needUpdate = true; _fillColor = value; }
		}
		public Color OutlineColor
		{
			get { return _outlineColor; }
			set
			{
				_outlineColor = value;
				_lineRenderer.startColor = _outlineColor;
				_lineRenderer.endColor = _outlineColor;
			}
		}
		public float OutlineWidth
		{
			get { return _outlineWidth; }
			set
			{
				_outlineWidth = value;
				_lineRenderer.widthMultiplier = _outlineWidth;
			}
		}


		protected virtual void Awake()
		{
			_meshFilter = GetComponent<MeshFilter>();

			_lineRenderer = GetComponent<LineRenderer>();
			_lineRenderer.startColor = OutlineColor;
			_lineRenderer.endColor = OutlineColor;
			_lineRenderer.widthMultiplier = OutlineWidth;

			_needUpdate = true;
			_mesh = new Mesh();
		}

		protected virtual void Update()
		{
			if (_needUpdate)
			{
				_needUpdate = false;
				UpdateShape();
			}
		}

		private void UpdateShape()
		{
			UpdateMesh();
			//UpdateOutline();
		}

		protected abstract void UpdateMesh();
		protected virtual void UpdateOutline()
		{
			if (OutlineWidth == 0)
			{
				_lineRenderer.positionCount = 0;
				return;
			}

			int meshCount = _meshFilter.mesh.vertices.Length;
			int count = meshCount + SAFE_OUTLINE_ADD_COUNT;

			_lineRenderer.positionCount = count;

			for (int i = 0; i < meshCount; i++)
				_lineRenderer.SetPosition(i, _meshFilter.mesh.vertices[i]);

			for (int i = 0; i < SAFE_OUTLINE_ADD_COUNT && i < meshCount; i++)
				_lineRenderer.SetPosition(meshCount + i, _meshFilter.mesh.vertices[i]);
		}
		protected abstract int SAFE_OUTLINE_ADD_COUNT { get; }
	}
}
