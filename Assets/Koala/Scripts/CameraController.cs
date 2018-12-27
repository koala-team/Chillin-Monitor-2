﻿using UnityEngine;

namespace Koala
{
	public class CameraController : MonoBehaviour
	{
		// wasd : basic movement
		// shift : Makes camera accelerate
		// space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/

		public Vector3 MinPosition { get; set; } = new Vector3(float.MinValue, float.MinValue, float.MinValue);
		public Vector3 MaxPosition { get; set; } = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		public Vector2 MinRotation { get; set; } = new Vector2(float.MinValue, float.MinValue);
		public Vector2 MaxRotation { get; set; } = new Vector2(float.MaxValue, float.MaxValue);

		// camera movements
		private const float MAIN_SPEED = 5.0f; // regular speed
		private const float SHIFT_ADD = 50.0f; // multiplied by how long shift is held. Basically running
		private const float MAX_SHIFT = 1000.0f; // Maximum speed when holding shift
		private const float CAM_SENS = 2.5f; // How sensitive it with mouse
		private float totalRun = 1.0f;

		void Update()
		{
			if (!Helper.GameStarted) return;

			// Mouse
			if (Input.GetMouseButton(0))
			{
				transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * CAM_SENS, Input.GetAxis("Mouse X") * CAM_SENS, 0), Space.World);
				transform.rotation = Quaternion.Euler(
					Mathf.Clamp(transform.rotation.eulerAngles.x, MinRotation.x, MaxRotation.x),
					Mathf.Clamp(transform.rotation.eulerAngles.y, MinRotation.y, MaxRotation.y),
					0
				);
			}

			//Keyboard commands
			Vector3 p = GetBaseInput();
			if (Input.GetKey(KeyCode.LeftShift))
			{
				totalRun += Time.deltaTime;
				p = p * totalRun * SHIFT_ADD;
				p.x = Mathf.Clamp(p.x, -MAX_SHIFT, MAX_SHIFT);
				p.y = Mathf.Clamp(p.y, -MAX_SHIFT, MAX_SHIFT);
				p.z = Mathf.Clamp(p.z, -MAX_SHIFT, MAX_SHIFT);
			}
			else
			{
				totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, MAX_SHIFT);
				p = p * MAIN_SPEED;
			}

			p = p * Time.deltaTime;
			var newPosition = transform.position + p;

			transform.position = new Vector3(
				Mathf.Clamp(newPosition.x, MinPosition.x, MaxPosition.x),
				Mathf.Clamp(newPosition.y, MinPosition.y, MaxPosition.y),
				Mathf.Clamp(newPosition.z, MinPosition.z, MaxPosition.z)
			);

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
			Vector3 p_Velocity = new Vector3();

			if (Input.GetKey(KeyCode.W))
			{
				p_Velocity += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.S))
			{
				p_Velocity += Vector3.back;
			}
			if (Input.GetKey(KeyCode.A))
			{
				p_Velocity += Vector3.left;
			}
			if (Input.GetKey(KeyCode.D))
			{
				p_Velocity += Vector3.right;
			}
			if (Input.GetKey(KeyCode.E))
			{
				p_Velocity += Vector3.up;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				p_Velocity += Vector3.down;
			}

			return p_Velocity;
		}
	}
}
