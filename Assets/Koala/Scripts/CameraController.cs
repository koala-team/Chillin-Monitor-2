using UnityEngine;
using UnityEngine.EventSystems;

namespace Koala
{
	// wasd : basic movement
	// shift : Makes camera accelerate
	public class CameraController : MonoBehaviour
	{
		// camera movements
		private const float MAIN_SPEED = 10.0f; // regular speed
		private const float SHIFT_ADD = 50.0f; // multiplied by how long shift is held. Basically running
		private const float MAX_SHIFT = 1000.0f; // Maximum speed when holding shift
		private const float CAM_SENS = 4.5f; // How sensitive it with mouse
		private const float SCROLL_SPEED = 0.5f;
		private const float MOVE_LERP_T = 0.5f;
		private const float ROTATE_LERP_T = 0.4f;
		private const float ZOOM_LERP_T = 0.4f;

		public Transform m_dummy;

		private bool _positionBoundryChanged = false;
		private Vector3 _minPosition = new Vector3(-1000, -1000, -1000);
		private Vector3 _maxPosition = new Vector3(1000, 1000, 1000);
		public Vector3 MinPosition
		{
			get { return _minPosition; }
			set { _positionBoundryChanged = true; _minPosition = value; }
		}
		public Vector3 MaxPosition
		{
			get { return _maxPosition; }
			set { _positionBoundryChanged = true; _maxPosition = value; }
		}

		private bool _rotationBoundryChanged = false;
		private Vector2 _minRotation = new Vector2(-89.9f, -361);
		private Vector2 _maxRotation = new Vector2(89.9f, 361);
		public Vector2 MinRotation
		{
			get { return _minRotation; }
			set { _rotationBoundryChanged = true; _minRotation = value; }
		}
		public Vector2 MaxRotation
		{
			get { return _maxRotation; }
			set { _rotationBoundryChanged = true; _maxRotation = value; }
		}

		private bool _zoomBoundryChanged = false;
		private float _minZoom = 1;
		private float _maxZoom = 100;
		public float MinZoom
		{
			get { return _minZoom; }
			set { _zoomBoundryChanged = true; _minZoom = value; }
		}
		public float MaxZoom
		{
			get { return _maxZoom; }
			set { _zoomBoundryChanged = true; _maxZoom = value; }
		}

		private float _totalRun = 1.0f;
		private Camera _camera;

		void Start()
		{
			_camera = GetComponent<Camera>();
			m_dummy.position = transform.position;
			m_dummy.rotation = transform.rotation;
		}

		void Update()
		{
			if (!Helper.GameStarted) return;

			var rotation = transform.eulerAngles;
			if (transform.hasChanged)
			{
				m_dummy.position = transform.position;
				m_dummy.rotation = transform.rotation;
			}

			// Mouse
			float scrollDelta = 0;
			bool rotated = false;
			Vector3 rotationChange = Vector3.zero;

			if (!EventSystem.current.IsPointerOverGameObject())
			{
				// Rotate
				if (Input.GetMouseButton(0))
				{
					rotated = true;
					rotationChange = new Vector3(-Input.GetAxis("Mouse Y") * CAM_SENS, Input.GetAxis("Mouse X") * CAM_SENS, 0);
					
				}

				// Zoom
				scrollDelta = Input.mouseScrollDelta.y * SCROLL_SPEED;
			}

			if (rotated || _rotationBoundryChanged)
			{
				Vector3 newRotation = Helper.WrapAngles(m_dummy.eulerAngles + rotationChange).Clamp(MinRotation, MaxRotation);

				m_dummy.transform.RotateAround(m_dummy.transform.position, m_dummy.transform.right, newRotation.x - m_dummy.eulerAngles.x);
				m_dummy.transform.RotateAround(m_dummy.transform.position, Vector3.up, newRotation.y - m_dummy.eulerAngles.y);

				_rotationBoundryChanged = false;
			}

			if (scrollDelta != 0 || _zoomBoundryChanged)
			{
				if (_camera.orthographic)
					_camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - scrollDelta, MinZoom, MaxZoom);
				else
					_camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView - scrollDelta * 3, MinZoom, MaxZoom);

				_zoomBoundryChanged = false;
			}

			//Keyboard commands
			Vector3 p = GetBaseInput();
			if (Input.GetKey(KeyCode.LeftShift))
			{
				_totalRun += Time.deltaTime;
				p = p * _totalRun * SHIFT_ADD;
				p.x = Mathf.Clamp(p.x, -MAX_SHIFT, MAX_SHIFT);
				p.y = Mathf.Clamp(p.y, -MAX_SHIFT, MAX_SHIFT);
				p.z = Mathf.Clamp(p.z, -MAX_SHIFT, MAX_SHIFT);
			}
			else
			{
				_totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, MAX_SHIFT);
				p = p * MAIN_SPEED;
			}

			if (p != Vector3.zero || _positionBoundryChanged)
			{
				if (Input.GetKey(KeyCode.LeftControl)) // If player wants to move on X and Z axis only
					p.y = 0;

				m_dummy.position = (transform.position + p * Time.deltaTime).Clamp(MinPosition, MaxPosition);

				_positionBoundryChanged = false;
			}

			transform.rotation = Quaternion.Lerp(transform.rotation, m_dummy.rotation, ROTATE_LERP_T);
			transform.position = Vector3.Lerp(transform.position, m_dummy.position, MOVE_LERP_T);

			transform.hasChanged = false;
		}

		private Vector3 GetBaseInput() // returns the basic values, if it's 0 than it's not active.
		{
			Vector3 p_Velocity = Vector3.zero;

			if (Input.GetKey(KeyCode.W))
			{
				p_Velocity += _camera.orthographic ? transform.up : transform.forward;
			}
			if (Input.GetKey(KeyCode.S))
			{
				p_Velocity += _camera.orthographic ? -transform.up : -transform.forward;
			}
			if (Input.GetKey(KeyCode.A))
			{
				p_Velocity += -transform.right;
			}
			if (Input.GetKey(KeyCode.D))
			{
				p_Velocity += transform.right;
			}
			if (!_camera.orthographic && Input.GetKey(KeyCode.E))
			{
				p_Velocity += transform.up;
			}
			if (!_camera.orthographic && Input.GetKey(KeyCode.Q))
			{
				p_Velocity += -transform.up;
			}

			return p_Velocity;
		}
	}
}
