using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	public Transform target;

	float distance = 3f;
	float xSpeed = 30f;
	float ySpeed = 18.75f;

	float yMinLimit = -89.9f;
	float yMaxLimit = 89.9f;

	float distanceMin = 1f;
	float distanceMax = 10f;

	float x = 0.0f;
	float y = 0.0f;
	
	void Start () {

	}

	void FixedUpdate() {
		if (UIManager.gamemode == Gamemode.PLAY) {
			x += Input.GetAxis("Mouse X") * xSpeed * 0.1f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.1f;

			y = clampAngle(y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler(y, x, 0);

			//Adjust for scrollwheel and clamp
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

			/*RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) 
			{
				distance -=  hit.distance;
			}*/

			//Set position
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			//Acctualy set the rot and pos of the camera
			transform.rotation = rotation;
			transform.position = position;
		}
	}


	public static float clampAngle(float angle, float min, float max) {
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
