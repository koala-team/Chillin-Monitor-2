using UnityEngine;
using UnityEngine.EventSystems;

namespace Koala
{
	public class CameraController : MonoBehaviour
	{
		private const float MIN_BOUNDRY = -10000;
		private const float MAX_BOUNDRY = +10000;

		// wasd : basic movement
		// shift : Makes camera accelerate

		private bool _positionBoundryChanged = false;
		private Vector3 _minPosition = new Vector3(MIN_BOUNDRY, MIN_BOUNDRY, MIN_BOUNDRY);
		private Vector3 _maxPosition = new Vector3(MAX_BOUNDRY, MAX_BOUNDRY, MAX_BOUNDRY);
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
		private Vector2 _minRotation = new Vector2(MIN_BOUNDRY, MIN_BOUNDRY);
		private Vector2 _maxRotation = new Vector2(MAX_BOUNDRY, MAX_BOUNDRY);
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
		private float _minZoom = MIN_BOUNDRY;
		private float _maxZoom = MAX_BOUNDRY;
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

		// camera movements
		private const float MAIN_SPEED = 5.0f; // regular speed
		private const float SHIFT_ADD = 50.0f; // multiplied by how long shift is held. Basically running
		private const float MAX_SHIFT = 1000.0f; // Maximum speed when holding shift
		private const float CAM_SENS = 2.5f; // How sensitive it with mouse
		private const float SCROLL_SPEED = 0.5f;

		private float _totalRun = 1.0f;
		private Camera _camera;

		void Start()
		{
			_camera = GetComponent<Camera>();
		}

		void Update()
		{
			if (!Helper.GameStarted) return;

			// Mouse
			float scrollDelta = 0;
			bool rotated = false;

			if (!EventSystem.current.IsPointerOverGameObject())
			{
				// Rotate
				if (Input.GetMouseButton(0))
				{
					rotated = true;
					transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * CAM_SENS, Input.GetAxis("Mouse X") * CAM_SENS, 0), Space.World);
				}

				// Zoom
				scrollDelta = Input.mouseScrollDelta.y * SCROLL_SPEED;
			}

			if (rotated || _rotationBoundryChanged)
			{
				transform.rotation = Quaternion.Euler(
					Mathf.Clamp(transform.rotation.eulerAngles.x, MinRotation.x, MaxRotation.x),
					Mathf.Clamp(transform.rotation.eulerAngles.y, MinRotation.y, MaxRotation.y),
					0
				);
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
				p = p * Time.deltaTime;
				var newPosition = transform.position + p;

				transform.position = newPosition.Clamp(MinPosition, MaxPosition);
			}

			//if (Input.GetKey(KeyCode.Space)) // If player wants to move on X and Z axis only
			//{
			//	Vector3 newPosition = transform.position;
			//	transform.Translate(p);
			//	newPosition.x = transform.position.x;
			//	newPosition.z = transform.position.z;
			//	transform.position = newPosition;
			//}
			//else
			//{
			//	transform.Translate(p);
			//}
		}

		private Vector3 GetBaseInput() // returns the basic values, if it's 0 than it's not active.
		{
			Vector3 p_Velocity = Vector3.zero;

			if (Input.GetKey(KeyCode.W))
			{
				p_Velocity += _camera.orthographic ? Vector3.up : Vector3.forward;
			}
			if (Input.GetKey(KeyCode.S))
			{
				p_Velocity += _camera.orthographic ? Vector3.down : Vector3.back;
			}
			if (Input.GetKey(KeyCode.A))
			{
				p_Velocity += Vector3.left;
			}
			if (Input.GetKey(KeyCode.D))
			{
				p_Velocity += Vector3.right;
			}
			if (!_camera.orthographic && Input.GetKey(KeyCode.E))
			{
				p_Velocity += Vector3.up;
			}
			if (!_camera.orthographic && Input.GetKey(KeyCode.Q))
			{
				p_Velocity += Vector3.down;
			}

			return p_Velocity;
		}
	}
}
