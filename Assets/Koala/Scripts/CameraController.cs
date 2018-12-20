using UnityEngine;

namespace Koala
{
	public class CameraController : MonoBehaviour
	{
		// wasd : basic movement
		// shift : Makes camera accelerate
		// space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/

		// camera movements
		float mainSpeed = 50.0f; //regular speed
		float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
		float maxShift = 1000.0f; //Maximum speed when holdin gshift
		float camSens = 4.0f; //How sensitive it with mouse
		private float totalRun = 1.0f;

		void Update()
		{
			if (!Helper.GameStarted) return;

			// Mouse
			if (Input.GetMouseButton(0))
			{
				transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y") * camSens, Input.GetAxis("Mouse X") * camSens, 0));
				var X = transform.rotation.eulerAngles.x;
				var Y = transform.rotation.eulerAngles.y;
				transform.rotation = Quaternion.Euler(X, Y, 0);
			}

			//Keyboard commands
			Vector3 p = GetBaseInput();
			if (Input.GetKey(KeyCode.LeftShift))
			{
				totalRun += Time.deltaTime;
				p = p * totalRun * shiftAdd;
				p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
				p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
				p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
			}
			else
			{
				totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
				p = p * mainSpeed;
			}

			p = p * Time.deltaTime;
			Vector3 newPosition = transform.position;
			if (Input.GetKey(KeyCode.Space)) //If player wants to move on X and Z axis only
			{
				transform.Translate(p);
				newPosition.x = transform.position.x;
				newPosition.z = transform.position.z;
				transform.position = newPosition;
			}
			else
			{
				transform.Translate(p);
			}
		}

		private Vector3 GetBaseInput() //returns the basic values, if it's 0 than it's not active.
		{
			Vector3 p_Velocity = new Vector3();
			if (Input.GetKey(KeyCode.W))
			{
				p_Velocity += new Vector3(0, 0, 1);
			}
			if (Input.GetKey(KeyCode.S))
			{
				p_Velocity += new Vector3(0, 0, -1);
			}
			if (Input.GetKey(KeyCode.A))
			{
				p_Velocity += new Vector3(-1, 0, 0);
			}
			if (Input.GetKey(KeyCode.D))
			{
				p_Velocity += new Vector3(1, 0, 0);
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
